using System.ComponentModel.DataAnnotations;

namespace CarInfo.Entities
{
    public class CarModel
    {
        public Guid Id { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Mark { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage = "Is required field")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Is required field")]
        public Guid CarTypeId { get; set; }
        public CarType CarTypes { get; set; }
        [Required(ErrorMessage = "Is required field")]
        public int Years { get; set; }
        [Required(ErrorMessage = "Is required field")]
        [MaxLength(20, ErrorMessage = "Maximum length is 50 characters")]
        public string Engine { get; set; }
        [Required(ErrorMessage = "Is required field")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string HorsePower { get; set; }
        [Required(ErrorMessage = "Is required field")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Acceleration { get; set; }
        public string? Consumption { get; set; }
        [Required(ErrorMessage = "Is required field")]
        public string Image { get; set; }
    }

}
