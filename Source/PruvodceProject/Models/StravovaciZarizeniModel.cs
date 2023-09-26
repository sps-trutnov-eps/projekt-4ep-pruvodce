using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class StravovaciZarizeniModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string nazev { get; set; }
        [Required]
        public string adresa { get; set; }
        [Required]
        public string odkazNaMenu { get; set; }
        [Required]
        public string popis { get; set; }
    }
}
