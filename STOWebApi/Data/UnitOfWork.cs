using Microsoft.EntityFrameworkCore;
using STOWebApi.Data.Interfaces;
using STOWebApi.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace STOWebApi.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly STODbContext stoDbContext;

		public UnitOfWork(STODbContext stoDbContext)
		{
			this.stoDbContext = stoDbContext;
		}

		public IUserRepository UserRepository => new UserRepository(this.stoDbContext);

		public ICarRepository CarRepository => new CarRepository(this.stoDbContext);

		public IOrderRepository OrderRepository => new OrderRepository(this.stoDbContext);

		public IMasterRepository MasterRepository => new MasterRepository(this.stoDbContext);

		public IWorkerRepository WorkerRepository => new WorkerRepository(this.stoDbContext);

		public IOrderMasterRepository OrderMasterRepository => new OrderMasterRepository(this.stoDbContext);

		public async Task SaveAsync()
		{
			await this.stoDbContext.SaveChangesAsync();
		}
	}
}
