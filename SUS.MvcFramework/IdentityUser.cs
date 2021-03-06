using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SUS.MvcFramework
{
    public class IdentityUser<T>
    {
        public T Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IdentityRole Role { get; set; }
    }
}
