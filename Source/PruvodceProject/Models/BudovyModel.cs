using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class BudovyModel
    {
        [Key]
        public Guid IdBudovy { get; set; }  //pro jistotu jsem to kv�li vazb�m p�ejmenoval V.K.
        [Required]
        public string name { get; set; }
        [Required]
        public string adresa { get; set; }
        [Required]
        public string kodoveOznaceni { get; set; }
        public ICollection<UcebnaModel> Ucebny { get; set; }
        public ICollection<AutomatyModel> Automaty { get; set; }
        public ICollection<PhotoModelBudovy> fotky { get; set; }
    }
}