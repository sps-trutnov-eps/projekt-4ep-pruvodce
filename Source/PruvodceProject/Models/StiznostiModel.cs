using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class StiznostiModel
    {
        [Key] public Guid Id { get; set; }
        [Required] public string UzivatelMail { get; set; } //Email uživatele který to vytvořil
        [Required] public string Nadpis { get; set; }
        [Required] public string Text { get; set; }
        [Required] public string Stav { get; set; } // (čeká na vyřízení/schváleno/zamítnuto/již existuje)
        [Required] public string AdminOdpoved { get; set; } = String.Empty;
        public string Existujici { get; set; } // (nepovinný, bokud je po ve Stavu již existuje bude zde GUID již existujícího reportu)  < (Napsal FireGamesCZ) Co?
    }
}