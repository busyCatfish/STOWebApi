using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class MasterRepository : IMasterRepository
	{
		private readonly STODbContext _dbContext;

		public MasterRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(Master entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (await _dbContext.Masters.FindAsync(entity.Id) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Masters.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			Master? master = await _dbContext.Masters.FindAsync(id);

			if (master == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			_dbContext.Masters.Remove(master);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Master>> GetAllAsync()
		{
			IEnumerable<Master> masters = await _dbContext.Masters.ToListAsync();

			return masters;
		}

		public async Task<IList<Master>> GetByIdsAsync(IEnumerable<int> mastersId)
		{
			IList<Master> masters = await _dbContext.Masters
																	.Where(master => mastersId.Contains(master.Id))
																	.ToListAsync();

			return masters;
		}

		public async Task<IEnumerable<Master>> GetAllWithDetailsAsync()
		{
			IEnumerable<Master> masters = await _dbContext.Masters
															.Include(m => m.Worker)
															.Include(m => m.Orders)
															.ThenInclude(o => o.Car)
															.ToListAsync();

			return masters;
		}

		public async Task<IEnumerable<int>> GetIdsByOrderIdWithDetailsAsync(int orderId)
		{
			IEnumerable<int> mastersId = await _dbContext.Masters
															.Include(m => m.Orders)
															.Where(m => m.Orders.FirstOrDefault(o => o.Id == orderId) != null)
															.Select(m => m.Id)
															.ToListAsync();

			return mastersId;
		}

		public async Task<Master?> GetByIdAsync(int id)
		{
			Master? master = await _dbContext.Masters.FindAsync(id);

			return master;
		}

		public async Task<Master?> GetByIdWithDetailsAsync(int id)
		{
			Master? master = await _dbContext.Masters
												.Where(u => u.Id == id)
												.Include(m => m.Worker)
												.Include(m => m.Orders)
												.ThenInclude(o => o.Car)
												.FirstOrDefaultAsync();

			return master;
		}

		public async Task<IEnumerable<Master>> GetMastersByTypeAsync(MasterTypeEnum type)
		{
			IEnumerable<Master> masters = await _dbContext.Masters
												.Where(m => m.Type == type)
												.ToListAsync();

			return masters;
		}

		public async Task UpdateAsync(Master entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbContext.Masters.Update(entity);

			await _dbContext.SaveChangesAsync();
		}
	}
}
