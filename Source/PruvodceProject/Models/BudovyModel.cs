using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class BudovyModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string adresa { get; set; }
        [Required]
        public string kodoveOznaceni { get; set; }
        //Napojen� N:1 mezi budovou a u�ebmou (Jedna u�ebna je na jedn� budov�, na jedn� budov� je n�kolik u�eben, tj. bude to list u�eben, mus� se spr�vn� napojit
        //Automaty N:1 jeden automat m��e b�t jenom na jedn� budov�, ale na jedn� budov� jich m��e b�t n�kolik (viz H59)
    }
}
