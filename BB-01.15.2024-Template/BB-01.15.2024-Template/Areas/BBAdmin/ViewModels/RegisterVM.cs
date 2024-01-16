using BB_01._15._2024_Template.Models;
using System.ComponentModel.DataAnnotations;

namespace BB_01._15._2024_Template.Areas.BBAdmin.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        public string Password { get; set; }

    }
}
