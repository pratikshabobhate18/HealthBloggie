using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace HealthBloggie.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required] 
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage="Password must be 6 character")]
        public string Password { get; set; }

        [Required]
        public string? ReturnUrl { get; set; }
    }
}
