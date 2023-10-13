namespace STOWebApi.Business.Interfaces
{
	public interface ICrud<TModel, TRegistrationModel, TKey>
	{
		Task<IEnumerable<TModel>> GetAllAsync();

		Task<TRegistrationModel> GetByIdAsync(TKey modelId);

		Task AddAsync(TRegistrationModel model);

		Task DeleteByIdAsync(TKey modelId);

		Task UpdateAsync(TRegistrationModel model, TKey modelId);
	}
}
