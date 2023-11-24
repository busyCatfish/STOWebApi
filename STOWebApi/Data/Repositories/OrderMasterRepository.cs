using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class OrderMasterRepository : IOrderMasterRepository
	{
		private STODbContext _stoDbContext;
		private readonly TransactionRepositoty _transactionRepositoty;

		public OrderMasterRepository(STODbContext stoDbContext)
		{
			_stoDbContext = stoDbContext;
			_transactionRepositoty = new TransactionRepositoty(_stoDbContext);
		}

		public async Task DeleteByOrderIdAsync(int orderId)
		{
			await _transactionRepositoty.DeleteWithTransactionAsync<int>(DeleteFunctionByOrderIdAsync, orderId);
		}

		public async Task DeleteFunctionByOrderIdAsync(int orderId)
		{
			await _stoDbContext.OrdersMasters.Where(om => om.OrderId == orderId).ExecuteDeleteAsync();
		}
	}
}
