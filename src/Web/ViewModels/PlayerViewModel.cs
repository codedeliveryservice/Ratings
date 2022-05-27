using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class PlayerViewModel
{
    public int Id { get; set; }

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    [Display(Name = "Image")]
    public IFormFile? FormFile { get; set; }

    [Display(Name = "Biography")]
    public string? Biography { get; set; }

    [Required]
    [Display(Name = "Classical")]
    public int ClassicalRating { get; set; }

    [Required]
    [Display(Name = "Rapid")]
    public int RapidRating { get; set; }

    [Required]
    [Display(Name = "Blitz")]
    public int BlitzRating { get; set; }
}