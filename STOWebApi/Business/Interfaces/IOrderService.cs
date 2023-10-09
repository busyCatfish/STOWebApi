using STOWebApi.Business.Models;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Interfaces
{
	public interface IOrderService : ICrud<OrderModel, OrderRegistrationModel, int>
	{
		Task<IEnumerable<OrderModel>> GetOrdersByFilterAsync(OrderFilterSearchModel filter);
		//Task<IEnumerable<OrderModel>> GetOrdersByStateAsync(StateEnum state);

		//Task<IEnumerable<OrderModel>> GetOrdersByPeriodOfTimeAsync(DateTime start, DateTime finish);

		//Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId);

		//Task<IEnumerable<OrderModel>> GetOrdersByVincodeAsync(string vincode);
	}
}
