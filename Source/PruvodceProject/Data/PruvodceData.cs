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

        public DbSet<PhotoModel> Photo { get; set; }


        //Nutno doplnit vazby mezi databázemi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhotoModel>().HasOne<BudovyModel>(a => a.IdBudovy).WithMany(a => a.fotky); //vazba budovy na fotky
            builder.Entity<PhotoModel>().HasOne<Ucebna>(a => a.UcebnaId).WithMany(a => a.fotky); //vazba ucebny na fotky
        }
        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AutomatyModel>().HasOne<BudovyModel>(a => a.budovaID).WithMany(a => a.Automaty);
            builder.Entity<UcebnaModel>().HasOne<BudovyModel>(a => a.budovaID).WithMany(a => a.Ucebny);
        }
    }
}