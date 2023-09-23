using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class AutomatyModel
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string budova { get; set; }
        [Required]
        public string patro { get; set; }
        [Required]
        public string typ { get; set; }
        [Required]
        public bool bagety { get; set; }
    }
}