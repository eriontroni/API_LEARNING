using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models
{
    public class AddRegionViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code Has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code Has to be a maximum of 3 characters")] // i bjen qe Code duhet mekon 3 karaktere
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name Has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

    }
}
