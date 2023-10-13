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
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			this._orderService = orderService;
		}


		// GET: api/orders
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderModel>>> GetAllAsync()
		{
			IEnumerable<OrderModel> orders = await _orderService.GetAllAsync();

			if (orders is null)
			{
				return NotFound();
			}

			return Ok(orders);
		}

		//GET: api/orders/1
		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<OrderRegistrationModel>> GetById(int id)
		{
			OrderRegistrationModel order = await _orderService.GetByIdAsync(id);

			if (order is null)
			{
				return NotFound();
			}

			return Ok(order);
		}

		//GET: api/orders/filter?state=YourStateEnumValue&сarVincode=YourCarVincode&userId=YourUserId&startDate=YourStartDate&finisheDate=YourFinisheDate
		[HttpGet]
		[Route("filter")]
		public async Task<ActionResult<IEnumerable<OrderModel>>> GetByRollAsync([FromQuery] OrderFilterSearchModel filter)
		{
			IEnumerable<OrderModel> orders = await _orderService.GetOrdersByFilterAsync(filter);

			if (orders is null)
			{
				return NotFound();
			}

			return Ok(orders);
		}

		// POST: api/orders
		[HttpPost]
		public async Task<ActionResult> Add([FromBody] OrderRegistrationModel order)
		{
			await _orderService.AddAsync(order);

			return Ok();
		}

		// PUT: api/orders/1
		// PATCH: api/orders/1
		[HttpPatch]
		[HttpPut]
		[Route("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] OrderRegistrationModel updateOrder)
		{
			//if (id != updateOrder.OrderId)
			//{
			//	return BadRequest();
			//}

			await _orderService.UpdateAsync(updateOrder, id);

			return Ok();
		}

		// DELETE: api/orders/1
		[HttpDelete]
		[Route("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _orderService.DeleteByIdAsync(id);

			return Ok();
		}
	}
}
