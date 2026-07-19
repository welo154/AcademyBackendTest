using Academy.Playlists.Api.Models;

namespace Academy.Playlists.Api.Repos;

public interface IPlaylistRepository
{
    Task<Playlist> CreatePlaylistAsync(Playlist playlist);

    Task<List<Playlist>> GetUserPlaylistsAsync(int userId);

    Task<Playlist?> GetPlaylistByIdAsync(int playlistId);

    Task<Song> AddSongAsync(Song song);
    Task<bool> DeletePlaylistAsync(int playlistId);

    Task<bool> DeleteSongAsync(int playlistId, int songId);
}