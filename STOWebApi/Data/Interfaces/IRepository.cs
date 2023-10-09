namespace STOWebApi.Data.Interfaces
{
	public interface IRepository<TEntity, TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		
		Task<IEnumerable<TEntity>> GetAllWithDetailsAsync();

		Task<TEntity?> GetByIdAsync(TKey id);

		Task<TEntity?> GetByIdWithDetailsAsync(TKey id);

		Task AddAsync(TEntity entity);

		Task DeleteByIdAsync(TKey id);

		Task UpdateAsync(TEntity entity);
	}
}
