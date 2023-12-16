using Microsoft.EntityFrameworkCore;
using PruvodceProject.Models;

namespace PruvodceProject.Data
{
    public class PruvodceData : DbContext
    {
        public DbSet<UcebnaModel> Ucebny { get; set; }
        public DbSet<UzivatelModel> Uzivatele { get; set; }
        public DbSet<UzivatelOvereni> OverovaciUdaje { get; set; }
        public DbSet<AutomatyModel> Automaty { get; set; }
        public DbSet<BudovyModel> Budovy { get; set; }
        public DbSet<StravovaciZarizeniModel> StravovaciZarizeni { get; set; }
        public DbSet<StiznostiModel> Stiznosti { get; set; }
        public DbSet<FotoModelBudovy> FotoBudovy { get; set; }
        public DbSet<FotoModelUcebny> FotoUcebny { get; set; }
        public DbSet<ClanekModel> Clanek { get; set; }
        
        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UcebnaModel>()
                .HasOne(t => t.Budova)
                .WithMany(t => t.Ucebny)
                .HasForeignKey(t => t.BudovaId);
            builder.Entity<BudovyModel>()
                .HasMany(p => p.Fotky)
                .WithOne(p => p.Budova)
                .HasForeignKey(p => p.BudovaID);
        }
    }
}