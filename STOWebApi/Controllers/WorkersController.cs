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
	public class WorkersController : ControllerBase
	{
		private readonly IWorkerService _workerService;

		public WorkersController(IWorkerService workerService)
		{
			this._workerService = workerService;
		}


		// GET: api/workers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<WorkerModel>>> GetAllAsync()
		{
			IEnumerable<WorkerModel> workers = await _workerService.GetAllAsync();

			if (workers is null)
			{
				return NotFound();
			}

			return Ok(workers);
		}

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
		[HttpGet]
		[Route("filter")]
		public async Task<ActionResult<IEnumerable<WorkerModel>>> GetByRollAsync([FromQuery] PositionEnum position)
		{
			IEnumerable<WorkerModel> workers = await _workerService.GetWorkersByPositionAsync(position);

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
		public async Task<ActionResult> Delete(int id)
		{
			await _workerService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
