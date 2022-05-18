using CardShow.Data.Models;

namespace CardShow.Data
{
    public interface ICardShowDbContext
    {
        IEnumerable<_CardSet> Sets { get; }
        
        Task<int> CreateSet(_CardSet set);
        Task DeleteSet(int id);

        Task<IEnumerable<_Card>> GetCardsBySetId(int setId);
        Task<int> CreateCard(_Card card);
        Task DeleteCard(int id);
        Task<bool> CardExists(int id);

        Task<IEnumerable<_Assessment>> GetCardAssessments(int cardId);
        Task<int> CreateAssessment(_Assessment assessment);
        Task DeleteAssessment(int id);
        Task<bool> AssessmentExists(int id);

        void Dispose();
    }
}