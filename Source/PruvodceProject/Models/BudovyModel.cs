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
}