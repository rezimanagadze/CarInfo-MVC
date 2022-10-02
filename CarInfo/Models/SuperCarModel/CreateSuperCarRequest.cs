namespace CarInfo.Models.SuperCarModel
{
    public class CreateSuperCarRequest
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public int Years { get; set; }
        public string Engine { get; set; }
        public string HorsePower { get; set; }
        public string TopSpeed { get; set; }
        public string Acceleration { get; set; }
        public string Consumption { get; set; }
        public string Price { get; set; }
        public IFormFile Image { get; set; }
    }
}

