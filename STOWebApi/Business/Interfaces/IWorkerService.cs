using STOWebApi.Business.Models;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Interfaces
{
	public interface IWorkerService : ICrud<WorkerModel, WorkerRegistrationModel, int>
	{
		Task<IEnumerable<WorkerModel>> GetWorkersByPositionAsync(PositionEnum position);
	}
}
