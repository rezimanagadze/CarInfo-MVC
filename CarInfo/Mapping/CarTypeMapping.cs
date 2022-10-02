using CarInfo.Entities;
using CarInfo.Models.CarTypeModel;
using CarInfo.Interfaces;

namespace CarInfo.Mapping
{
    public class CarTypeMapping : IMapper<CarType, CarTypeModel>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarTypeMapping(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }

        public CarTypeModel MapFromEntityToModel(CarType source) => new CarTypeModel
        {
            Id = source.Id,
            Category = source.Category,
        };

        public CarType MapFromModelToEntity(CarTypeModel source)
        {
            var carType = new CarType();

            MapFromModelToEntity(source, carType);

            return carType;
        }


        public void MapFromModelToEntity(CarTypeModel source, CarType target)
        {
            var photo = UploadFile(source);

            target.Id = source.Id;
            target.Image = photo;
            target.Category = source.Category;
        }

        public string UploadFile(CarTypeModel carType)
        {
            string uniqueFileName = null;

            if (carType.Image != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + carType.Image.FileName;
                string filepath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    carType.Image.CopyTo(fileStream);
                }

            }
            return uniqueFileName;

        }
    }
}
