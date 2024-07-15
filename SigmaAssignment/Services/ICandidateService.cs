using SigmaAssignment.Models;

namespace SigmaAssignment.Services
{
    public interface ICandidateService
    {
        Task<Candidate> UpsertCandidateAsync(Candidate candidate);
    }
}
