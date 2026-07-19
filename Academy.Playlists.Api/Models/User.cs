namespace Academy.Playlists.Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}