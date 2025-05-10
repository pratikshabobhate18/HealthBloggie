﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HealthBloggie.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Password has to be atleast 6 character")]
        public string Password { get; set; }
    }
}
