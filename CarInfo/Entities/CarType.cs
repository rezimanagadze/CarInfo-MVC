using System.ComponentModel.DataAnnotations;

namespace CarInfo.Entities
{
    public class CarType
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Is required field")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Category { get; set; }
        [Required(ErrorMessage ="Is required field")]
        public string Image { get; set; }
    }
}
