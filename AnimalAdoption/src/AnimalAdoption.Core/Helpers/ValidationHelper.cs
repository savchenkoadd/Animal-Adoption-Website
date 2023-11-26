using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Helpers
{
	internal class ValidationHelper
	{
		internal static async Task ValidateRequest(object? obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException();
			}

			List<ValidationResult> results = new List<ValidationResult>();
			ValidationContext validationContext = new ValidationContext(obj);

			if (!Validator.TryValidateObject(obj, validationContext, validationResults: results, validateAllProperties: true))
			{
				throw new ArgumentException(results.First().ErrorMessage);
			}

			await Task.CompletedTask;
		}
	}
}
