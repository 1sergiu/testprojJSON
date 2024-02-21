using System.ComponentModel.DataAnnotations;

namespace TestProjJSON.Data.Models
{
    public class Classifier
    {
        [Key]
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; }
	}
}
