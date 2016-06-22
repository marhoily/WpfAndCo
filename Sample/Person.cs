
namespace Sample
{
    public class Person
    {
        public string Name { get; set; }
        [Navigation]
        public City City { get; set; }
    }
}