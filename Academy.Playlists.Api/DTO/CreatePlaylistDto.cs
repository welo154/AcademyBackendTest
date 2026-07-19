using System.ComponentModel.DataAnnotations;

namespace Academy.Playlists.Api.DTO;

public class CreatePlaylistDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}