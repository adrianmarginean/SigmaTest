using SigmaAssignment.Models;

namespace SigmaAssignment.Data
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task UpsertCandidateAsync(Candidate candidate);
    }
}
