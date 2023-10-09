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
	public class WorkersController : Controller
	{
		private readonly IWorkerService _workerService;

		public WorkersController(IWorkerService workerService)
		{
			this._workerService = workerService;
		}


		// GET: api/workers
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			IEnumerable<WorkerModel> workers = await _workerService.GetAllAsync();

			if (workers is null)
			{
				return NotFound();
			}

			return View(workers);
		}

		//GET: api/workers/1
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			WorkerModel worker = await _workerService.GetByIdAsync(id);

			if (worker is null)
			{
				return NotFound();
			}

			return View(worker);
		}

		//GET: api/workers/filter?position=YourPositionEnum
		[HttpGet]
		[Route("filter")]
		public async Task<IActionResult> GetByRollAsync([FromQuery] PositionEnum position)
		{
			IEnumerable<WorkerModel> workers = await _workerService.GetWorkersByPositionAsync(position);

			if (workers is null)
			{
				return NotFound();
			}

			return View(workers);
		}

		// POST: api/workers
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] WorkerRegistrationModel worker)
		{
			await _workerService.AddAsync(worker);

			return View("Added");
		}

		// PUT: api/workers/1
		// PATCH: api/workers/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] WorkerModel updateWorker)
		{
			if (id != updateWorker.WorkerId)
			{
				return BadRequest();
			}

			await _workerService.UpdateAsync(updateWorker);

			return View("Updated");
		}

		// DELETE: api/workers/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _workerService.DeleteByIdAsync(id);

			return View("Deleted");
		}
	}
}
