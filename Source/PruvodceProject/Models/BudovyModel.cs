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
        //Napojení N:1 mezi budovou a uèebmou (Jedna uèebna je na jedné budovì, na jedné budovì je nìkolik uèeben, tj. bude to list uèeben, musí se správnì napojit
        //Automaty N:1 jeden automat mùže být jenom na jedné budovì, ale na jedné budovì jich mùže být nìkolik (viz H59)
    }
}
