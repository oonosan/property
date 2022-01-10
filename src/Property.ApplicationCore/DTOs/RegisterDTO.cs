﻿using System.ComponentModel.DataAnnotations;

namespace Property.ApplicationCore.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
