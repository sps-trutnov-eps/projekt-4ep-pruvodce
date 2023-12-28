using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Datový model pro učebnu.
    /// Primárním klíčem je Id, které je typu GUID
    /// </summary>
    public class UcebnaModel
    {
        [Key] public Guid Id { get; set; }  
        [Required] public string Nazev { get; set; }  
        [Required] public string Patro { get; set; } = String.Empty;
        [Required] public string Druh { get; set; } = String.Empty;
        public string Poddruh { get; set; } = String.Empty;
        public BudovyModel Budova { get; set; }
        [Required] public Guid BudovaId { get; set; }
        public ICollection<FotoModelUcebny> Fotky { get; set; } //vazba na foto 1:N
    }

    public static class nasaditDataUceben
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            List<string> nazvy = new List<string>();
            List<string> patra = new List<string>();
            List<string> druhy = new List<string>();
            List<string> podDruhy = new List<string>();
            List<string> budovy = new List<string>();
            List<Guid> BudovyId = new List<Guid>();

            

            using (var soubor = new StreamReader("./csvdata/ucebny.csv"))
            {
                BudovyId.Add(Guid.NewGuid());
                while(!soubor.EndOfStream)
                {
                    var radek = soubor.ReadLine();
                    var hodnoty = radek.Split(";");

                    nazvy.Add(hodnoty[0]);
                    patra.Add(hodnoty[1]);
                    druhy.Add(hodnoty[2]);
                    podDruhy.Add(hodnoty[3]);
                    budovy.Add(hodnoty[4]);
                }
            }
            using (var context  = new PruvodceData(serviceProvider.GetRequiredService<DbContextOptions<PruvodceData>>()))
            {
                if (context.Ucebny.Any())
                {
                    return;
                }
                for (int i = 1; i < budovy.Count; i++)
                {
                    BudovyModel? hledanaSkolni = context.Budovy.FirstOrDefault(n => n.name == "Školní 101");
                    BudovyModel? hledanaHorska59 = context.Budovy.FirstOrDefault(n => n.name == "Horská 59");
                    BudovyModel? hledanaHorska618 = context.Budovy.FirstOrDefault(n => n.name == "Horská 618");

                    if (budovy[i] == "Školní 101")
                    {
                        BudovyId.Add(hledanaSkolni.IdBudovy);
                    }
                    if (budovy[i] == "Horská 59")
                    {
                        BudovyId.Add(hledanaHorska59.IdBudovy);
                    }
                    if (budovy[i] == "Horská 618")
                    {
                        BudovyId.Add(hledanaHorska618.IdBudovy);
                    }
                }

                for (int i = 1; i < nazvy.Count; i++)
                {
                    context.Ucebny.AddRange(
                        new UcebnaModel
                        {
                            Id = Guid.NewGuid(),
                            Nazev = nazvy[i],
                            Patro = patra[i],
                            Druh = druhy[i],
                            Poddruh = podDruhy[i],
                            BudovaId = BudovyId[i]
                        }
                        );
                }
                context.SaveChanges();
            }
        }
    }
}