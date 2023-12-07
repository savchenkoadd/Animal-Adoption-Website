using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoption.Infrastructure.Helpers
{
	internal class BindingContext
	{
		public Type EntityType { get; }
		public string TableName { get; }

		public BindingContext(
				Type entityType,
				string tableName
			)
		{
			if (entityType is null || tableName is null)
			{
				throw new ArgumentNullException();
			}

			EntityType = entityType;
			TableName = tableName;
		}
	}
}
