namespace Academy.Playlists.Api.DTO;

public class PlaylistResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<SongResponseDto> Songs { get; set; } = new();
}