using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Data;
using SigmaAssignment.Models;

namespace SigmaAssignment.Tests.Data
{
    public class CandidateRepositoryTests
    {
        private readonly CandidateRepository _repository;
        private readonly CandidatesDbContext _context;

        public CandidateRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CandidatesDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CandidatesDbContext(options);
            _repository = new CandidateRepository(_context);
        }

        [Fact]
        public async Task GetCandidateByEmailAsync_ReturnsCandidate_WhenCandidateExists()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test comment"
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            var result = await _repository.GetCandidateByEmailAsync("john.doe@example.com");

            Assert.NotNull(result);
            Assert.Equal(candidate.Email, result.Email);
        }

        [Fact]
        public async Task GetCandidateByEmailAsync_ReturnsNull_WhenCandidateDoesNotExist()
        {
            var result = await _repository.GetCandidateByEmailAsync("nonexistent@example.com");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpsertCandidateAsync_AddsNewCandidate_WhenCandidateDoesNotExist()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test comment"
            };

            await _repository.UpsertCandidateAsync(candidate);
            var result = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == candidate.Email);

            Assert.NotNull(result);
            Assert.Equal(candidate.Email, result.Email);
        }

        [Fact]
        public async Task UpsertCandidateAsync_UpdatesExistingCandidate_WhenCandidateExists()
        {
            var existingCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Comment = "Old comment"
            };

            _context.Candidates.Add(existingCandidate);
            await _context.SaveChangesAsync();

            var updatedCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Comment = "Updated comment"
            };

            await _repository.UpsertCandidateAsync(updatedCandidate);
            var result = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == updatedCandidate.Email);

            Assert.NotNull(result);
            Assert.Equal(updatedCandidate.Comment, result.Comment);
        }
    }
}
