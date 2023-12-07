using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.Helpers
{
	/// <summary>
	/// Helper class for validating objects using data annotations.
	/// </summary>
	internal class ValidationHelper
	{
		/// <summary>
		/// Validates the specified objects using data annotations.
		/// </summary>
		/// <param name="objects">The objects to validate.</param>
		/// <exception cref="ArgumentNullException">Thrown when the input array or any object in it is null.</exception>
		/// <exception cref="ArgumentException">Thrown when validation fails. The exception message contains the first validation error.</exception>
		/// <returns>A completed task.</returns>
		internal static async Task ValidateObjects(params object?[]? objects)
		{
			if (objects is null)
			{
				throw new ArgumentNullException();
			}

			foreach (var obj in objects)
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
			}

			await Task.CompletedTask;
		}
	}
}
