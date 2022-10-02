using CarInfo.Entities;
using CarInfo.Interfaces;
using CarInfo.Models.CarModel;

namespace CarInfo.Mapping
{
    public class CarModelMapping: IMapper<Entities.CarModel, CarsModelModel>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarModelMapping(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }

        public CarsModelModel MapFromEntityToModel(CarModel source) => new CarsModelModel
        {
            Id = source.Id,
            Mark = source.Mark,
            Model = source.Model,
            Years = source.Years,
            Type = source.CarTypeId,
            Engine=source.Engine,
            HorsePower = source.HorsePower,
            Acceleration=source.Acceleration,
            Consumption= source.Consumption,

        };

       

        public CarModel MapFromModelToEntity(CarsModelModel source)
        {
            var sedan=new CarModel();

            MapFromModelToEntity(source,sedan);

            return sedan;
        }

        public void  MapFromModelToEntity(CarsModelModel source, CarModel target)
        {
            var photo = UploadFile(source);

            target.Id = source.Id;
            target.Mark = source.Mark;
            target.Model = source.Model;
            target.Years = source.Years;
            target.CarTypeId =source.Type;
            target.Engine = source.Engine;
            target.HorsePower = source.HorsePower;
            target.Acceleration = source.Acceleration;
            target.Consumption = source.Consumption;
            target.Image = photo;

        }

        public string UploadFile(CarsModelModel sedan)
        {
            string uniqueFileName = null;

            if (sedan.Image != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sedan.Image.FileName;
                string filepath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    sedan.Image.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}
