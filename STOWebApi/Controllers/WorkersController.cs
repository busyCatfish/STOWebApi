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
	public class WorkersController : ControllerBase
	{
		private readonly IWorkerService _workerService;

		public WorkersController(IWorkerService workerService)
		{
			this._workerService = workerService;
		}


		// GET: api/workers
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<WorkerModel>>> GetAllAsync()
		//{
		//	IEnumerable<WorkerModel> workers = await _workerService.GetAllAsync();

		//	if (workers is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(workers);
		//}

		//GET: api/workers/1
		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<WorkerRegistrationModel>> GetById(int id)
		{
			WorkerRegistrationModel worker = await _workerService.GetByIdAsync(id);

			if (worker is null)
			{
				return NotFound();
			}

			return Ok(worker);
		}

		//GET: api/workers/filter?position=YourPositionEnum
		//[HttpGet]
		//[Route("filter")]
		//public async Task<ActionResult<IEnumerable<WorkerModel>>> GetByPositionAsync([FromQuery] PositionEnum position)
		//{
		//	IEnumerable<WorkerModel> workers = await _workerService.GetWorkersByPositionAsync(position);

		//	if (workers is null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(workers);
		//}

		//GET: api/workers/filter?position=YourPositionEnum
		[HttpGet]
		public async Task<ActionResult<IEnumerable<WorkerModel>>> GetByPositionAsync([FromQuery] PositionEnum? position)
		{
			IEnumerable<WorkerModel> workers = null;
			if(position == null)
			{
				workers = await _workerService.GetAllAsync();
			}
			else workers = await _workerService.GetWorkersByPositionAsync((PositionEnum)position);

			if (workers is null)
			{
				return NotFound();
			}

			return Ok(workers);
		}

		// POST: api/workers
		[HttpPost]
		public async Task<ActionResult> Add([FromBody] WorkerRegistrationModel worker)
		{
			await _workerService.AddAsync(worker);

			return Ok();
		}

		// PUT: api/workers/1
		// PATCH: api/workers/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] WorkerRegistrationModel updateWorker)
		{
			//if (id != updateWorker.WorkerId)
			//{
			//	return BadRequest();
			//}

			await _workerService.UpdateAsync(updateWorker, id);

			return Ok();
		}

		// DELETE: api/workers/1
		[HttpDelete]
		[Route("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult> Delete(int id)
		{
			await _workerService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
