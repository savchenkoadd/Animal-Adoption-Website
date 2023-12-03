﻿using AnimalAdoption.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO
{
	public class RequestResponse
	{
		public Guid Id { get; set; }

		public RequestStatus Status { get; set; }

		public int? Age { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		public string? Breed { get; set; }
	}
}
