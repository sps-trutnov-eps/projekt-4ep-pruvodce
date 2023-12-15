using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string OdkazNaMenu { get; set; }
        [Required]
        public string Popis { get; set; }
    }
}