using System.ComponentModel.DataAnnotations;

namespace AnimalAdoption.Core.DTO.AnimalProfile
{
    public class AnimalProfileUpdateRequest
    {
        [Range(0, 100)]
        public int? Age { get; set; }

        [StringLength(20)]
        public string? Name { get; set; }

        [StringLength(1500)]
        public string? Description { get; set; }

        [Url]
        [StringLength(200)]
        public string? ImageUrl { get; set; }

        [StringLength(30)]
        public string? Breed { get; set; }
    }
}
