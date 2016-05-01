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

        private bool Equals(CategoryViewModel other)
        {
            return string.Equals(_name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is CategoryViewModel && Equals((CategoryViewModel) obj);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public static bool operator ==(CategoryViewModel left, CategoryViewModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CategoryViewModel left, CategoryViewModel right)
        {
            return !Equals(left, right);
        }
    }
}