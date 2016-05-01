using Caliburn.Micro;

namespace Alphabet
{
    public sealed class CategoryViewModel : PropertyChangedBase
    {
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