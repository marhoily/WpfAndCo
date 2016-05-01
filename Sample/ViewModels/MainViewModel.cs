using Caliburn.Micro;

namespace Alphabet
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private string _input = "AMEMW";

        public string Input
        {
            get { return _input; }
            set
            {
                if (value == _input) return;
                _input = value;
                NotifyOfPropertyChange();
            }
        }
        public IObservableCollection<string> Letters
            { get; } = new BindableCollection<string>();
        public void Add()
        {
            Letters.Add(Input);
        }
    }
}