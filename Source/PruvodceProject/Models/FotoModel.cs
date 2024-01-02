using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace PruvodceProject.Models
{
    public class FotoModelBudovy
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Nazev { get; set; }
        [Required] public string Cesta { get; set; }
        [Required] public string Pripona { get; set; }
        public Guid BudovaID { get; set; }  //dva řádky kvůli vazbě 1:N (1 budova: N fotkám) V.K.
        public BudovyModel Budova { get; set; }
    }
    public class FotoModelUcebny
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Nazev { get; set; }
        [Required] public string Cesta { get; set; }
        [Required] public string Pripona { get; set; }
        public Guid UcebnaID { get; set; }  //tady ty další jsou zas kvůli vazbě 1:N (1 učebna: N fotkám) V.K.
        public UcebnaModel Ucebna { get; set; }
    }

    public static class nasaditDataKFotkamUceben
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            List<string> nazvy = new List<string>();
            List<string> cesty = new List<string>();
            List<string> pripony = new List<string>();
            List<string> ucebny = new List<string>();
            List<Guid> IdUceben = new List<Guid>();

            using (var soubor = new StreamReader("./csvdata/fotoUcebny.csv"))
            {
                IdUceben.Add(Guid.NewGuid());
                while (!soubor.EndOfStream)
                {
                    var radek = soubor.ReadLine();
                    var hodnoty = radek.Split(";");

                    nazvy.Add(hodnoty[0]);
                    cesty.Add(hodnoty[1]);
                    pripony.Add(hodnoty[2]);
                    ucebny.Add(hodnoty[3]);
                }
            }
            using (var context = new PruvodceData(serviceProvider.GetRequiredService<DbContextOptions<PruvodceData>>()))
            {
                if (context.FotoUcebny.Any())
                {
                    return;
                }
                for(int i = 1; i < ucebny.Count; i++)
                {
                    UcebnaModel? hledanaUcebna = context.Ucebny.FirstOrDefault(n => n.Nazev == ucebny[i]); 
                    if(hledanaUcebna == null)
                    {
                        continue;
                    }
                    IdUceben.Add(hledanaUcebna.Id);
                }

                for (int i = 1;i < nazvy.Count-1 ; i++)
                {
                    context.FotoUcebny.AddRange(
                        new FotoModelUcebny
                        {
                            Id = Guid.NewGuid(),
                            Nazev = nazvy[i],
                            Cesta = cesty[i],
                            Pripona = pripony[i],
                            UcebnaID = IdUceben[i]
                        }
                        );  
                }
                context.SaveChanges();
            }
        }
    }
}
