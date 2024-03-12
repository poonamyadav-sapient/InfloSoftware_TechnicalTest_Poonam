// User.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace ExtentApplication_UserManagement.Components.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
