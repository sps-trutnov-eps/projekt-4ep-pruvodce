using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class StravovaciZarizeniModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Nazev { get; set; }
        [Required]
        public string Adresa { get; set; }
        public string OdkazNaMenu { get; set; }
        [Required]
        public string Popis { get; set; }
        public string Typ {  get; set; }
    }
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new PruvodceData(serviceProvider.GetRequiredService<DbContextOptions<PruvodceData>>()))
            {
                if(context.StravovaciZarizeni.Any())
                {
                    return;
                }
                context.StravovaciZarizeni.AddRange(
                    new StravovaciZarizeniModel { 
                        ID = Guid.NewGuid(),
                        Nazev = "Nevim",
                        Adresa = "Nevim",
                        OdkazNaMenu = "www.nevim.cz",
                        Popis = "nsdjnvxcvnxj",
                        Typ = "neco"
                    }
                    );
                context.SaveChanges();

            }
        }
    }
}