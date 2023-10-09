using STOWebApi.Data.Entity;

namespace STOWebApi.Data.Interfaces
{
	public interface IUserRepository : IRepository<User, int>
	{
		Task<IEnumerable<User>> GetUsersByRoleAsync(RoleEnum role);

		Task<User?> GetUserByUserNameAsync(string userName);
	}
}
