using STOWebApi.Business.Models;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Interfaces
{
	public interface ICarService : ICrud<CarModel, CarRegistrationModel, string>
	{
		Task<IEnumerable<CarModel>> GetCarsByUserIdAsync(int userId);
	}
}
