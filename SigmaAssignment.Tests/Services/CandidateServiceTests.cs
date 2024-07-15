using Moq;
using SigmaAssignment.Data;
using SigmaAssignment.Models;
using SigmaAssignment.Services;

namespace SigmaAssignment.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly ICandidateService _candidateService;
        private readonly Mock<ICandidateRepository> _mockRepo;

        public CandidateServiceTests()
        {
            _mockRepo = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_mockRepo.Object);
        }

        [Fact]
        public async Task UpsertCandidate_ThrowsExceptionIfRequiredFieldsAreMissing()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
                // Comment is missing
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _candidateService.UpsertCandidateAsync(candidate));
        }

        [Fact]
        public async Task UpsertCandidate_AddsNewCandidate()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test comment"
            };

            _mockRepo.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email))
                     .ReturnsAsync((Candidate)null);

            var result = await _candidateService.UpsertCandidateAsync(candidate);

            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@example.com", result.Email);

            _mockRepo.Verify(repo => repo.UpsertCandidateAsync(It.IsAny<Candidate>()), Times.Once);
        }

        [Fact]
        public async Task UpsertCandidate_UpdatesExistingCandidate()
        {
            var existingCandidate = new Candidate
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Comment = "Old comment"
            };

            var updatedCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Comment = "Updated comment"
            };

            _mockRepo.Setup(repo => repo.GetCandidateByEmailAsync(existingCandidate.Email))
                     .ReturnsAsync(existingCandidate);

            var result = await _candidateService.UpsertCandidateAsync(updatedCandidate);

            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Smith", result.LastName);
            Assert.Equal("jane.smith@example.com", result.Email);
            Assert.Equal("Updated comment", result.Comment);

            _mockRepo.Verify(repo => repo.UpsertCandidateAsync(It.IsAny<Candidate>()), Times.Once);
        }
    }
}
