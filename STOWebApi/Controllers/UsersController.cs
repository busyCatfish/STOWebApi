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
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			this._userService = userService;
		}


		// GET: api/users
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<UserModel>>> GetAllAsync()
		//{
		//	IEnumerable<UserModel> users = await _userService.GetAllAsync();

		//	if (users is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(users);
		//}

		//GET: api/users/1
		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<UserRegistrationModel>> GetById(int id)
		{
			UserRegistrationModel user = await _userService.GetByIdAsync(id);

			if (user is null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		//GET: api/users/filter?role=YourRoleEnumValue
		//[HttpGet]
		//[Route("filter")]
		//public async Task<ActionResult<IEnumerable<UserModel>>> GetByRollAsync([FromQuery] RoleEnum role)
		//{
		//	IEnumerable<UserModel> users = await _userService.GetUsersByRollAsync(role);

		//	if (users is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(users);
		//}

		//GET: api/users/filter?role=YourRoleEnumValue
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserModel>>> GetByRollAsync([FromQuery] RoleEnum? role)
		{
			IEnumerable<UserModel> users = null;
			if(role == null)
			{
				users = await _userService.GetAllAsync();
			}
			else users = await _userService.GetUsersByRollAsync((RoleEnum)role);

			if (users is null)
			{
				return NotFound();
			}

			return Ok(users);
		}

		// POST: api/users
		[HttpPost]
		public async Task<ActionResult> Add([FromBody] UserRegistrationModel user)
		{
			await _userService.AddAsync(user);

			return Ok("Added");
		}

		// PUT: api/users/1
		// PATCH: api/users/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] UserRegistrationModel updateUser)
		{
			await _userService.UpdateAsync(updateUser, id);

			return Ok("Updated");
		}

		// DELETE: api/users/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _userService.DeleteByIdAsync(id);

			return Ok("Deleted");
		}
	}
}
