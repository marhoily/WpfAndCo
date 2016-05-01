using Caliburn.Micro;

namespace Alphabet
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private LetterViewModel _letter;

        public LetterViewModel Letter
        {
            get { return _letter; }
            set
            {
                if (Equals(value, _letter)) return;
                _letter = value;
                NotifyOfPropertyChange();
            }
        }
        //= "AMEMW"
        public IObservableCollection<LetterViewModel> Letters
            { get; } = new BindableCollection<LetterViewModel>();
        public void New() => Letters.Add(new LetterViewModel {Code = "AMEMW" });
        public void Add() => Letters.Add(Letter);
    }
}