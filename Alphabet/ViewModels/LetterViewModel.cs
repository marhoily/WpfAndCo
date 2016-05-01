using Caliburn.Micro;
using Newtonsoft.Json;

namespace Alphabet
{
    public sealed class LetterViewModel : PropertyChangedBase
    {
        [JsonConstructor]
        public LetterViewModel()
        {
        }

        public LetterViewModel(string code)
        {
            _code = code;
        }

        private string _code ;

        [JsonProperty]
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
        [JsonProperty]
        public IObservableCollection<CategoryViewModel> Categories
            { get; } = new BindableCollection<CategoryViewModel>();
    }
}