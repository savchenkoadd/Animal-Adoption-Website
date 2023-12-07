using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Infrastructure.Helpers
{
	internal static class CopyHelper
	{
		internal async static Task CopyAnimalProfileFields(AnimalProfile? from, AnimalProfile? to)
		{
			if (from is null || to is null)
			{
				throw new ArgumentNullException();
			}

			to.Name = from.Name;
			to.Age = from.Age;
			to.Description = from.Description;
			to.Breed = from.Breed;
			to.ImageUrl = from.ImageUrl;

			await Task.CompletedTask;
		}
	}
}
