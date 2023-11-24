using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly STODbContext _dbContext;
		private readonly TransactionRepositoty _transactionRepositoty;

		public OrderRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
			_transactionRepositoty = new TransactionRepositoty(dbContext);
		}

		public async Task AddAsync(Order entity)
		{
			await _transactionRepositoty.AddWithTransactionAsync<Order>(AddFunctionAsync, entity);
		}

		private async Task AddFunctionAsync(Order entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if(await _dbContext.Orders.FindAsync(entity.Id) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Orders.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			await _transactionRepositoty.DeleteWithTransactionAsync<int>(DeleteFunctionByIdAsync, id);
		}

		private async Task DeleteFunctionByIdAsync(int id)
		{
			Order? order = await _dbContext.Orders.FindAsync(id);
			
			if(order == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			_dbContext.Orders.Remove(order);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> GetAllAsync()
		{
			IEnumerable<Order> orders = await _dbContext.Orders.OrderByDescending(o => o.Id).ToListAsync();

			return orders;
		}

		public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
		{
			IEnumerable<Order> orders = await _dbContext.Orders
															.OrderByDescending(o => o.Id)
															.Include(o => o.Car)
															.Include(o => o.User)
															.Include(o => o.Masters)
															.ThenInclude(m => m.Worker)
															.ToListAsync();

			return orders;
		}

		public async Task<Order?> GetByIdAsync(int id)
		{
			Order? order = await _dbContext.Orders.FindAsync(id);

			return order;
		}

		public async Task<Order?> GetByIdWithDetailsAsync(int id)
		{
			Order? order = await _dbContext.Orders
												.Where(u => u.Id == id)
												.OrderByDescending(o => o.Id)
												.Include(o => o.Car)
												.Include(o => o.User)
												.Include(o => o.Masters)
												.ThenInclude(m => m.Worker)
												.FirstOrDefaultAsync();

			return order;
		}

		public async Task<IEnumerable<Order>> GetOrdersByPeriodOfTimeAsync(DateTime start, DateTime finish)
		{
			IEnumerable<Order> orders = await _dbContext.Orders
												.Where(o => o.StartDate >= start && o.StartDate <= finish)
												.OrderByDescending(o => o.Id)
												.Include(o => o.Car)
												.Include(o => o.User)
												.Include(o => o.Masters)
												.ThenInclude(m => m.Worker)
												.ToListAsync();

			return orders;
		}

		public async Task<IEnumerable<Order>> GetOrdersByStateAsync(StateEnum state)
		{
			IEnumerable<Order> orders = await _dbContext.Orders
									.Where(o => o.State == state)
									.OrderByDescending(o => o.Id)
									.Include(o => o.Car)
									.Include(o => o.User)
									.Include(o => o.Masters)
									.ThenInclude(m => m.Worker)
									.ToListAsync();

			return orders;
		}

		public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
		{
			IEnumerable<Order> orders = await _dbContext.Orders
						.Where(o => o.UserId == userId)
						.OrderByDescending(o => o.Id)
						.Include(o => o.Car)
						.Include(o => o.User)
						.Include(o => o.Masters)
						.ThenInclude(m => m.Worker)
						.ToListAsync();

			return orders;
		}

		public async Task<IEnumerable<Order>> GetOrdersByVincodeAsync(string vincode)
		{
			IEnumerable<Order> orders = await _dbContext.Orders
																.Where(o => o.CarVincode == vincode)
																.OrderByDescending(o => o.Id)
																.Include(o => o.Car)
																.Include(o => o.User)
																.Include(o => o.Masters)
																.ThenInclude(m => m.Worker)
																.ToListAsync();

			return orders;
		}

		public async Task UpdateAsync(Order entity)
		{
			await _transactionRepositoty.UpdateWithTransactionAsync<Order>(UpdateFunctionAsync, entity);
		}

		private async Task UpdateFunctionAsync(Order entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			await _dbContext.OrdersMasters.Where(om => om.OrderId == entity.Id).ExecuteDeleteAsync();

			_dbContext.Orders.Update(entity);

			await _dbContext.SaveChangesAsync();
		}
	}
}
