using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AnimalAdoption.Core.Enums;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace ServicesTests
{
	public class RequestServiceTests
	{
		private readonly IRequestService _requestService;
		private readonly Mock<IRequestRepository> _requestRepositoryMock;
		private readonly IRequestRepository _requestRepository;
		private readonly Mock<IAnimalRepository> _animalRepositoryMock;
		private readonly IAnimalRepository _animalRepository;

		public RequestServiceTests()
		{
			_requestRepositoryMock = new Mock<IRequestRepository>();
			_requestRepository = _requestRepositoryMock.Object;
			_animalRepositoryMock = new Mock<IAnimalRepository>();
			_animalRepository = _animalRepositoryMock.Object;

			_requestService = new RequestService(_requestRepository, _animalRepository);
		}


		#region AddRequest


		[Fact]
		public async Task AddRequest_NullRequest_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _requestService.AddRequest(null!);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task AddRequest_InvalidModelState_ToBeArgumentException()
		{
			Func<Task> action = async () =>
			{
				await _requestService.AddRequest(new AddRequest()
				{
					Age = -1,
					Breed = "Breed",
					Name = "name",
					Description = "description",
					ImageUrl = "https://microsoft.com",
					UserId = Guid.NewGuid(),
				});
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task AddRequest_ProperRequest_ToBeTrue()
		{
			AddRequest? request = new AddRequest()
			{
				Age = 1,
				UserId = Guid.NewGuid(),
				Description = "description",
				ImageUrl = "https://microsoft.com",
				Name = "name",
				Breed = "Breed"
			};

			_requestRepositoryMock
				.Setup(temp => temp.AddRequest(It.IsAny<Request>()))
				.ReturnsAsync(true);

			var actual = await _requestService.AddRequest(request);

			actual.Should().BeTrue();
		}


		#endregion


		#region RejectRequest


		[Fact]
		public async Task RejectRequest_NullId_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _requestService.RejectRequest(null!);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task RejectRequest_WrongId_ToBeFalse()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetRequest(It.IsAny<Guid>()))
				.ReturnsAsync(null as Request);

			var actual = await _requestService.RejectRequest(Guid.NewGuid());

			actual.Should().BeFalse();
		}

		[Fact]
		public async Task RejectRequest_ProperId_ToBeTrue()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetRequest(It.IsAny<Guid>()))
				.ReturnsAsync(new Request());

			var actual = await _requestService.RejectRequest(Guid.NewGuid());

			actual.Should().BeTrue();
		}


		#endregion


		#region ApproveRequest


		[Fact]
		public async Task ApproveRequest_NullId_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _requestService.ApproveRequest(null!);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task ApproveRequest_WrongId_ToBeFalse()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetRequest(It.IsAny<Guid>()))
				.ReturnsAsync(null as Request);

			var actual = await _requestService.ApproveRequest(Guid.NewGuid());

			actual.Should().BeFalse();
		}

		[Fact]
		public async Task ApproveRequest_ProperId_ToBeTrue()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetRequest(It.IsAny<Guid>()))
				.ReturnsAsync(new Request());

			var actual = await _requestService.ApproveRequest(Guid.NewGuid());

			actual.Should().BeTrue();
		}


		#endregion


		#region GetRequestsForAdmin


		[Fact]
		public async Task GetRequestsForAdmin_NullRequests_ToBeEmptyList()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetAllRequests())
				.ReturnsAsync(null as List<Request>);

			var actual = await _requestService.GetRequestsForAdmin();

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetRequestsForAdmin_FoundRequests_ToBeSuccesful()
		{
			var animalId = Guid.NewGuid();

			List<Request> requests = new List<Request>()
			{
				new Request()
				{
					UserId = Guid.NewGuid(),
					Status = RequestStatus.InProcess,
					Age = 1,
					AnimalId = animalId,
					Breed = "Breed",
					Description = "Description",
					ImageUrl = "https://example.com",
					Name = "Name"
				},
				new Request()
				{
					UserId = Guid.NewGuid(),
					Status = RequestStatus.Approved,
					Age = 2,
					AnimalId = Guid.NewGuid(),
					Breed = "Breed",
					Description = "Description",
					ImageUrl = "https://example.com",
					Name = "Name",
				},
				new Request()
				{
					UserId = Guid.NewGuid(),
					Status = RequestStatus.Rejected,
					Age = 3,
					AnimalId = Guid.NewGuid(),
					Breed = "Breed",
					Description = "Description",
					ImageUrl = "https://example.com",
					Name = "Name",
				}
			};

			List<RequestResponse> expected = new List<RequestResponse>()
			{
				new RequestResponse()
				{
					ImageUrl = requests[0].ImageUrl,
					Age = requests[0].Age,
					Breed = requests[0].Breed,
					Description = requests[0].Description,
					Name = requests[0].Name,
					Status = requests[0].Status,
					Id = animalId
				}
			};

			_requestRepositoryMock
				.Setup(temp => temp.GetAllRequests())
				.ReturnsAsync(requests);

			var actual = await _requestService.GetRequestsForAdmin();

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion


		#region GetRequestsByUserId


		[Fact]
		public async Task GetRequestsByUserId_NullId_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _requestService.GetRequestsByUserId(null);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task GetRequestsByUserId_WrongId_ToBeEmptyList()
		{
			_requestRepositoryMock
				.Setup(temp => temp.GetRequestsByUserId(It.IsAny<Guid>()))
				.ReturnsAsync(null as List<Request>);

			var actual = await _requestService.GetRequestsByUserId(Guid.NewGuid());

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetRequestsByUserId_ValidId_ToBeSuccesful()
		{
			var userId = Guid.NewGuid();

			var list = new List<Request>()
			{
				new Request()
				{
					Age = 1,
					AnimalId = Guid.NewGuid(),
					Breed = "Breed",
					Description = "description",
					ImageUrl = "https://example.com",
					Name = "name",
					Status = RequestStatus.InProcess,
					UserId = userId,
				},
				new Request()
				{
					Age = 2,
					AnimalId = Guid.NewGuid(),
					Breed = "Breed1",
					Description = "description1",
					ImageUrl = "https://example1.com",
					Name = "name1",
					Status = RequestStatus.Approved,
					UserId = userId,
				},
				new Request()
				{
					Age = 3,
					AnimalId = Guid.NewGuid(),
					Breed = "Breed3",
					Description = "description3",
					ImageUrl = "https://example1.com",
					Name = "name3",
					Status = RequestStatus.Rejected,
					UserId = userId,
				}
			};

			var expected = list
							.Where(temp => temp.UserId == userId)
							.Select(temp =>
							new RequestResponse()
							{
								Age = temp.Age,
								Breed = temp.Breed,
								Description = temp.Description,
								ImageUrl = temp.ImageUrl,
								Id = temp.AnimalId,
								Name = temp.Name,
								Status = temp.Status,
							});

			_requestRepositoryMock
				.Setup(temp => temp.GetRequestsByUserId(It.IsAny<Guid>()))
				.ReturnsAsync(list);

			var actual = await _requestService.GetRequestsByUserId(Guid.NewGuid());

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion
	}
}
