using Caliburn.Micro;
using Newtonsoft.Json;

namespace Alphabet
{
    public sealed class CategoryViewModel : PropertyChangedBase
    {
        [JsonConstructor]
        public CategoryViewModel()
        {
        }

        public CategoryViewModel(string name)
        {
            _name = name;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                NotifyOfPropertyChange();
            }
        }
    }
}