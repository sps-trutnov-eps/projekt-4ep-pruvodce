using System.ComponentModel.DataAnnotations;
namespace PruvodceProject.Models
{
    public class ObchodModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Nazev { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        public string ObsahClanku { get; set; }
        [Required]
        public DateTime DatumVytvoreni { get; set; }
    }
}