using System.ComponentModel.DataAnnotations;

namespace Academy.Playlists.Api.DTO;

public class CreateUserDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}