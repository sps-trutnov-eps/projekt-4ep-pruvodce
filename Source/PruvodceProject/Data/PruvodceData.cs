using Microsoft.EntityFrameworkCore;

namespace PruvodceProject.Data
{
    public class PruvodceData : DbContext
    {
        public DbSet<Models.UserModel> prihlasovaci_Udaje { get; set; }

        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }
    }
}
