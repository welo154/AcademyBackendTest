namespace Academy.Playlists.Api.DTO;

public class SongResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Artist { get; set; } = string.Empty;

    public int? DurationInSeconds { get; set; }
}