using System.ComponentModel.DataAnnotations;

namespace CarInfo.Entities
{
    public class SuperCar
    {
        public Guid Id { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Mark { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Model { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Is required field")]
        public int Years { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Engine { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string HorsePower { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string TopSpeed { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Acceleration { get; set; }
        public string? Consumption { get; set; }
        public string? Price { get; set; }
        [Required(ErrorMessage = "Is required field")]
        public string Image { get; set; }
    }
}





