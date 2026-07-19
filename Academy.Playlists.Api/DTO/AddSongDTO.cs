using System.ComponentModel.DataAnnotations;

namespace Academy.Playlists.Api.DTO;

public class AddSongDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Artist { get; set; } = string.Empty;

    [Range(1, 86400)]
    public int? DurationInSeconds { get; set; }
}