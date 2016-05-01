using Caliburn.Micro;

namespace Alphabet
{
    public sealed class LetterViewModel : PropertyChangedBase
    {
        public LetterViewModel(string code)
        {
            _code = code;
        }

        private string _code ;

        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                _code = value;
                NotifyOfPropertyChange();
            }
        }
    }
}