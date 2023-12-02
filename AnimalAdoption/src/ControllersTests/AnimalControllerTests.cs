using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.UI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControllersTests
{
	public class AnimalControllerTests
	{
		private readonly IAnimalService _animalService;
        private readonly Mock<IAnimalService> _animalServiceMock;
		private readonly IRequestService _requestService;
		private readonly Mock<IRequestService> _requestServiceMock;

		public AnimalControllerTests()
        {
            _animalServiceMock = new Mock<IAnimalService>();
            _animalService = _animalServiceMock.Object;

			_requestServiceMock = new Mock<IRequestService>();
			_requestService = _requestServiceMock.Object;
		}

        [Fact]
        public async Task Main_ShouldReturnMainView()
        {
            AnimalController animalController = new AnimalController(_animalService, _requestService);

            IActionResult? actionResult = await animalController.Main();

            actionResult.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task Feed_ShouldReturnFeedViewWithProfileResponses()
        {
            List<AnimalProfileResponse> list = new List<AnimalProfileResponse>()
            {
                new AnimalProfileResponse()
                {
                    Age = 1,
                    Breed = "Breed",
                    Description = "description",
                    ImageUrl = "https://microsoft.com",
                    Name = "name",
                    Id = Guid.NewGuid(),
                },
                new AnimalProfileResponse()
                {
                    Age = 12,
                    Breed = "Breed1",
                    Description = "description1",
                    ImageUrl = "https://microsoft.com",
                    Name = "name1",
                    Id = Guid.NewGuid(),
                }
            };

            AnimalController animalController = new AnimalController(_animalService, _requestService);

            _animalServiceMock
                .Setup(temp => temp.GetAnimalProfiles())
                .ReturnsAsync(list);

            IActionResult? actionResult = await animalController.Feed();

            actionResult.Should().BeOfType<ViewResult>();

            ViewResult viewResult = (ViewResult) actionResult;

            viewResult.Model.Should().BeEquivalentTo(list);
        }
    }
}
