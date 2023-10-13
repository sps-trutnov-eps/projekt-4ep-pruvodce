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
    }
}
