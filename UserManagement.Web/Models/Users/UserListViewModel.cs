using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage ="Forename is required.")]
    public string? Forename { get; set; }
    [Required(ErrorMessage = "Surname is required.")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Email Address is required.")]
    [EmailAddress(ErrorMessage ="Invalid email address.")]
    public string? Email { get; set; }
    [Required(ErrorMessage ="Date of birth is required")]
    [DataType(DataType.DateTime)]
    public DateTime DateofBirth { get; set; } // New Property for date of Birth of User
    public bool IsActive { get; set; }
}
