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
	public class OrdersController : Controller
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			this._orderService = orderService;
		}


		// GET: api/orders
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			IEnumerable<OrderModel> orders = await _orderService.GetAllAsync();

			if (orders is null)
			{
				return NotFound();
			}

			return View(orders);
		}

		//GET: api/orders/1
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			OrderModel order = await _orderService.GetByIdAsync(id);

			if (order is null)
			{
				return NotFound();
			}

			return View(order);
		}

		//GET: api/orders/filter?state=YourStateEnumValue&сarVincode=YourCarVincode&userId=YourUserId&startDate=YourStartDate&finisheDate=YourFinisheDate
		[HttpGet]
		[Route("filter")]
		public async Task<IActionResult> GetByRollAsync([FromQuery] OrderFilterSearchModel filter)
		{
			IEnumerable<OrderModel> orders = await _orderService.GetOrdersByFilterAsync(filter);

			if (orders is null)
			{
				return NotFound();
			}

			return View(orders);
		}

		// POST: api/orders
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] OrderRegistrationModel order)
		{
			await _orderService.AddAsync(order);

			return View("Added");
		}

		// PUT: api/orders/1
		// PATCH: api/orders/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] OrderModel updateOrder)
		{
			if (id != updateOrder.OrderId)
			{
				return BadRequest();
			}

			await _orderService.UpdateAsync(updateOrder);

			return View("Updated");
		}

		// DELETE: api/orders/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _orderService.DeleteByIdAsync(id);

			return View("Deleted");
		}
	}
}
