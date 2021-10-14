using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Models
{
    [Table("Platform")]
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
    }
}
