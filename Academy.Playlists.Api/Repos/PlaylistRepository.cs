using Academy.Playlists.Api.Data;
using Academy.Playlists.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Playlists.Api.Repos;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _dbContext;

    public PlaylistRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Playlist> CreatePlaylistAsync(Playlist playlist)
    {
        _dbContext.Playlists.Add(playlist);

        await _dbContext.SaveChangesAsync();

        return playlist;
    }

    public async Task<List<Playlist>> GetUserPlaylistsAsync(int userId)
    {
        return await _dbContext.Playlists
            .AsNoTracking()
            .Where(playlist => playlist.UserId == userId)
            .Include(playlist => playlist.Songs)
            .ToListAsync();
    }

    public async Task<Playlist?> GetPlaylistByIdAsync(int playlistId)
    {
        return await _dbContext.Playlists
            .Include(playlist => playlist.Songs)
            .FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
    }

    public async Task<Song> AddSongAsync(Song song)
    {
        _dbContext.Songs.Add(song);

        await _dbContext.SaveChangesAsync();

        return song;
    }
    public async Task<bool> DeletePlaylistAsync(int playlistId)
    {
        var playlist = await _dbContext.Playlists
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (playlist is null)
        {
            return false;
        }

        _dbContext.Playlists.Remove(playlist);

        await _dbContext.SaveChangesAsync();

        return true;
    }
    public async Task<bool> DeleteSongAsync(
    int playlistId,
    int songId)
    {
        var song = await _dbContext.Songs
            .FirstOrDefaultAsync(song =>
                song.Id == songId &&
                song.PlaylistId == playlistId);

        if (song is null)
        {
            return false;
        }

        _dbContext.Songs.Remove(song);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}

