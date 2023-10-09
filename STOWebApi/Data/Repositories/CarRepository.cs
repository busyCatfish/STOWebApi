using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class CarRepository : ICarRepository
	{
		private readonly STODbContext _dbContext;

		public CarRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(Car entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if(await _dbContext.Cars.FindAsync(entity.Vincode) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Cars.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(string vincode)
		{
			Car? car = await _dbContext.Cars.FindAsync(vincode);
			
			if(car == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			_dbContext.Cars.Remove(car);

			await _dbContext.SaveChangesAsync();
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
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbContext.Cars.Update(entity);

			await _dbContext.SaveChangesAsync();
		}
	}
}
