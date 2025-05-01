using System.ComponentModel.DataAnnotations;

namespace ValueMateApi.Models;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password  { get; set; }
}