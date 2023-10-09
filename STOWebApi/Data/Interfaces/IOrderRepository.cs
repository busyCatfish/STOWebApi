using STOWebApi.Data.Entity;

namespace STOWebApi.Data.Interfaces
{
	public interface IOrderRepository: IRepository<Order, int>
	{
		Task<IEnumerable<Order>> GetOrdersByStateAsync(StateEnum state);

		Task<IEnumerable<Order>> GetOrdersByPeriodOfTimeAsync(DateTime start, DateTime finish);
		
		Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

		Task<IEnumerable<Order>> GetOrdersByVincodeAsync(string vincode);
	}
}
