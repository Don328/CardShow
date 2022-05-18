using CardShow.Data.Models;

namespace CardShow.Core.Data
{
    public interface IDbFixture
    {
        IEnumerable<_CardSet> GetAllCardSets();
        Task<int> CreateSet(_CardSet set);
        Task DeleteSet(int id);
        Task<bool> SetIsDeleted(int id);

        Task<IEnumerable<_Card>> GetCardsBySet(int setId);
        Task<int> CreateCard(_Card card);
        Task DeleteCard(int id);
        Task<bool> CardIsDeleted(int id);

        Task<IEnumerable<_Assessment>> GetCardAssessments(int cardId);
        Task<int> CreateAssessment(_Assessment assessment);
        Task DeleteAssessment(int id);
        Task<bool> AssessmentIsDeleted(int id);

        void Dispose();
    }
}