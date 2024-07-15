using Microsoft.AspNetCore.Mvc;
using Moq;
using SigmaAssignment.Controllers;
using SigmaAssignment.Models;
using SigmaAssignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaAssignment.Tests.Controllers
{
    public class CandidatesControllerTests
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateService> _mockService;

        public CandidatesControllerTests()
        {
            _mockService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockService.Object);
        }

        [Fact]
        public async Task UpsertCandidate_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Required");

            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Comment = "Test comment"
            };

            var result = await _controller.UpsertCandidate(candidate);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpsertCandidate_ReturnsBadRequest_WhenRequiredFieldsAreMissing()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
                // Comment is missing
            };

            _mockService.Setup(service => service.UpsertCandidateAsync(candidate))
                        .ThrowsAsync(new ArgumentException("First name, last name, email, and comment are required."));

            var result = await _controller.UpsertCandidate(candidate);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("First name, last name, email, and comment are required.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpsertCandidate_ReturnsOkResult_WhenCandidateIsValid()
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Test comment"
            };

            _mockService.Setup(service => service.UpsertCandidateAsync(candidate))
                        .ReturnsAsync(candidate);

            var result = await _controller.UpsertCandidate(candidate);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCandidate = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(candidate.Email, returnedCandidate.Email);
        }
    }
}
