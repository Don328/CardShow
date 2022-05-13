using CardShow.Data.Models;

namespace CardShow.Core.Data
{
    public interface ICardShowDbFixture
    {
        IEnumerable<_CardSet> GetAllCardSets();
        void DeleteSet(int id);

        void Dispose();
    }
}