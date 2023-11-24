using STOWebApi.Data.Validation;

namespace STOWebApi.Data.Repositories
{
	internal class TransactionRepositoty
	{
		private readonly STODbContext _dbContext;
		public TransactionRepositoty(STODbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddWithTransactionAsync<TEntity>(Func<TEntity, Task> addFunc, TEntity entity) where TEntity : class
		{
			using (var transaction = await _dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					await addFunc(entity);

					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw new STODataBaseException("Щось пішло не так при додаванні даних.");
				}
			}
		}

		public async Task DeleteWithTransactionAsync<TKey>(Func<TKey, Task> deleteFunc, TKey key)
		{
			using (var transaction = await _dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					await deleteFunc(key);

					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw new STODataBaseException("Щось пішло не так при видаленні даних.");
				}
			}
		}

		public async Task UpdateWithTransactionAsync<TEntity>(Func<TEntity, Task> updateFunc, TEntity entity) where TEntity : class
		{
			using (var transaction = await _dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					await updateFunc(entity);

					await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
					throw new STODataBaseException("Щось пішло не так при оновленні даних.");
				}
			}
		}
	}
}
