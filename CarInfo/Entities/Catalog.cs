using System.ComponentModel.DataAnnotations;

namespace CarInfo.Entities
{
    public class Catalog
    {
        public Guid Id { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        [Required(ErrorMessage ="Is required field")]
        public string Mark {get; set;}
        [Required(ErrorMessage = "Is required field")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]

        public string Country { get; set;}
        [Required(ErrorMessage = "Is required field")]
        public string Logo { get; set; }
    }
}
