using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using System.Data;
using System.Reflection;

namespace STOWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Administrator,Manager")]
	public class MastersController : ControllerBase
	{
		private readonly IMasterService _masterService;

		public MastersController(IMasterService masterService)
		{
			this._masterService = masterService;
		}


		// GET: api/masters
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<MasterModel>>> GetAllAsync()
		//{
		//	IEnumerable<MasterModel> masters = await _masterService.GetAllAsync();

		//	if (masters is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(masters);
		//}

		//GET: api/masters/1
		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<MasterRegistrationModel>> GetById(int id)
		{
			MasterRegistrationModel master = await _masterService.GetByIdAsync(id);

			if (master is null)
			{
				return NotFound();
			}

			return Ok(master);
		}

		//GET: api/masters/filter?type=YourMasterTypeEnum
		//[HttpGet]
		//[Route("filter")]
		//public async Task<ActionResult<IEnumerable<MasterModel>>> GetByMasterTypeAsync([FromQuery] MasterTypeEnum type)
		//{
		//	IEnumerable<MasterModel> masters = await _masterService.GetMastersByTypeAsync(type);

		//	if (masters is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(masters);
		//}
		//GET: api/masters/filter?type=YourMasterTypeEnum
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MasterModel>>> GetByMasterTypeAsync([FromQuery] MasterTypeEnum? type)
		{
			IEnumerable<MasterModel> masters = null;
			if (type == null)
			{
				masters = await _masterService.GetAllAsync();
			}
			else masters = await _masterService.GetMastersByTypeAsync((MasterTypeEnum)type);

			if (masters is null)
			{
				return NotFound();
			}

			return Ok(masters);
		}

		// POST: api/masters
		[HttpPost]
		public async Task<ActionResult> Add([FromBody] MasterRegistrationModel master)
		{
			await _masterService.AddAsync(master);

			return Ok();
		}

		// PUT: api/masters/1
		// PATCH: api/masters/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] MasterRegistrationModel updateMaster)
		{
			//if (id != updateMaster.MasterId)
			//{
			//	return BadRequest();
			//}

			await _masterService.UpdateAsync(updateMaster, id);

			return Ok();
		}

		// DELETE: api/masters/1
		[HttpDelete]
		[Route("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> Delete(int id)
		{
			await _masterService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
