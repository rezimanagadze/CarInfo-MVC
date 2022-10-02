namespace CarInfo.Models.CarModel
{
    public class CreateCarModelRequest
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public Guid Type { get; set; }
        public int Years { get; set; }
        public string Engine { get; set; }
        public string HorsePower { get; set; }
        public string Acceleration { get; set; }
        public string Consumption{ get; set; }
        public IFormFile Image { get; set; }
    }
}
