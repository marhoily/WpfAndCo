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

        public void AddCities()
        {
            using (var ctx = _func())
            {
                ctx.Database.EnsureCreated();
                if (!ctx.Cities.Any())
                {
                    ctx.Cities.Add(new City {Name = "Minsk"});
                    ctx.Cities.Add(new City {Name = "Berlin"});
                    ctx.Cities.Add(new City {Name = "Zurich"});
                }
                ctx.SaveChanges();
            }
        }

        public void Save()
        {
            using (var ctx = _func())
            {
                ctx.People.Add(SelectedPerson);
                ctx.SaveChanges();
            }
        }

        public void NewPerson()
        {
            SelectedPerson = new Person();
        }
        public void Load()
        {
            using (var ctx = _func())
            {
                People = ctx.People.ToList();
                Cities = ctx.Cities.ToList();
                NotifyOfPropertyChange(() => People);
                NotifyOfPropertyChange(() => Cities);
            }
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