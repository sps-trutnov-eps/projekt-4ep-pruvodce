using System;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class ZastavkaModel
    {
        [Key]
        public Guid Id { get; set; }
        public JizdniRadModel? JizdniRad { get; set; }
        [Required]
        public string Stanice { get; set; } = String.Empty;
        [Required]
        public string Autobus { get; set; } = String.Empty;
        [Required]
        public string Cas { get; set; } = String.Empty;

    }
}