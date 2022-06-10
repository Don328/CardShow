namespace CardShow.Data.Models
{
    public class _CardSet
    {
        public _CardSet(
            int year,
            string name,
            int sport,
            int id = 0)
        {
            Id = id;
            Year = year;
            Name = name;
            Sport = sport;
        }

        public int Id { get; }
        public int Year { get; }
        public string Name { get; }
            = string.Empty;
        public int Sport { get; }
    }
}
