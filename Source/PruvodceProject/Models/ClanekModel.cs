using System.ComponentModel.DataAnnotations;
namespace PruvodceProject.Models
{
    public class ClanekModel
    {
        [Key] public Guid Id { get; set; }
        [Required] public Guid AutorId {  get; set; }
        [Required] public string NadpisClanku { get; set; }
        [Required] public string ObsahClanku { get; set; }
        [Required] public DateTime DatumVytvoreni { get; set; }
    }
}
