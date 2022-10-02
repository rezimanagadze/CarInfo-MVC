using CarInfo.Entities;
using CarInfo.Models.SuperCarModel;
using CarInfo.Interfaces;

namespace CarInfo.Mapping
{
    public class SuperCarMapping : IMapper<SuperCar, SuperCarModel>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SuperCarMapping(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }


        public SuperCarModel MapFromEntityToModel(SuperCar source) => new SuperCarModel
        {
            Id = source.Id,
            Mark = source.Mark,
            Model = source.Model,
            Category=source.Category,
            Years = source.Years,
            Engine = source.Engine,
            HorsePower = source.HorsePower,
            TopSpeed = source.TopSpeed,
            Acceleration = source.Acceleration,
            Consumption = source.Consumption,
            Price=source.Price,

        };

        public SuperCar MapFromModelToEntity(SuperCarModel source)
        {
            var SuperCar = new SuperCar();

            MapFromModelToEntity(source, SuperCar);

            return SuperCar;
        }


        public void MapFromModelToEntity(SuperCarModel source, SuperCar target)
        {
            string image = UploadFile(source);

            target.Id = source.Id;
            target.Mark = source.Mark;
            target.Model = source.Model;
            target.Category = source.Category;
            target.Years = source.Years;
            target.Engine = source.Engine;
            target.HorsePower = source.HorsePower;
            target.TopSpeed = source.TopSpeed;
            target.Acceleration = source.Acceleration;
            target.Consumption = source.Consumption;
            target.Price = source.Price;
            target.Image = image;
        }

        public string UploadFile(SuperCarModel SuperCar)
        {
            string uniqueFileName = null;

            if (SuperCar.Image != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + SuperCar.Image.FileName;
                string filepath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    SuperCar.Image.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}
