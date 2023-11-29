using AnimalAdoption.UI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ControllersTests
{
	public class ErrorControllerTests
	{
		[Fact]
		public async Task Error_ShouldReturnErrorView()
		{
			ErrorController controller = new ErrorController();

			var actionResult = await controller.Error();

			actionResult.Should().BeOfType<ViewResult>();
		}
	}
}