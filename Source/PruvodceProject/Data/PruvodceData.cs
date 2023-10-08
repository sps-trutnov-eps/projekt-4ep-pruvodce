using Microsoft.EntityFrameworkCore;
using PruvodceProject.Models;
using System.Security.Cryptography;

namespace PruvodceProject.Data
{
    public class PruvodceData : DbContext
    {
        public DbSet<Ucebna> Ucebna { get; set; }
        public DbSet<UserModel> PrihlasovaciUdaje { get; set; }

        public DbSet<UserVerify> OverovaciUdaje { get; set; }

        public DbSet<AutomatyModel> Automaty { get; set; }

        public DbSet<BudovyModel> Budovy { get; set; }
        
        public DbSet<StravovaciZarizeniModel> StravovaciZarizeni { get; set; }


        //Nutno doplnit vazby mezi databázemi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PruvodceData(DbContextOptions<PruvodceData> options) : base(options) { }
    }
}
