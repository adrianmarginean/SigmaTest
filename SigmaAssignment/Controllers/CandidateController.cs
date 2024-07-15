using Microsoft.AspNetCore.Mvc;
using SigmaAssignment.Models;
using SigmaAssignment.Services;

namespace SigmaAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService ?? throw new ArgumentNullException(nameof(candidateService));
        }

        [HttpPost]
        [Route(nameof(UpsertCandidate))]
        public async Task<IActionResult> UpsertCandidate(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCandidate = await _candidateService.UpsertCandidateAsync(candidate);
                return Ok(updatedCandidate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
