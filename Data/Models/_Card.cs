namespace CardShow.Data.Models
{
    public class _Card
    {
        public _Card(
            int setId,
            string name,
            string setIndex,
            int id = 0)
        {
            Id = id;
            SetId = setId;
            Name = name;
            SetIndex = setIndex;
        }

        public int Id { get; }
        public int SetId { get; }
        public string Name { get; }
            = string.Empty;
        public string SetIndex { get; }
            = string.Empty;
    }
}
