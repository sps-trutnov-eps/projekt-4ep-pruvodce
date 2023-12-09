using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class CrowdSourceModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string mailUzivatele { get; set; } //Email uživatele který to vytvořil
        [Required]
        public string nadpis { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string stav { get; set; } // (čeká na vyřízení/schváleno/zamítnuto/již existuje)
        [Required]
        public string odpovedAmina { get; set; } = String.Empty;
        public string existujici { get; set; } // (nepovinný, bokud je po ve Stavu již existuje bude zde GUID již existujícího reportu)  < (Napsal FireGamesCZ) Co?
    }
}