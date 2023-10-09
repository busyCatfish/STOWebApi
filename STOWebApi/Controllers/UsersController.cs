using Microsoft.AspNetCore.Mvc;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using System.Reflection;

namespace STOWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			this._userService = userService;
		}


		// GET: api/users
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			IEnumerable<UserModel> users = await _userService.GetAllAsync();

			if (users is null)
			{
				return NotFound();
			}

			return View(users);
		}

		//GET: api/users/1
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			UserModel user = await _userService.GetByIdAsync(id);

			if (user is null)
			{
				return NotFound();
			}

			return View(user);
		}

		//GET: api/users/filter?role=YourRoleEnumValue
		[HttpGet]
		[Route("filter")]
		public async Task<IActionResult> GetByRollAsync([FromQuery] RoleEnum role)
		{
			IEnumerable<UserModel> users = await _userService.GetUsersByRollAsync(role);

			if (users is null)
			{
				return NotFound();
			}

			return View(users);
		}

		// POST: api/users
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] UserRegistrationModel user)
		{
			await _userService.AddAsync(user);

			return View("Added");
		}

		// PUT: api/users/1
		// PATCH: api/users/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UserModel updateUser)
		{
			if (id != updateUser.UserId)
			{
				return BadRequest();
			}

			await _userService.UpdateAsync(updateUser);

			return View("Updated");
		}

		// DELETE: api/users/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _userService.DeleteByIdAsync(id);

			return View("Deleted");
		}
	}
}
