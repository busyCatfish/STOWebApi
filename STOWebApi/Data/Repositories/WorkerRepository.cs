using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class WorkerRepository : IWorkerRepository
	{
		private readonly STODbContext _dbContext;
		private readonly TransactionRepositoty _transactionRepositoty;

		public WorkerRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
			_transactionRepositoty = new TransactionRepositoty(dbContext);
		}

		public async Task AddAsync(Worker entity)
		{
			await _transactionRepositoty.AddWithTransactionAsync<Worker>(AddFunctionAsync, entity);
		}

		private async Task AddFunctionAsync(Worker entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if(await _dbContext.Workers.FindAsync(entity.Id) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Workers.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			await _transactionRepositoty.DeleteWithTransactionAsync<int>(DeleteFunctionByIdAsync, id);
		}

		private async Task DeleteFunctionByIdAsync(int id)
		{
			Worker? worker = await _dbContext.Workers.FindAsync(id);
			
			if(worker == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			_dbContext.Workers.Remove(worker);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Worker>> GetAllAsync()
		{
			IEnumerable<Worker> workers = await _dbContext.Workers.OrderByDescending(w => w.Id).ToListAsync();

			return workers;
		}

		public async Task<IEnumerable<Worker>> GetAllWithDetailsAsync()
		{
			IEnumerable<Worker> workers = await _dbContext.Workers
															.OrderByDescending(w => w.Id)
															.Include(w => w.Master)
															.ToListAsync();

			return workers;
		}

		public async Task<Worker?> GetByIdAsync(int id)
		{
			Worker? worker = await _dbContext.Workers.FindAsync(id);

			return worker;
		}

		public async Task<Worker?> GetByIdWithDetailsAsync(int id)
		{
			Worker? worker = await _dbContext.Workers
												.Where(u => u.Id == id)
												.Include(w => w.Master)
												.FirstOrDefaultAsync();

			return worker;
		}

		public async Task<IEnumerable<Worker>> GetWorkersByPositionAsync(PositionEnum position)
		{
			IEnumerable<Worker> workers = await _dbContext.Workers
												.Where(w => w.Position == position)
												.OrderByDescending(w => w.Id)
												.Include(w => w.Master)
												.ToListAsync();

			return workers;
		}

		public async Task UpdateAsync(Worker entity)
		{
			await _transactionRepositoty.UpdateWithTransactionAsync<Worker>(UpdateFunctionAsync, entity);
		}

		private async Task UpdateFunctionAsync(Worker entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbContext.Workers.Update(entity);

			await _dbContext.SaveChangesAsync();
		}
	}
}
