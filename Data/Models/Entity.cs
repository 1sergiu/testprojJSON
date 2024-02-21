using System.ComponentModel.DataAnnotations;

namespace TestProjJSON.Data.Models
{
    public class Entity
    {
        [Key]
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "TypeGuid is required.")]
        public Guid TypeGuid { get; set; }

        public string Description { get; set; }

	}
}