using STOWebApi.Data.Entity;

namespace STOWebApi.Data.Interfaces
{
	public interface IWorkerRepository : IRepository<Worker, int>
	{
		Task<IEnumerable<Worker>> GetWorkersByPositionAsync(PositionEnum position);
	}
}
