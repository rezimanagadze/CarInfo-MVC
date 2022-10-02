using CarInfo.Entities;

namespace CarInfo.Models.CarTypeModel
{
    public class CreateCarTypeRequest
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }

    }
}

