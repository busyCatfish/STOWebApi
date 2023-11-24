using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;
using STOWebApi.Data.Validation;

namespace STOWebApi.Data.Repositories
{
	public class CarRepository : ICarRepository
	{
		private readonly STODbContext _dbContext;
		private readonly TransactionRepositoty _transactionRepositoty;

		public CarRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
			_transactionRepositoty = new TransactionRepositoty(dbContext);
		}

		public async Task AddAsync(Car entity)
		{
			await _transactionRepositoty.AddWithTransactionAsync<Car>(AddFunctionAsync, entity);
		}

		private async Task AddFunctionAsync(Car entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (await _dbContext.Cars.FindAsync(entity.Vincode) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Cars.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(string vincode)
		{
			await _transactionRepositoty.DeleteWithTransactionAsync<string>(DeleteFunctionByIdAsync, vincode);
		}

		private async Task DeleteFunctionByIdAsync(string vincode)
		{
			Car? car = await _dbContext.Cars.FindAsync(vincode);
			
			if(car == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			using (var transaction = _dbContext.Database.BeginTransaction())
			{
				try
				{
					_dbContext.Cars.Remove(car);

					await _dbContext.SaveChangesAsync();

					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw new STODataBaseException("Щось пішло не так при видаленні машини.");
				}
			}
		}

		public async Task<IEnumerable<Car>> GetAllAsync()
		{
			IEnumerable<Car> workers = await _dbContext.Cars.ToListAsync();

			return workers;
		}

		public async Task<IEnumerable<Car>> GetAllWithDetailsAsync()
		{
			IEnumerable<Car> cars = await _dbContext.Cars
															.Include(c => c.User)
															.Include(c => c.Orders)
															.ToListAsync();

			return cars;
		}

		public async Task<Car?> GetByIdAsync(string vincode)
		{
			Car? car = await _dbContext.Cars.FindAsync(vincode);

			return car;
		}

		public async Task<Car?> GetByIdWithDetailsAsync(string vincode)
		{
			Car? car = await _dbContext.Cars
												.Where(u => u.Vincode == vincode)
												.Include(c => c.User)
												.Include(c => c.Orders)
												.FirstOrDefaultAsync();

			return car;
		}

		public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(int userId)
		{
			IEnumerable<Car> cars = await _dbContext.Cars
												.Where(c => c.UserId == userId)
												.Include(c => c.User)
												.Include(c => c.Orders)
												.ToListAsync();

			return cars;
		}

		public async Task UpdateAsync(Car entity)
		{
			await _transactionRepositoty.UpdateWithTransactionAsync<Car>(UpdateFunctionAsync, entity);
		}

		private async Task UpdateFunctionAsync(Car entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			using (var transaction = _dbContext.Database.BeginTransaction())
			{
				try
				{
					_dbContext.Cars.Update(entity);

					await _dbContext.SaveChangesAsync();

					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw new STODataBaseException("Щось пішло не так при оновленні машини.");
				}
			}
		}
	}
}
