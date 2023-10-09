namespace STOWebApi.Business.Interfaces
{
	public interface ICrud<TModel, TRegistrationModel, TKey>
	{
		Task<IEnumerable<TModel>> GetAllAsync();

		Task<TModel> GetByIdAsync(TKey modelId);

		Task AddAsync(TRegistrationModel model);

		Task DeleteByIdAsync(TKey modelId);

		Task UpdateAsync(TModel model);
	}
}
