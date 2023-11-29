using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using FluentAssertions;
using Moq;

namespace ServicesTests
{
	public class AnimalServiceTests
	{
		private readonly IAnimalService _animalService;
		private readonly Mock<IAnimalRepository> _animalRepositoryMock;
		private readonly IAnimalRepository _animalRepository;

        public AnimalServiceTests()
        {
			_animalRepositoryMock = new Mock<IAnimalRepository>();
			_animalRepository = _animalRepositoryMock.Object;

			_animalService = new AnimalService(_animalRepository);
		}


		#region CreateAnimalProfile


		[Fact]
		public async Task CreateAnimalProfile_NullProfile_ToBeArgumentNullException()
		{
			AnimalProfileAddRequest animalProfile = null!;

			Func<Task> action = async () =>
			{
				await _animalService.CreateAnimalProfile(animalProfile);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateAnimalProfile_AgeOutOfRange_ToBeArgumentException()
		{
			AnimalProfileAddRequest animalProfileAddRequest = new AnimalProfileAddRequest()
			{
				Age = -1,
				Breed = "rgebbbbeb",
				Description = "description",
				ImageUrl = "https://microsoft.com",
				Name = "name",
			};

			Func<Task> action = async () =>
			{
				await _animalService.CreateAnimalProfile(animalProfileAddRequest);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateAnimalProfile_NameOutOfRange_ToBeArgumentException()
		{
			AnimalProfileAddRequest animalProfileAddRequest = new AnimalProfileAddRequest()
			{
				Age = 12,
				Breed = "rgebbbbebdvdvdvdvdvdvdvvdvdvdvdvdvdvv",
				Description = "description",
				ImageUrl = "https://microsoft.com",
				Name = "name",
			};

			Func<Task> action = async () =>
			{
				await _animalService.CreateAnimalProfile(animalProfileAddRequest);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateAnimalProfile_InvalidUrl_ToBeArgumentException()
		{
			AnimalProfileAddRequest animalProfileAddRequest = new AnimalProfileAddRequest()
			{
				Age = 12,
				Breed = "rgebbbbe",
				Description = "description",
				ImageUrl = "Unknown",
				Name = "name",
			};

			Func<Task> action = async () =>
			{
				await _animalService.CreateAnimalProfile(animalProfileAddRequest);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateAnimalProfile_ProperParams_ToBeSuccesful()
		{
			AnimalProfileAddRequest animalProfileAddRequest = new AnimalProfileAddRequest()
			{
				Age = 12,
				Breed = "rgebbbbe",
				Description = "description",
				ImageUrl = "https://microsoft.com",
				Name = "Name",
			};

			AnimalProfile animalProfile = new AnimalProfile()
			{
				Age = animalProfileAddRequest.Age,
				Breed = animalProfileAddRequest.Breed,
				Description = animalProfileAddRequest.Description,
				ImageUrl = animalProfileAddRequest.ImageUrl,
				Name = animalProfileAddRequest.Name,
				Id = Guid.NewGuid()
			};

			_animalRepositoryMock
				.Setup(temp => temp.CreateAnimalProfile(It.IsAny<AnimalProfile>()))
				.ReturnsAsync(true);

			var actual = await _animalService.CreateAnimalProfile(animalProfileAddRequest);

			actual.Should().Be(true);
		}


		#endregion


		#region DeleteAnimalProfile


		[Fact]
		public async Task DeleteAnimalProfile_NullId_ToBeArgumentNullException()
		{
			Guid? guid = null!;

			Func<Task> action = async () =>
			{
				await _animalService.DeleteAnimalProfile(guid);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task DeleteAnimalProfile_InvalidId_ToBeZero()
		{
			Guid? guid = Guid.NewGuid();

			int expected = 0;

			_animalRepositoryMock
				.Setup(temp => temp.DeleteAnimalProfile(It.IsAny<Guid>()))
				.ReturnsAsync(expected);

			var actual = await _animalService.DeleteAnimalProfile(guid);

			actual.Should().Be(expected);
		}

		[Fact]
		public async Task DeleteAnimalProfile_ProperId_ToBeMoreThanZero()
		{
			Guid validGuid = Guid.NewGuid();
			int expectedCount = 1; // Assuming that 1 indicates successful deletion

			_animalRepositoryMock
				.Setup(temp => temp.DeleteAnimalProfile(It.IsAny<Guid>()))
				.ReturnsAsync(expectedCount);

			var actualCount = await _animalService.DeleteAnimalProfile(validGuid);

			actualCount.Should().BeGreaterThan(0);
		}


		#endregion


		#region UpdateAnimalProfile


		[Fact]
		public async Task UpdateAnimalProfile_NullIdOrNullProfile_ToBeArgumentNullException()
		{
			Guid? nullGuid = null!;
			AnimalProfileUpdateRequest? validAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = 12,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "https://microsoft.com",
				Name = "Name"
			};

			Func<Task> nullGuidAction = async () =>
			{
				await _animalService.UpdateAnimalProfile(nullGuid, validAnimalProfile);
			};

			Func<Task> nullProfileAction = async () =>
			{
				await _animalService.UpdateAnimalProfile(Guid.NewGuid(), null!);
			};

			await nullGuidAction.Should().ThrowAsync<ArgumentNullException>();
			await nullProfileAction.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task UpdateAnimalProfile_InvalidGuid_ToBeZero()
		{
			int expected = 0;

			AnimalProfileUpdateRequest? validAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = 12,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "https://microsoft.com",
				Name = "Name"
			};

			_animalRepositoryMock
				.Setup(temp => temp.UpdateAnimalProfile(It.IsAny<Guid>(), It.IsAny<AnimalProfile>()))
				.ReturnsAsync(expected);

			var actual = await _animalService.UpdateAnimalProfile(Guid.NewGuid(), validAnimalProfile);

			actual.Should().Be(expected);
		}

		[Fact]
		public async Task UpdateAnimalProfile_AgeOutOfRange_ToBeArgumentException()
		{
			AnimalProfileUpdateRequest? invalidAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = -1,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "https://microsoft.com",
				Name = "Name"
			};

			Func<Task> action = async () =>
			{
				await _animalService.UpdateAnimalProfile(Guid.NewGuid(), invalidAnimalProfile);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task UpdateAnimalProfile_NameOutOfRange_ToBeArgumentException()
		{
			AnimalProfileUpdateRequest? invalidAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = 12,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "https://microsoft.com",
				Name = "NameNameNameNameNameNameNameNameNameNameName"
			};

			Func<Task> action = async () =>
			{
				await _animalService.UpdateAnimalProfile(Guid.NewGuid(), invalidAnimalProfile);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task UpdateAnimalProfile_InvalidUrl_ToBeArgumentException()
		{
			AnimalProfileUpdateRequest? invalidAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = 12,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "Unknown",
				Name = "Name"
			};

			Func<Task> action = async () =>
			{
				await _animalService.UpdateAnimalProfile(Guid.NewGuid(), invalidAnimalProfile);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task UpdateAnimalProfile_ProperParams_ToBeMoreThanZero()
		{
			int unexpectedValue = 0;
			int expectedValue = 1;

			AnimalProfileUpdateRequest? validAnimalProfile = new AnimalProfileUpdateRequest()
			{
				Age = 12,
				Breed = "bbgbg",
				Description = "beebebrb",
				ImageUrl = "https://microsoft.com",
				Name = "Name"
			};

			_animalRepositoryMock
				.Setup(temp => temp.UpdateAnimalProfile(It.IsAny<Guid>(), It.IsAny<AnimalProfile>()))
				.ReturnsAsync(expectedValue);

			var actual = await _animalService.UpdateAnimalProfile(Guid.NewGuid(), validAnimalProfile);

			actual.Should().NotBe(unexpectedValue);
		}


		#endregion


		#region GetAnimalProfiles


		[Fact]
		public async Task GetAnimalProfiles_EmptyProfilesDb_ToBeEmptyList()
		{
			_animalRepositoryMock
				.Setup(temp => temp.GetAnimalProfiles())
				.ReturnsAsync(new List<AnimalProfile>());

			var actual = await _animalService.GetAnimalProfiles();

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAnimalProfiles_ToBeSuccesful()
		{
			var expected = new List<AnimalProfileResponse>()
			{
				new AnimalProfileResponse()
				{
					Age = 12,
					Breed = "Breed",
					Description = "Description",
					Id = Guid.NewGuid(),
					Name = "Name",
					ImageUrl = "https://microsoft.com"
				},
				new AnimalProfileResponse()
				{
					Age = 13,
					Breed = "Breed1",
					Description = "Description1",
					Id = Guid.NewGuid(),
					Name = "Name1",
					ImageUrl = "https://microsoft.com"
				}
			};

			var animalProfiles = expected
				.Select(x => new AnimalProfile()
			{
					Age = x.Age,
					Breed = x.Breed,
					Description = x.Description,
					Id = x.Id,
					Name = x.Name,
					ImageUrl = x.ImageUrl,
			}).ToList();

			_animalRepositoryMock
				.Setup(temp => temp.GetAnimalProfiles())
				.ReturnsAsync(animalProfiles);

			var actual = await _animalService.GetAnimalProfiles();

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion
	}
}