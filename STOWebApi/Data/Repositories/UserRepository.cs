using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly STODbContext _dbContext;
		private readonly TransactionRepositoty _transactionRepositoty;

		public UserRepository(STODbContext dbContext)
		{
			_dbContext = dbContext;
			_transactionRepositoty = new TransactionRepositoty(dbContext);
		}

		public async Task AddAsync(User entity)
		{
			await _transactionRepositoty.AddWithTransactionAsync<User>(AddFunctionAsync, entity);
		}

		private async Task AddFunctionAsync(User entity)
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
			await _transactionRepositoty.DeleteWithTransactionAsync<int>(DeleteFunctionByIdAsync, id);
		}

		private async Task DeleteFunctionByIdAsync(int id)
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
															.OrderByDescending(u => u.Id)
															.Include(u => u.Orders)
															.ThenInclude(o => o.Masters)
															.Include(u => u.Cars)
															.ToListAsync();

			return users;
		}

		public async Task<User?> GetByIdAsync(int id)
		{
			User? user = await _dbContext.Users.FindAsync(id);

			if (user != null)
			{
				_dbContext.Entry(user).State = EntityState.Detached;
			}

			return user;
		}

		public async Task<User?> GetByIdWithDetailsAsync(int id)
		{
			User? user = await _dbContext.Users
												.Where(u => u.Id == id)
												.OrderByDescending(u => u.Id)
												.Include(u => u.Cars)
												.Include(u => u.Orders)
												.ThenInclude(o => o.Masters)
												.FirstOrDefaultAsync();

			return user;
		}

		public async Task<User?> GetUserByUserNameAsync(string userName)
		{
			User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));

			return user;
		}

		public async Task<IEnumerable<User>> GetUsersByRoleAsync(RoleEnum role)
		{
			IEnumerable<User> users = await _dbContext.Users.Where(u => u.Role == role).OrderByDescending(u => u.Id).ToListAsync();

			return users;
		}

		public async Task UpdateAsync(User entity)
		{
			await _transactionRepositoty.UpdateWithTransactionAsync<User>(UpdateFunctionAsync, entity);
		}

		private async Task UpdateFunctionAsync(User entity)
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
