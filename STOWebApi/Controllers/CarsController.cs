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
	public class CarsController : ControllerBase
	{
		private readonly ICarService _carService;

		public CarsController(ICarService carService)
		{
			this._carService = carService;
		}


		// GET: api/cars
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CarModel>>> GetAllAsync()
		{
			IEnumerable<CarModel> cars = await _carService.GetAllAsync();

			if (cars is null)
			{
				return NotFound();
			}

			return Ok(cars);
		}

		//GET: api/cars/1HGCM82633A123456
		[HttpGet]
		[Route("{vincode}")]
		public async Task<ActionResult<CarModel>> GetById(string vincode)
		{
			CarModel car = await _carService.GetByIdAsync(vincode);

			if (car is null)
			{
				return NotFound();
			}

			return Ok(car);
		}

		//GET: api/cars/filter?userId=UserId
		[HttpGet]
		[Route("filter")]
		public async Task<ActionResult<IEnumerable<CarModel>>> GetByRollAsync([FromQuery] int userId)
		{
			IEnumerable<CarModel> cars = await _carService.GetCarsByUserIdAsync(userId);

			if (cars is null)
			{
				return NotFound();
			}

			return Ok(cars);
		}

		// POST: api/cars
		[HttpPost]
		public async Task<ActionResult> Add([FromBody] CarRegistrationModel car)
		{
			await _carService.AddAsync(car);

			return Ok();
		}

		// PUT: api/cars/1HGCM82633A123456
		// PATCH: api/cars/1HGCM82633A123456
		[HttpPatch]
		[HttpPut]
		[Route("{vincode}")]
		public async Task<ActionResult> Update(string vincode, [FromBody] CarRegistrationModel updateCar)
		{
			if (vincode != updateCar.Vincode)
			{
				return BadRequest();
			}

			await _carService.UpdateAsync(updateCar, vincode);

			return Ok();
		}

		// DELETE: api/cars/1HGCM82633A123456
		[HttpDelete]
		[Route("{vincode}")]
		public async Task<ActionResult> Delete(string vincode)
		{
			await _carService.DeleteByIdAsync(vincode);

			return Ok();
		}
	}
}
