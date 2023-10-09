using STOWebApi.Business.Models;
using STOWebApi.Data.Entity;

namespace STOWebApi.Business.Interfaces
{
	public interface IMasterService : ICrud<MasterModel, MasterRegistrationModel, int>
	{
		Task<IEnumerable<MasterModel>> GetMastersByTypeAsync(MasterTypeEnum type);
	}
}
