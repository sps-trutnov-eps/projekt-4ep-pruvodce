using Microsoft.EntityFrameworkCore;
using PruvodceProject.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections.Generic;

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
        [Key] public Guid ID { get; set; }
        [Required] public string Nazev { get; set; }
        [Required] public string Adresa { get; set; }
        public string OdkazNaMenu { get; set; }
        [Required] public string Popis { get; set; }
        public string Typ {  get; set; }
    }
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {   
            List<string> nazvy = new List<string>();
            List<string> adresy = new List<string>();
            List<string> odkazy = new List<string>();
            List<string> popisy = new List<string>();
            List<string> typy = new List<string>();
            using (var soubor = new StreamReader("../../textdata/pokus.csv"))
            {
                while (!soubor.EndOfStream)
                {
                    var radek = soubor.ReadLine();
                    var hodnoty = radek.Split(";");

                    nazvy.Add(hodnoty[0]);
                    adresy.Add(hodnoty[1]);
                    odkazy.Add(hodnoty[2]);
                    popisy.Add(hodnoty[3]);
                    typy.Add(hodnoty[4]);
                }
            }

            using(var context = new PruvodceData(serviceProvider.GetRequiredService<DbContextOptions<PruvodceData>>()))
            {
                if(context.StravovaciZarizeni.Any())
                    return;
                }
                for (int i = 0;i < nazvy.Count(); i++)
                {
                    context.StravovaciZarizeni.AddRange(
                        new StravovaciZarizeniModel { 
                            ID = Guid.NewGuid(),
                            Nazev = nazvy[i],
                            Adresa = adresy[i],
                            OdkazNaMenu = odkazy[i],
                            Popis = popisy[i],
                            Typ = typy[i]
                        }
                    );
                }
                
                context.SaveChanges();
            }
        }
    }
}