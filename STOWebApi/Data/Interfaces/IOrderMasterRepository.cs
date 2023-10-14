namespace STOWebApi.Data.Interfaces
{
	public interface IOrderMasterRepository
	{
		Task DeleteByOrderIdAsync(int orderId);
	}
}
