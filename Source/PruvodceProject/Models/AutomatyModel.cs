using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class AutomatyModel
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Budova { get; set; }
        [Required] public string Patro { get; set; }
        [Required] public string Typ { get; set; }
        [Required] public bool Bagety { get; set; }
        [Required] public BudovyModel BudovaId { get; set; } //Jeden automat m��e b�t jenom na jedn� budov�, jednoduch� napojen�
    }
}