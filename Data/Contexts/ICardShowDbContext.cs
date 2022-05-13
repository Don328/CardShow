using CardShow.Data.Models;

namespace CardShow.Data.Contexts
{
    public interface ICardShowDbContext
    {
        IEnumerable<_CardSet> Sets { get; }
        void DeleteSet(int id);

        void Dispose();
    }
}