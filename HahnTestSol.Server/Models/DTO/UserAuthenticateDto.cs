using System.ComponentModel.DataAnnotations;

namespace HahnTestSol.Server.Models.DTO
{
    public class UserAuthenticateDto
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
