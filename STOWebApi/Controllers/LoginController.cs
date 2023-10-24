using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STOWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly IConfiguration config;

		public LoginController(IUserService userService, IConfiguration config)
		{
			this.userService = userService;
			this.config = config;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
		{
			var user = await userService.Authentificate(userLoginModel);

			if (user != null)
			{
				var token = this.GenerateToken(user);

				return Ok(new { token });
			}

			return NotFound("UserName or Password is not correct");
		}

		private string GenerateToken(UserModel user)
		{
			var secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

			var credentials = new SigningCredentials(secutityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(ClaimTypes.Role, user.Role),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Surname, user.Surname)
			};

			var token = new JwtSecurityToken(
				issuer: config["Jwt:Issuer"],
				audience: config["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddHours(8),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
