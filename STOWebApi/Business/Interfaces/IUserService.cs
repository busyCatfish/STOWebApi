using STOWebApi.Business.Models;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Interfaces
{
	public interface IUserService : ICrud<UserModel, UserRegistrationModel, int>
	{
		Task<IEnumerable<UserModel>> GetUsersByRollAsync(RoleEnum role);

		Task<UserModel?> Authentificate(UserLoginModel userLoginModel);
	}
}
