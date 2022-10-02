namespace CarInfo.Models.CarTypeModel
{
    public class CarTypeModel
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }

    }
}
