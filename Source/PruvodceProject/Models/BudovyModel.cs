using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class BudovyModel
    {
        [Key] public Guid IdBudovy { get; set; }
        public string name { get; set; }
        [Required] public string Adresa { get; set; }
        [Required] public string KodoveOznaceni { get; set; }
        public ICollection<UcebnaModel> Ucebny { get; set; }
        public ICollection<AutomatyModel> Automaty { get; set; }
        public ICollection<FotoModelBudovy> Fotky { get; set; }
    }
    public static class nasaditData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            List<string> nazvy = new List<string>();
            List<string> adresy = new List<string>();
            List<string> kodovaOznaceni = new List<string>();

            using (var soubor = new StreamReader("./csvdata/budovy.csv"))
            {
                while (!soubor.EndOfStream)
                {
                    var radek = soubor.ReadLine();
                    var hodnoty = radek.Split(";");

                    nazvy.Add(hodnoty[0]);
                    adresy.Add(hodnoty[1]);
                    kodovaOznaceni.Add(hodnoty[2]);
                }
            }

            using (var context = new PruvodceData(serviceProvider.GetRequiredService<DbContextOptions<PruvodceData>>()))
            {
                if (context.Budovy.Any())
                {
                    return;
                }
                for (int i = 1;i < nazvy.Count;i++)
                {
                    context.Budovy.AddRange(
                        new BudovyModel
                        {
                            IdBudovy = Guid.NewGuid(),
                            name = nazvy[i],
                            Adresa = adresy[i],
                            KodoveOznaceni = kodovaOznaceni[i]
                        }
                        );
                }

                context.SaveChanges();
            }
        }
    }
}