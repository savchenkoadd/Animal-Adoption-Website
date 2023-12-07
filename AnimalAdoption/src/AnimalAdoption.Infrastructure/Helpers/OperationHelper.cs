using AnimalAdoption.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Helpers
{
	internal static class OperationHelper
	{
		internal static async Task<int> PerformOperationAndSaveAsync(DbContext dbContext, Func<Task> operation)
		{
			await operation.Invoke();

			return await dbContext.SaveChangesAsync();
		}
	}
}
