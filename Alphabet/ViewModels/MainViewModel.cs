using Caliburn.Micro;

namespace Alphabet
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private readonly LettersStore _lettersStore;

        public MainViewModel(LettersStore lettersStore)
        {
            _lettersStore = lettersStore;
            Load();
        }

        private LetterViewModel _letter;

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

        public IObservableCollection<LetterViewModel> Letters { get; set; } 
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
            Letters = _lettersStore.Letters;
            NotifyOfPropertyChange(nameof(Letters));
        }

        public void Save() => _lettersStore.Save();
    }
}