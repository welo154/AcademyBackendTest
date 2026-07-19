namespace Academy.Playlists.Api.Models;

public class Playlist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Song> Songs { get; set; } = new List<Song>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User? User { get; set; }
}