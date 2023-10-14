using STOWebApi.Data.Interfaces;
using STOWebApi.Data.Entity;

namespace STOWebApi.Data.Interfaces
{
	public interface IMasterRepository : IRepository<Master, int>
	{
		Task<IEnumerable<Master>> GetMastersByTypeAsync(MasterTypeEnum type);

		Task<IList<Master>> GetByIdsAsync(IEnumerable<int> mastersId);

		Task<IEnumerable<int>> GetIdsByOrderIdWithDetailsAsync(int orderId);
	}
}
