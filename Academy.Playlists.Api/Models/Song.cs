namespace Academy.Playlists.Api.Models;

public class Song
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Artist { get; set; }

    public int? DurationInSeconds { get; set; }

    public int PlaylistId { get; set; }

    public Playlist? Playlist { get; set; }
}