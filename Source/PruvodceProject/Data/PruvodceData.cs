using Microsoft.EntityFrameworkCore;

namespace PruvodceProject.Data
{
    public class DatovyKontext : DbContext
    {
        public DbSet<Models.UserModel> prihlasovaci_Udaje { get; set; }

        public DatovyKontext(DbContextOptions<DatovyKontext> options) : base(options) { }
    }
}
