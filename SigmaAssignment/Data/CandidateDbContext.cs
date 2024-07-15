using Microsoft.EntityFrameworkCore;
using SigmaAssignment.Models;

namespace SigmaAssignment.Data
{
    public class CandidatesDbContext : DbContext
    {
        public CandidatesDbContext(DbContextOptions<CandidatesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
