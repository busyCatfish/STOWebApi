using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class OrderMasterRepository : IOrderMasterRepository
	{
		private STODbContext _stoDbContext;

		public OrderMasterRepository(STODbContext stoDbContext)
		{
			_stoDbContext = stoDbContext;
		}

		public async Task DeleteByOrderIdAsync(int orderId)
		{
			await _stoDbContext.OrdersMasters.Where(om => om.OrderId == orderId).ExecuteDeleteAsync();
		}
	}
}
