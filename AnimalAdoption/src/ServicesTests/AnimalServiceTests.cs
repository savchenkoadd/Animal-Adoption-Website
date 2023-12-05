using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace ServicesTests
{
	public class AnimalServiceTests
	{
		private readonly IAnimalService _animalService;
		private readonly Mock<IAnimalRepository> _animalRepositoryMock;
		private readonly IAnimalRepository _animalRepository;
		private readonly IFixture _fixture;

        public AnimalServiceTests()
        {
			_fixture = new Fixture();

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


		#region GetAnimalProfileById


		[Fact]
		public async Task GetAnimalProfileById_NullId_ToBeArgumentNullException()
		{
			Guid? id = null!;

			Func<Task> action = async () =>
			{
				await _animalService.GetAnimalProfileById(id);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task GetAnimalProfileById_WrongId_ToBeArgumentException()
		{
			var id = Guid.NewGuid();

			Func<Task> action = async () =>
			{
				await _animalService.GetAnimalProfileById(id);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task GetAnimalProfileById_ProperId_ToBeSuccesful()
		{
			var profile = new AnimalProfile()
			{
				Age = 1,
				Breed = "Breed",
				Description = "description",
				Id = Guid.NewGuid(),
				ImageUrl = "https://microsoft.com",
				Name = "Name"
			};

			var expected = new AnimalProfileResponse()
			{
				Name = profile.Name,
				Age = profile.Age,
				Breed = profile.Breed,
				Description = profile.Description,
				Id = profile.Id,
				ImageUrl = profile.ImageUrl
			};

			_animalRepositoryMock
				.Setup(temp => temp.GetAnimalProfileById(It.IsAny<Guid>()))
				.ReturnsAsync(profile);

			var actual = await _animalService.GetAnimalProfileById(Guid.NewGuid());

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion


		#region SearchByName


		[Fact]
		public async Task SearchByName_NullName_ToBeArgumentNullException()
		{
			string? name = null!;

			Func<Task> action = async () =>
			{
				await _animalService.SearchByName(name);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task SearchByName_WrongName_ToBeEmptyList()
		{
			_animalRepositoryMock
				.Setup(temp => temp.SearchByName(It.IsAny<string>()))
				.ReturnsAsync(new List<AnimalProfile>());

			var actual = await _animalService.SearchByName(_fixture.Create<string>());

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task SearchByName_ProperName_ToBeSuccesful()
		{
			var mockList = new List<AnimalProfile>()
			{
				_fixture.Build<AnimalProfile>()
						.With(temp => temp.Age, 12)
						.With(temp => temp.ImageUrl, "https://microsoft.com")
						.With(temp => temp.Name, "Test")
						.Create(),

				_fixture.Build<AnimalProfile>()
						.With(temp => temp.Age, 13)
						.With(temp => temp.ImageUrl, "https://microsoft.com")
						.With(temp => temp.Name, "Test1")
						.Create(),
			};

			var expected = new List<AnimalProfileResponse>()
			{
				new AnimalProfileResponse()
				{
					Name = mockList[0].Name,
					Age = mockList[0].Age,
					ImageUrl = mockList[0].ImageUrl,
					Breed = mockList[0].Breed,
					Description = mockList[0].Description,
					Id = mockList[0].Id
				},

				new AnimalProfileResponse()
				{
					Name = mockList[1].Name,
					Age = mockList[1].Age,
					ImageUrl = mockList[1].ImageUrl,
					Breed = mockList[1].Breed,
					Description = mockList[1].Description,
					Id = mockList[1].Id
				},
			};
			
			_animalRepositoryMock
				.Setup(temp => temp.SearchByName(It.IsAny<string>()))
				.ReturnsAsync(mockList);

			var actual = await _animalService.SearchByName(_fixture.Create<string>());

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion
	}
}