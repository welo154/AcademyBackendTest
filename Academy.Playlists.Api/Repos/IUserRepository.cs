using Academy.Playlists.Api.Models;

namespace Academy.Playlists.Api.Repos;

public interface IUserRepository
{
	Task<User> CreateAsync(User user);

	Task<bool> ExistsAsync(int userId);
}