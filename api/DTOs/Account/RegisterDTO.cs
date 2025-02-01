using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Account;

public class RegisterDTO
{
  [Required(ErrorMessage = "Username required")]
  public string? Username { get; set; }
  [Required(ErrorMessage = "Email required")]
  [EmailAddress]
  public string? Email { get; set; }
  [Required(ErrorMessage = "Password required")]
  public string? Password { get; set; }
}
