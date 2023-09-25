using Microsoft.EntityFrameworkCore;
using PruvodceProject.Models;

namespace PruvodceProject.Data
{
    public class PruvodceData : DbContext
    {
        public DbSet<Ucebna> Ucebna { get; set; }
        public DbSet<UserModel> PrihlasovaciUdaje { get; set; }

        public DbSet<UserVerify> OverovaciUdaje { get; set; }

        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }
    }
}
