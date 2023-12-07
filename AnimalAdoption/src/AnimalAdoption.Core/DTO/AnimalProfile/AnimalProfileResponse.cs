using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.AnimalProfile
{
    public class AnimalProfileResponse
    {
        public Guid Id { get; set; }

        [Range(0, 100, ErrorMessage = "Age must be in range of 0 to 100")]
        [Required(ErrorMessage = "Age can't be empty")]
        public int? Age { get; set; }

        [StringLength(20, ErrorMessage = "Name length must not exceed 20 characters")]
        [Required(ErrorMessage = "Name can't be empty")]
        public string? Name { get; set; }

        [StringLength(1500, ErrorMessage = "Description length must not exceed 1500 characters")]
        [Required(ErrorMessage = "Description can't be empty")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Image url must be a valid url")]
        [StringLength(200, ErrorMessage = "Image url length must not exceed 200 characters")]
        [Required(ErrorMessage = "Image url can't be empty")]
        public string? ImageUrl { get; set; }

        [StringLength(30, ErrorMessage = "Breed length must not exceed 30 characters")]
        [Required(ErrorMessage = "Breed can't be empty")]
        public string? Breed { get; set; }
    }
}
