namespace Sample
{
    public class City
    {
        public string Name { get; set; }
        [Navigation]
        public City BrotherCity { get; set; }
    }
}