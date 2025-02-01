using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Account;

public class LoginDTO
{
  [Required(ErrorMessage = "Username required")]
  public string? Username { get; set; }
  [Required(ErrorMessage = "Password required")]
  public string? Password { get; set; }
}
