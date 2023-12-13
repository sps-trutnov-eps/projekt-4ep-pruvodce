using Microsoft.EntityFrameworkCore;
using PruvodceProject.Models;
using System.Security.Cryptography;

namespace PruvodceProject.Data
{
    public class PruvodceData : DbContext
    {
        public DbSet<UcebnaModel> Ucebna { get; set; }
        
        public DbSet<UserModel> PrihlasovaciUdaje { get; set; }

        public DbSet<UserVerify> OverovaciUdaje { get; set; }

        public DbSet<AutomatyModel> Automaty { get; set; }

        public DbSet<BudovyModel> Budovy { get; set; }
        
        public DbSet<StravovaciZarizeniModel> StravovaciZarizeni { get; set; }
        public DbSet<CrowdSourceModel> CrowdSource { get; set; }

        public DbSet<PhotoModelBudovy> PhotoBudovy { get; set; }

        public DbSet<ObchodModel> Obchody { get; set; }
        
        public DbSet<PhotoModelUcebny> PhotoUcebny { get; set; }

        public DbSet<KafeModel> Kafarny { get; set; }

        public DbSet<ClanekModel> Clanek { get; set; }



        //Nutno doplnit vazby mezi databázemi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BudovyModel>().HasMany(t => t.fotky).WithOne(t => t.IdBudovy);
            builder.Entity<UcebnaModel>().HasMany(t => t.fotky).WithOne(t => t.UcebnaId);
            builder.Entity<BudovyModel>().HasMany(t => t.Ucebny).WithOne(t => t.budovaID);
            builder.Entity<BudovyModel>().HasMany(t => t.Automaty).WithOne(t => t.budovaID);
        }
    }
}