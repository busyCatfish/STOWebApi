using STOWebApi.Data.Entity;

namespace STOWebApi.Data.Interfaces
{
	public interface ICarRepository : IRepository<Car, string>
	{
		Task<IEnumerable<Car>> GetCarsByUserIdAsync(int userId);
	}
}
