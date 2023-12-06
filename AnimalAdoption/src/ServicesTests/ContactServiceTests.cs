using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using FluentAssertions;
using Moq;

namespace ServicesTests
{
	public class ContactServiceTests
	{
		private readonly IContactService _contactService;
		private readonly IContactRepository _contactRepository;
		private readonly Mock<IContactRepository> _contactRepositoryMock;

		public ContactServiceTests()
		{
			_contactRepositoryMock = new Mock<IContactRepository>();
			_contactRepository = _contactRepositoryMock.Object;

			_contactService = new ContactService(_contactRepository);
		}


		#region CreateForm


		[Fact]
		public async Task CreateForm_NullRequest_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _contactService.Create(null);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateForm_InvalidModelState_ToBeArgumentException()
		{
			Func<Task> action1 = async () =>
			{
				await _contactService.Create(new ContactFormCreateRequest()
				{
					SenderEmail = "1"
				});
			};

			Func<Task> action2 = async () =>
			{
				await _contactService.Create(new ContactFormCreateRequest()
				{
					SenderEmail = "1",
					Description = "2",
					SenderId = Guid.NewGuid(),
					Subject = "Subject"
				});
			};

			await action1.Should().ThrowAsync<ArgumentException>();
			await action2.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateForm_ValidRequest_ToBeTrue()
		{
			var request = new ContactFormCreateRequest()
			{
				Subject = "1",
				Description = "2",
				SenderId = Guid.NewGuid(),
				SenderEmail = "example@gmail.com"
			};

			_contactRepositoryMock
				.Setup(temp => temp.Create(It.IsAny<ContactForm>()))
				.ReturnsAsync(true);

			var actual = await _contactService.Create(request);

			actual.Should().BeTrue();
		}


		#endregion


		#region GetAllForms


		[Fact]
		public async Task GetAllForms_Empty_ToBeEmptyList()
		{
			_contactRepositoryMock
				.Setup(temp => temp.GetAll())
				.ReturnsAsync(null as List<ContactForm>);

			var actual = await _contactService.GetAll();

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAllForms_NotEmpty_ToBeSuccesful()
		{
			var forms = new List<ContactForm>()
			{
				new ContactForm()
				{
					Description = "1",
					Id = Guid.NewGuid(),
					Subject = "1",
					Response = "1",
					SenderEmail = "example@gmail.com",
					SenderId = Guid.NewGuid()
				},
				new ContactForm()
				{
					Description = "1",
					Id = Guid.NewGuid(),
					Subject = "1",
					Response = "1",
					SenderEmail = "example@gmail.com",
					SenderId = Guid.NewGuid()
				}
			};

			var expected = forms
				.Select(temp => new ContactFormResponse()
				{
					Id = temp.Id,
					Subject = temp.Subject,
					Response = temp.Response,
					Description = temp.Description,
					SenderEmail = temp.SenderEmail,
					SenderId = temp.SenderId
				});

			_contactRepositoryMock
				.Setup(temp => temp.GetAll())
				.ReturnsAsync(forms);

			var actual = await _contactService.GetAll();

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion


		#region GetByUserId


		[Fact]
		public async Task GetByUserId_NullUserId_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _contactService.GetByUserId(null);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task GetByUserId_WrongUserId_ToBeEmptyList()
		{
			_contactRepositoryMock
				.Setup(temp => temp.GetByUserId(It.IsAny<Guid>()))
				.ReturnsAsync(null as List<ContactForm>);

			var actual = await _contactService.GetByUserId(Guid.NewGuid());

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetByUserId_ProperUserId_ToBeSuccesful()
		{
			var userId = Guid.NewGuid();

			var forms = new List<ContactForm>()
			{
				new ContactForm()
				{
					Description = "Test",
					Response = "Test",
					SenderEmail = "Test",
					SenderId = userId,
					Subject = "Test",
					Id = Guid.NewGuid()
				},
				new ContactForm()
				{
					Description = "Test",
					Response = "Test",
					SenderEmail = "Test",
					SenderId = userId,
					Subject = "Test",
					Id = Guid.NewGuid()
				}
			};

			var expected = forms
							.Where(temp => temp.SenderId == userId)
							.Select(temp => new ContactFormResponse()
							{
								SenderId = temp.SenderId,
								Subject = temp.Subject,
								Id = temp.Id,
								Description = temp.Description,
								Response = temp.Response,
								SenderEmail = temp.SenderEmail	
							}).ToList();
			
			_contactRepositoryMock
				.Setup(temp => temp.GetByUserId(It.IsAny<Guid>()))
				.ReturnsAsync(forms);

			var actual = await _contactService.GetByUserId(userId);

			actual.Should().BeEquivalentTo(expected);
		}


		#endregion


		#region Respond 


		[Fact]
		public async Task Respond_NullRequest_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _contactService.Respond(null);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task Respond_InvalidModelState_ToBeArgumentException()
		{
			var requests = new List<ContactFormRespondRequest>()
			{
				new ContactFormRespondRequest()
				{
					Id = null,
					SenderId = Guid.NewGuid(),
					Response = "Response"
				},
				new ContactFormRespondRequest()
				{
					Id = Guid.NewGuid(),
					SenderId = null,
					Response = "Response"
				},
				new ContactFormRespondRequest()
				{
					Id = Guid.NewGuid(),
					SenderId = Guid.NewGuid(),
					Response = null
				},
			};

			var actions = new List<Func<Task>>();

			foreach (var request in requests)
			{
				Func<Task> action = async () =>
				{
					await _contactService.Respond(request);
				};

				actions.Add(action);
			}

			actions
				.Select(async temp => await temp.Should().ThrowAsync<ArgumentException>());

			await Task.CompletedTask;
		}

		[Fact]
		public async Task Respond_WrongUserId_ToBeFalse()
		{
			ContactFormRespondRequest form = new ContactFormRespondRequest()
			{
				Id = Guid.NewGuid(),
				Response = "Response",
				SenderId = Guid.NewGuid()
			};

			_contactRepositoryMock
				.Setup(temp => temp.Respond(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>()))
				.ReturnsAsync(false);

			var actual = await _contactService.Respond(form);

			actual.Should().BeFalse();
		}

		[Fact]
		public async Task Respond_ProperUserId_ToBeTrue()
		{
			ContactFormRespondRequest form = new ContactFormRespondRequest()
			{
				Id = Guid.NewGuid(),
				Response = "Response",
				SenderId = Guid.NewGuid()
			};

			_contactRepositoryMock
				.Setup(temp => temp.Respond(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>()))
				.ReturnsAsync(true);

			var actual = await _contactService.Respond(form);

			actual.Should().BeTrue();
		}


		#endregion
	}
}
