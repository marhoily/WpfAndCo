using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace Sample
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private readonly Func<RdbContext> _func;
        private Person _selectedPerson;

        public MainViewModel(Func<RdbContext> func)
        {
            _func = func;
        }

        public List<Person> People { get; set; }
        public List<City> Cities { get; set; }

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (Equals(value, _selectedPerson)) return;
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }
    }
}