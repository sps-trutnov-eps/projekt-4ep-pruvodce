using System;
using System.ComponentModel.DataAnnotations;

namespace PruvodceProject.Models
{
    public class Ucebna
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string stanice { get; set; } = String.Empty;
        [Required]
        public string autobus { get; set; } = String.Empty;
        [Required]
        public string cas { get; set; } = String.Empty;

    }
}