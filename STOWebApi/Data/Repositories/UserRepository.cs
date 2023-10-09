using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly STODbContext _dbContext;

		public UserRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(User entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if(await _dbContext.Users.FindAsync(entity.Id) != null)
			{
				throw new ArgumentException("This element is already exist!");
			}

			await _dbContext.Users.AddAsync(entity);

			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			User? user = await _dbContext.Users.FindAsync(id);
			
			if(user == null)
			{
				throw new ArgumentException("This element isn`t exist!");
			}

			_dbContext.Users.Remove(user);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			IEnumerable<User> users = await _dbContext.Users.ToListAsync();

			return users;
		}

		public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
		{
			IEnumerable<User> users = await _dbContext.Users
															.Include(u => u.Orders)
															.ThenInclude(o => o.Masters)
															.Include(u => u.Cars)
															.ToListAsync();

			return users;
		}

		public async Task<User?> GetByIdAsync(int id)
		{
			User? user = await _dbContext.Users.FindAsync(id);

			return user;
		}

		public async Task<User?> GetByIdWithDetailsAsync(int id)
		{
			User? user = await _dbContext.Users
												.Where(u => u.Id == id)
												.Include(u => u.Cars)
												.Include(u => u.Orders)
												.ThenInclude(o => o.Masters)
												.FirstOrDefaultAsync();

			return user;
		}

		public async Task<User?> GetUserByUserNameAsync(string userName)
		{
			User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

			return user;
		}

		public async Task<IEnumerable<User>> GetUsersByRoleAsync(RoleEnum role)
		{
			IEnumerable<User> users = await _dbContext.Users.Where(u => u.Role == role).ToListAsync();

			return users;
		}

		public async Task UpdateAsync(User entity)
		{
			if(entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbContext.Users.Update(entity);

			await _dbContext.SaveChangesAsync();
		}
	}
}
