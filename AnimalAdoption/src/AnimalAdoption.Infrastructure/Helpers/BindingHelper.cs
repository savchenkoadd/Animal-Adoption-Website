using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Helpers
{
	internal static class BindingHelper
	{
		private static ModelBuilder? _modelBuilder;
		private static readonly object _lockObject = new object();

		internal static void InitializeBuilder(ModelBuilder modelBuilder)
		{
			lock (_lockObject)
			{
				if (modelBuilder is null)
				{
					throw new ArgumentNullException();
				}

				_modelBuilder = modelBuilder;
			}
		}

		internal static void BindEntityToTable(BindingContext? bindingContext)
		{
			lock (_lockObject)
			{
				if (bindingContext is null)
				{
					throw new ArgumentNullException();
				}

				_modelBuilder?.Entity(bindingContext.EntityType).ToTable(bindingContext.TableName);
			}
		}

		internal static void BindEntitiesToTables(IEnumerable<BindingContext>? bindingContexts)
		{
			lock (_lockObject)
			{
				if (bindingContexts is null)
				{
					throw new ArgumentNullException();
				}

				foreach (var context in bindingContexts)
				{
					BindEntityToTable(context);
				}
			}
		}
	}
}
