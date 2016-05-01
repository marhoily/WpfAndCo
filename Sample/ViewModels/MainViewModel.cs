using System.IO;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace Alphabet
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private LetterViewModel _letter;
        private const string Store = "../../../letters.txt";

        private static BindableCollection<LetterViewModel> Read()
        {
            return new BindableCollection<LetterViewModel>(
                File.Exists(Store)
                    ? JsonConvert
                        .DeserializeObject<string[]>(File.ReadAllText(Store))
                        .Select(x => new LetterViewModel(x))
                    : Enumerable.Empty<LetterViewModel>());

        }

        public LetterViewModel Letter
        {
            get { return _letter; }
            set
            {
                if (Equals(value, _letter)) return;
                _letter = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanDelete));
            }
        }

        public IObservableCollection<LetterViewModel> Letters { get; set; } = Read();
        public void New()
        {
            var letterViewModel = new LetterViewModel("");
            Letters.Add(letterViewModel);
            Letter = letterViewModel;
        }

        public void Delete() => Letters.Remove(Letter);
        public bool CanDelete => Letter != null;

        public void Load()
        {
            Letters = Read();
            NotifyOfPropertyChange(nameof(Letters));
        } 
        public void Save() => File.WriteAllText(Store,
            JsonConvert.SerializeObject(Letters.Select(x => x.Code)));
    }
}