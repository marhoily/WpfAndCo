

using System.ComponentModel.DataAnnotations;

namespace Sample
{
    public class Person
    {
        public string Name { get; set; }
        [Navigation, Required]
        public City City { get; set; }
        [Navigation]
        public City FavoriteCity { get; set; }
    }
}