using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class BudovyModel
    {
        [Key]
        public Guid IdBudovy { get; set; }  //pro jistotu jsem to kvùli vazbám pøejmenoval V.K.
        [Required]
        public string adresa { get; set; }
        [Required]
        public string kodoveOznaceni { get; set; }

        public virtual ICollection<PhotoModel> fotky { get; set; } //vazba na PhotoModel jako 1:N (1 budova : N fotkám) V.K.
        //Napojení N:1 mezi budovou a uèebmou (Jedna uèebna je na jedné budovì, na jedné budovì je nìkolik uèeben, tj. bude to list uèeben, musí se správnì napojit
        //Automaty N:1 jeden automat mùže být jenom na jedné budovì, ale na jedné budovì jich mùže být nìkolik (viz H59)
    }
}
