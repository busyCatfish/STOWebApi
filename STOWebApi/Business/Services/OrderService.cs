using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;
using System.Data;
using System.Reflection;

namespace STOWebApi.Business.Services
{
	public class OrderService : IOrderService
	{
		public OrderService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task AddAsync(OrderRegistrationModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("Order cannot be null!");
			}

			Order order = Mapper.Map<Order>(model);

			order.UserId = await this.GetUserIdByUserName(model.UserName);

			order.Masters = await this.GetMastersByTheirId(model.MastersId);

			this.CheckOrderModel(order);

			await Object.OrderRepository.AddAsync(order);

			await Object.SaveAsync();
		}

		public async Task DeleteByIdAsync(int modelId)
		{
			if(modelId <= 0)
			{
				throw new STOSystemException("OrderId should be more than 0!");
			}

			await Object.OrderRepository.DeleteByIdAsync(modelId);

			await Object.SaveAsync();
		}

		public async Task<IEnumerable<OrderModel>> GetAllAsync()
		{
			var allOrders = await Object.OrderRepository.GetAllWithDetailsAsync();

			var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(allOrders);

			return ordersModel;
		}

		public async Task<OrderRegistrationModel> GetByIdAsync(int modelId)
		{
			if (modelId <= 0)
			{
				throw new STOSystemException("OrderId should be more than 0!");
			}

			var order = await Object.OrderRepository.GetByIdWithDetailsAsync(modelId);

			var orderModel = Mapper.Map<OrderRegistrationModel>(order);

			return orderModel;
		}

		public async Task<IEnumerable<OrderModel>> GetOrdersByFilterAsync(OrderFilterSearchModel filter)
		{
			this.CheckPropertyOfOrderFilterSearchModel(filter);

			var orders = await Object.OrderRepository.GetAllWithDetailsAsync();

			if (filter.CarVincode != null)
			{
				orders = orders.Where(o => o.CarVincode == filter.CarVincode);
			}

			if(filter.UserId != null)
			{
				orders = orders.Where(o => o.UserId == filter.UserId);
			}

			if (filter.State != null)
			{
				var filterState = StaticTools.GetStateEnumByStateString(filter.State);
				orders = orders.Where(o => o.State == filterState);
			}

			if (filter.StartDate != null)
			{
				if(filter.FinisheDate != null)
					orders = orders.Where(o => o.StartDate >= filter.StartDate && o.StartDate <= filter.FinisheDate);
				else orders = orders.Where(o => o.StartDate >= filter.StartDate);
			}

			var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(orders);

			return ordersModel;
		}

		//public async Task<IEnumerable<OrderModel>> GetOrdersByPeriodOfTimeAsync(DateTime start, DateTime finish)
		//{
		//	if (finish < start)
		//	{
		//		throw new STOSystemException("Finishe date cannot be less than start date");
		//	}

		//	var allOrders = await Object.OrderRepository.GetOrdersByPeriodOfTimeAsync(start, finish);

		//	var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(allOrders);

		//	return ordersModel;
		//}

		//public async Task<IEnumerable<OrderModel>> GetOrdersByStateAsync(StateEnum state)
		//{
		//	var allOrders = await Object.OrderRepository.GetOrdersByStateAsync(state);

		//	var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(allOrders);

		//	return ordersModel;
		//}

		//public async Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId)
		//{
		//	if (userId <= 0)
		//	{
		//		throw new STOSystemException("UserId should be more than 0!");
		//	}

		//	var allOrders = await Object.OrderRepository.GetOrdersByUserIdAsync(userId);

		//	var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(allOrders);

		//	return ordersModel;
		//}

		//public async Task<IEnumerable<OrderModel>> GetOrdersByVincodeAsync(string vincode)
		//{
		//	if (string.IsNullOrEmpty(vincode))
		//	{
		//		throw new STOSystemException("CarVincode cannot be null or empty");
		//	}

		//	var allOrders = await Object.OrderRepository.GetOrdersByVincodeAsync(vincode);

		//	var ordersModel = Mapper.Map<IEnumerable<OrderModel>>(allOrders);

		//	return ordersModel;
		//}

		public async Task UpdateAsync(OrderRegistrationModel model, int modelId)
		{
			if (model == null)
			{
				throw new STOSystemException("Order cannot be null!");
			}

			Order order = Mapper.Map<Order>(model);

			order.Id = modelId;

			order.UserId = await this.GetUserIdByUserName(model.UserName);

			CheckOrderModel(order);

			await Object.OrderMasterRepository.DeleteByOrderIdAsync(modelId);

			order.Masters = await this.GetMastersByTheirId(model.MastersId);

			await Object.OrderRepository.UpdateAsync(order);

			await Object.SaveAsync();
		}

		private async Task<int?> GetUserIdByUserName(string userName)
		{
			var user = await Object.UserRepository.GetUserByUserNameAsync(userName);

			if (user == null)
			{
				return null;
				//throw new STOSystemException($"Не існує користувача з таким username: {userName}");
			}

			return user.Id;
		}

		private async Task<IList<Master>> GetMastersByTheirId(IEnumerable<int> mastersId)
		{
			var masters = await Object.MasterRepository.GetByIdsAsync(mastersId);

			if (masters == null)
			{
				return new List<Master>();
				//throw new STOSystemException("Ви не вибрали жодного майстра для цієї роботи!");
			}

			return masters;
		}

		private void CheckOrderModel(Order order)
		{
			if (order == null)
			{
				throw new STOSystemException("Order cannot be null!");
			}

			if (order.Id < 0)
			{
				throw new STOSystemException("OrderId should be more than 0!");
			}

			if (string.IsNullOrEmpty(order.CarVincode) || order.CarVincode == "")
			{
				order.CarVincode = null;
				//throw new STOSystemException("CarVincode cannot be null or empty");
			}

			if (string.IsNullOrEmpty(order.Description))
			{
				throw new STOSystemException("Description cannot be null or empty");
			}

			if (string.IsNullOrEmpty(order.Details))
			{
				throw new STOSystemException("Details cannot be null or empty");
			}

			if (order.UserId != null && order.UserId < 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			if (order.StartDate > order.FinisheDate)
			{
				throw new STOSystemException("FinisheDate cannot be less than StartDate");
			}

			if (order.Price < 0)
			{
				throw new STOSystemException("Price cannot be less than 0");
			}

			if (order.PriceOfDetails < 0)
			{
				throw new STOSystemException("Price of details cannot be less than 0");
			}
		}

		private void CheckPropertyOfOrderFilterSearchModel(OrderFilterSearchModel model)
		{
			if (model == null) throw new STOSystemException("Filter cannot be null");

			if (model.StartDate != null && model.FinisheDate != null && model.StartDate > model.FinisheDate)
			{
				throw new STOSystemException("FinisheDate cannot be less than StartDate");
			}

			if (model.UserId != null && model.UserId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			if (model.CarVincode != null && (model.CarVincode == "" || model.CarVincode == string.Empty))
			{
				throw new STOSystemException("CarVincode cannot be null or empty");
			}
		}
	}
}
