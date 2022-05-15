using CardShow.Data.Models;

namespace CardShow.Core.Data
{
    public interface IDbFixture
    {
        IEnumerable<_CardSet> GetAllCardSets();
        Task<int> CreateSet(_CardSet set);
        Task DeleteSet(int id);
        bool SetIsDeleted(int id);

        IEnumerable<_Card> GetCardsBySet(int setId);
        Task<int> CreateCard(_Card card);
        Task DeleteCard(int id);
        bool CardIsDeleted(int id);

        void Dispose();
    }
}