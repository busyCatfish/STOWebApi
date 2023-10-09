using AutoMapper;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;
using System.Data;

namespace STOWebApi.Business.Services
{
	public class WorkerService : IWorkerService
	{
		public WorkerService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task AddAsync(WorkerRegistrationModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("Worker cannot be null!");
			}

			Worker worker = Mapper.Map<Worker>(model);

			this.CheckWorkerModel(worker);

			await Object.WorkerRepository.AddAsync(worker);

			await Object.SaveAsync();
		}

		public async Task DeleteByIdAsync(int modelId)
		{
			if (modelId <= 0)
			{
				throw new STOSystemException("WorkerId should be more than 0!");
			}

			await Object.WorkerRepository.DeleteByIdAsync(modelId);

			await Object.SaveAsync();
		}

		public async Task<IEnumerable<WorkerModel>> GetAllAsync()
		{
			var allWorkers = await Object.WorkerRepository.GetAllWithDetailsAsync();

			var workersModel = Mapper.Map<IEnumerable<WorkerModel>>(allWorkers);

			return workersModel;
		}

		public async Task<WorkerModel> GetByIdAsync(int modelId)
		{
			if (modelId <= 0)
			{
				throw new STOSystemException("UserId should be more than 0!");
			}

			var worker = await Object.WorkerRepository.GetByIdWithDetailsAsync(modelId);

			var workerModel = Mapper.Map<WorkerModel>(worker);

			return workerModel;
		}

		public async Task<IEnumerable<WorkerModel>> GetWorkersByPositionAsync(PositionEnum position)
		{
			var allWorkers = await Object.WorkerRepository.GetWorkersByPositionAsync(position);

			var workersModel = Mapper.Map<IEnumerable<WorkerModel>>(allWorkers);

			return workersModel;
		}

		public async Task UpdateAsync(WorkerModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("Worker cannot be null!");
			}

			Worker worker = Mapper.Map<Worker>(model);

			CheckWorkerModel(worker);

			await Object.WorkerRepository.UpdateAsync(worker);

			await Object.SaveAsync();
		}

		private void CheckWorkerModel(Worker worker)
		{
			if (worker == null)
			{
				throw new STOSystemException("Worker cannot be null!");
			}

			if (worker.Id <= 0)
			{
				throw new STOSystemException("WorkerId should be more than 0!");
			}

			if (worker.Salary < 0)
			{
				throw new STOSystemException("Worker salary cannot be less than 0");
			}

			if (string.IsNullOrEmpty(worker.FirstName))
			{
				throw new STOSystemException("Name cannot be null or empty");
			}

			if (string.IsNullOrEmpty(worker.LastName))
			{
				throw new STOSystemException("Surname cannot be null or empty");
			}

			if (string.IsNullOrEmpty(worker.Email))
			{
				throw new STOSystemException("Email cannot be null or empty");
			}

			if (string.IsNullOrEmpty(worker.Telephone))
			{
				throw new STOSystemException("Telephone cannot be null or empty");
			}
		}
	}
}
