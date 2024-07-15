using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Models;

namespace SigmaAssignment.Data
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidatesDbContext _context;

        public CandidateRepository(CandidatesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task UpsertCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == candidate.Email);

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;

                _context.Candidates.Update(existingCandidate);
            }
            else
            {
                await _context.Candidates.AddAsync(candidate);
            }

            await _context.SaveChangesAsync();
        }
    }
}
