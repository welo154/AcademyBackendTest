using Academy.Playlists.Api.DTO;
using Academy.Playlists.Api.Models;
using Academy.Playlists.Api.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Playlists.Api.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
	private readonly IUserRepository _userRepository;

	public UsersController(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	[HttpPost]
	[ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<UserResponseDto>> CreateUser(
		CreateUserDto request)
	{
		var user = new User
		{
			Name = request.Name.Trim()
		};

		var createdUser = await _userRepository.CreateAsync(user);

		var response = new UserResponseDto
		{
			Id = createdUser.Id,
			Name = createdUser.Name
		};

		return StatusCode(StatusCodes.Status201Created, response);
	}
}