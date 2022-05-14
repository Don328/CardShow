using CardShow.Data.Models;

namespace CardShow.Data.Contexts
{
    public interface ICardShowDbContext
    {
        IEnumerable<_CardSet> Sets { get; }
        Task<int> CreateSet(_CardSet set);
        Task DeleteSet(int id);

        void Dispose();
    }
}