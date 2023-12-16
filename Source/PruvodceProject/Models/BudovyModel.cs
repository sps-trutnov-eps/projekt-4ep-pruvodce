using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class BudovyModel
    {
        [Key] public Guid IdBudovy { get; set; }  //pro jistotu jsem to kv�li vazb�m p�ejmenoval V.K.
        [Required] public string Adresa { get; set; }
        [Required] public string KodoveOznaceni { get; set; }
        public ICollection<UcebnaModel> Ucebny { get; set; }
        public ICollection<AutomatyModel> Automaty { get; set; }
        public ICollection<FotoModelBudovy> Fotky { get; set; } //vazba na PhotoModel jako 1:N (1 budova : N fotk�m) V.K.
        //Napojen� N:1 mezi budovou a u�ebmou (Jedna u�ebna je na jedn� budov�, na jedn� budov� je n�kolik u�eben, tj. bude to list u�eben, mus� se spr�vn� napojit
        //Automaty N:1 jeden automat m��e b�t jenom na jedn� budov�, ale na jedn� budov� jich m��e b�t n�kolik (viz H59)
    }
}
