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
	public class CarsController : Controller
	{
		private readonly ICarService _carService;

		public CarsController(ICarService carService)
		{
			this._carService = carService;
		}


		// GET: api/cars
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			IEnumerable<CarModel> cars = await _carService.GetAllAsync();

			if (cars is null)
			{
				return NotFound();
			}

			return View(cars);
		}

		//GET: api/cars/1HGCM82633A123456
		[HttpGet]
		[Route("{vincode}")]
		public async Task<IActionResult> GetById(string vincode)
		{
			CarModel car = await _carService.GetByIdAsync(vincode);

			if (car is null)
			{
				return NotFound();
			}

			return View(car);
		}

		//GET: api/cars/filter?userId=UserId
		[HttpGet]
		[Route("filter")]
		public async Task<IActionResult> GetByRollAsync([FromQuery] int userId)
		{
			IEnumerable<CarModel> cars = await _carService.GetCarsByUserIdAsync(userId);

			if (cars is null)
			{
				return NotFound();
			}

			return View(cars);
		}

		// POST: api/cars
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CarRegistrationModel car)
		{
			await _carService.AddAsync(car);

			return View("Added");
		}

		// PUT: api/cars/1HGCM82633A123456
		// PATCH: api/cars/1HGCM82633A123456
		[HttpPatch]
		[HttpPut]
		[Route("{vincode}")]
		public async Task<IActionResult> Update(string vincode, [FromBody] CarModel updateCar)
		{
			if (vincode != updateCar.Vincode)
			{
				return BadRequest();
			}

			await _carService.UpdateAsync(updateCar);

			return View("Updated");
		}

		// DELETE: api/cars/1HGCM82633A123456
		[HttpDelete]
		[Route("{vincode}")]
		public async Task<IActionResult> Delete(string vincode)
		{
			await _carService.DeleteByIdAsync(vincode);

			return View("Deleted");
		}
	}
}
