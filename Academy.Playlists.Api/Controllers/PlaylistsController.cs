using Academy.Playlists.Api.DTO;
using Academy.Playlists.Api.Models;
using Academy.Playlists.Api.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Playlists.Api.Controllers;

[ApiController]
[Route("api/playlists")]
[Produces("application/json")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IUserRepository _userRepository;

    public PlaylistsController(
        IPlaylistRepository playlistRepository,
        IUserRepository userRepository)
    {
        _playlistRepository = playlistRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    [ProducesResponseType(
    typeof(PlaylistResponseDto),
    StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlaylistResponseDto>> CreatePlaylist(
    CreatePlaylistDto request)
    {
        var userExists =
            await _userRepository.ExistsAsync(request.UserId);

        if (!userExists)
        {
            return NotFound(new
            {
                message = $"User with ID {request.UserId} was not found."
            });
        }

        var playlist = new Playlist
        {
            Name = request.Name.Trim(),
            UserId = request.UserId
        };

        var createdPlaylist =
            await _playlistRepository.CreatePlaylistAsync(playlist);

        var response = MapToResponse(createdPlaylist);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<PlaylistResponseDto>>>
        GetUserPlaylists(int userId)
    {
        var playlists =
            await _playlistRepository.GetUserPlaylistsAsync(userId);

        var response = playlists
            .Select(MapToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpPost("{playlistId:int}/songs")]
    public async Task<ActionResult<SongResponseDto>> AddSong(
        int playlistId,
        AddSongDto request)
    {
        var playlist =
            await _playlistRepository.GetPlaylistByIdAsync(playlistId);

        if (playlist is null)
        {
            return NotFound(new
            {
                message = $"Playlist with ID {playlistId} was not found."
            });
        }
    
        var song = new Song
        {
            Title = request.Title.Trim(),
            Artist = request.Artist.Trim(),
            DurationInSeconds = request.DurationInSeconds,
            PlaylistId = playlistId
        };

        var createdSong =
            await _playlistRepository.AddSongAsync(song);

        var response = new SongResponseDto
        {
            Id = createdSong.Id,
            Title = createdSong.Title,
            Artist = createdSong.Artist,
            DurationInSeconds = createdSong.DurationInSeconds
        };

        return Created(
            $"/api/playlists/{playlistId}/songs/{createdSong.Id}",
            response);
    }
    [HttpDelete("{playlistId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlaylist(int playlistId)
    {
        var deleted =
            await _playlistRepository.DeletePlaylistAsync(playlistId);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Playlist with ID {playlistId} was not found."
            });
        }

        return NoContent();
    }
    [HttpDelete("{playlistId:int}/songs/{songId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSong(
    int playlistId,
    int songId)
    {
        var deleted =
            await _playlistRepository.DeleteSongAsync(
                playlistId,
                songId);

        if (!deleted)
        {
            return NotFound(new
            {
                message =
                    $"Song with ID {songId} was not found in playlist {playlistId}."
            });
        }

        return NoContent();
    }
    private static PlaylistResponseDto MapToResponse(
        Playlist playlist)
    {
        return new PlaylistResponseDto
        {
            Id = playlist.Id,
            Name = playlist.Name,
            UserId = playlist.UserId,
            CreatedAt = playlist.CreatedAt,

            Songs = playlist.Songs
                .Select(song => new SongResponseDto
                {
                    Id = song.Id,
                    Title = song.Title,
                    Artist = song.Artist,
                    DurationInSeconds = song.DurationInSeconds
                })
                .ToList()
        };
    }
}