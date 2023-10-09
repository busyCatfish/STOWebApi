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
	public class MastersController : Controller
	{
		private readonly IMasterService _masterService;

		public MastersController(IMasterService masterService)
		{
			this._masterService = masterService;
		}


		// GET: api/masters
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			IEnumerable<MasterModel> masters = await _masterService.GetAllAsync();

			if (masters is null)
			{
				return NotFound();
			}

			return View(masters);
		}

		//GET: api/masters/1
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			MasterModel master = await _masterService.GetByIdAsync(id);

			if (master is null)
			{
				return NotFound();
			}

			return View(master);
		}

		//GET: api/masters/filter?type=YourMasterTypeEnum
		[HttpGet]
		[Route("filter")]
		public async Task<IActionResult> GetByRollAsync([FromQuery] MasterTypeEnum type)
		{
			IEnumerable<MasterModel> masters = await _masterService.GetMastersByTypeAsync(type);

			if (masters is null)
			{
				return NotFound();
			}

			return View(masters);
		}

		// POST: api/masters
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] MasterRegistrationModel master)
		{
			await _masterService.AddAsync(master);

			return View("Added");
		}

		// PUT: api/masters/1
		// PATCH: api/masters/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] MasterModel updateMaster)
		{
			if (id != updateMaster.MasterId)
			{
				return BadRequest();
			}

			await _masterService.UpdateAsync(updateMaster);

			return View("Updated");
		}

		// DELETE: api/masters/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _masterService.DeleteByIdAsync(id);

			return View("Deleted");
		}
	}
}
