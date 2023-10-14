using STOWebApi.Data.Interfaces;

namespace STOWebApi.Data.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }

		ICarRepository CarRepository { get; }

		IOrderRepository OrderRepository { get; }

		IMasterRepository MasterRepository { get; }

		IWorkerRepository WorkerRepository { get; }

		IOrderMasterRepository OrderMasterRepository { get; }

		Task SaveAsync();
	}
}
