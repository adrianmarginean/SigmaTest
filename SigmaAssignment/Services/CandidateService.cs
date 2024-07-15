using SigmaAssignment.Data;
using SigmaAssignment.Models;

namespace SigmaAssignment.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository ?? throw new ArgumentNullException(nameof(candidateRepository));
        }

        public async Task<Candidate> UpsertCandidateAsync(Candidate candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate.FirstName) ||
                string.IsNullOrWhiteSpace(candidate.LastName) ||
                string.IsNullOrWhiteSpace(candidate.Email) ||
                string.IsNullOrWhiteSpace(candidate.Comment))
            {
                throw new ArgumentException("First name, last name, email, and comment are required.");
            }

            await _candidateRepository.UpsertCandidateAsync(candidate);
            return candidate;
        }
    }

}
