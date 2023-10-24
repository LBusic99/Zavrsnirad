using Microsoft.EntityFrameworkCore;
using NursingHomeResidents.Models;

namespace NursingHomeResidents.Data
{
    public class NursingHomeContext : DbContext
    {
        public DbSet<Resident> Residents { get; set; }

        public NursingHomeContext(DbContextOptions<NursingHomeContext> options) : base(options) { }
    }
}
