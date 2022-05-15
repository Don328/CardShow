using CardShow.Data.Models;

namespace CardShow.Data.Contexts
{
    public interface ICardShowDbContext
    {
        IEnumerable<_CardSet> Sets { get; }
        Task<int> CreateSet(_CardSet set);
        Task DeleteSet(int id);
        
        IEnumerable<_Card> GetCardsBySetId(int setId);
        Task<int> CreateCard(_Card card);
        Task DeleteCard(int id);
        bool CardExists(int id);

        void Dispose();
    }
}