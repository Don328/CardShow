using CardShow.Data.Models;

namespace CardShow.Core.Data
{
    public interface ICardShowDbFixture
    {
        IEnumerable<_CardSet> GetAllCardSets();
        Task CreateSet(_CardSet set);
        Task DeleteSet(int id);

        void Dispose();
    }
}