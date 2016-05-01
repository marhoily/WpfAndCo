using System;
using Caliburn.Micro;
using JetBrains.Annotations;

namespace Alphabet
{
    public sealed class CategoryViewModel : PropertyChangedBase
    {
        public CategoryViewModel([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            _name = name;
        }

        [NotNull]private string _name;

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