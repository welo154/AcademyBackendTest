using Academy.Playlists.Api.Data;
using Academy.Playlists.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Playlists.Api.Repos;

public class UserRepository : IUserRepository
{
	private readonly AppDbContext _dbContext;

	public UserRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<User> CreateAsync(User user)
	{
		_dbContext.Users.Add(user);

		await _dbContext.SaveChangesAsync();

		return user;
	}

	public async Task<bool> ExistsAsync(int userId)
	{
		return await _dbContext.Users
			.AnyAsync(user => user.Id == userId);
	}
}