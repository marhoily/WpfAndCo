using System.Collections.Generic;
using Microsoft.Data.Entity;

namespace Sample
{
    public class RdbContext
    {
        public List<Person> People { get; set; }
        public List<City> Cities { get; set; }
    }
}