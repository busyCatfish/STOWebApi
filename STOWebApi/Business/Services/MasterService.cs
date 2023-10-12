using AutoMapper;
using STOWebApi.Business.Interfaces;
using STOWebApi.Business.Models;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Entity;
using STOWebApi.Data.Interfaces;
using System.Data;

namespace STOWebApi.Business.Services
{
	public class MasterService : IMasterService
	{
		public MasterService(IUnitOfWork @object, IMapper mapper)
		{
			Object = @object;
			Mapper = mapper;
		}

		public IUnitOfWork Object { get; }

		public IMapper Mapper { get; }

		public async Task AddAsync(MasterRegistrationModel model)
		{
			if (model == null)
			{
				throw new STOSystemException("Master cannot be null!");
			}

			Master master = Mapper.Map<Master>(model);

			this.CheckMasterModel(master);

			await Object.MasterRepository.AddAsync(master);

			await Object.SaveAsync();
		}

		public async Task DeleteByIdAsync(int modelId)
		{
			if(modelId <= 0)
			{
				throw new STOSystemException("MasterId should be more than 0!");
			}

			await Object.MasterRepository.DeleteByIdAsync(modelId);

			await Object.SaveAsync();
		}

		public async Task<IEnumerable<MasterModel>> GetAllAsync()
		{
			var allMasters = await Object.MasterRepository.GetAllWithDetailsAsync();

			var mastersModel = Mapper.Map<IEnumerable<MasterModel>>(allMasters);

			return mastersModel;
		}

		public async Task<MasterModel> GetByIdAsync(int modelId)
		{
			if (modelId <= 0)
			{
				throw new STOSystemException("MasterId should be more than 0!");
			}

			var master = await Object.MasterRepository.GetByIdWithDetailsAsync(modelId);

			var masterModel = Mapper.Map<MasterModel>(master);

			return masterModel;
		}

		public async Task<IEnumerable<MasterModel>> GetMastersByTypeAsync(MasterTypeEnum type)
		{
			var allMasters = await Object.MasterRepository.GetMastersByTypeAsync(type);

			var mastersModel = Mapper.Map<IEnumerable<MasterModel>>(allMasters);

			return mastersModel;
		}

		public async Task UpdateAsync(MasterRegistrationModel model, int masterId)
		{
			if (model == null)
			{
				throw new STOSystemException("Master cannot be null!");
			}

			Master master = Mapper.Map<Master>(model);

			master.Id = masterId;

			CheckMasterModel(master);

			await Object.MasterRepository.UpdateAsync(master);

			await Object.SaveAsync();
		}

		private void CheckMasterModel(Master master)
		{
			if (master == null)
			{
				throw new STOSystemException("Master cannot be null!");
			}

			if (master.Id < 0)
			{
				throw new STOSystemException("MasterId should be more than 0!");
			}

			if (master.WorkerId <= 0)
			{
				throw new STOSystemException("WorkerId should be more than 0!");
			}

			if (string.IsNullOrEmpty(master.Description))
			{
				throw new STOSystemException("Description cannot be null or empty");
			}
		}
	}
}
