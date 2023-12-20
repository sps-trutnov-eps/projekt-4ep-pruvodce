using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    /// <summary>
    /// Datový model pro učebnu.
    /// Primárním klíčem je Id, které je typu GUID
    /// </summary>
    public class UcebnaModel
    {
        [Key] public Guid Id { get; set; }  
        [Required] public string Nazev { get; set; }  
        [Required] public string Patro { get; set; } = String.Empty;
        [Required] public string Druh { get; set; } = String.Empty;
        public string Poddruh { get; set; } = String.Empty;
        public BudovyModel Budova { get; set; }
        [Required] public Guid BudovaId { get; set; }
        public ICollection<FotoModelUcebny> Fotky { get; set; } //vazba na foto 1:N
    }
}