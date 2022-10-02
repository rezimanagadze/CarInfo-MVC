using Microsoft.EntityFrameworkCore;
using CarInfo.Interfaces;
using CarInfo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarInfo.Entities;

namespace CarInfo.Models.CarModel
{
    public class CarModelService : ICarModelService
    {
        public readonly CarInfoContext _context;
        public readonly IMapper<Entities.CarModel, CarsModelModel> _carModelMapper;

        public CarModelService(CarInfoContext context, IMapper<Entities.CarModel, CarsModelModel> carModelMapper)
        {
            _carModelMapper = carModelMapper; 
            _context = context;
        }

        public async Task<IEnumerable<Entities.CarModel>> AllModel(string searchString)
        {
            if (searchString == null)
            {
                var models = await _context.Sedans.ToListAsync();
                foreach (var item in models)
                {
                    var CarType = await _context.CarTypes.Where(x => x.Id == item.CarTypeId).FirstOrDefaultAsync();
                    item.CarTypes = CarType;
                }

                return models;
            }

            var model = from s in _context.Sedans
                        select s;

            var models2 = await _context.Sedans.AsNoTracking().Where(x => x.Mark.Equals(searchString)).ToListAsync();

            foreach (var item in models2)
            {
                var CarType = await _context.CarTypes.Where(x => x.Id == item.CarTypeId).FirstOrDefaultAsync();
                item.CarTypes = CarType;
            }
            return models2;
        }

        public async Task<CreateCarModelReturn> CreateModel(CarsModelModel modelModel)
        {

            var modelAlreadyExists = await _context.Sedans.AnyAsync(x => x.Id == modelModel.Id);

            if (modelAlreadyExists)
            {
                throw new DbUpdateException($"model With Id {modelModel.Id} Already Exists");
            }                 

            var record = _context.Sedans.Add(_carModelMapper.MapFromModelToEntity(modelModel));
            await _context.SaveChangesAsync();

            return new CreateCarModelReturn { CreatedSedan = _carModelMapper.MapFromEntityToModel(record.Entity) };
        }

        public async Task<GetCarModelReturn> GetModel(GetCarModelRequest getmodelRequest)
        {
            var model = await _context.Sedans.FindAsync(getmodelRequest.Id);

            return new GetCarModelReturn { Sedan = _carModelMapper.MapFromEntityToModel(model) };
        }

        

        public async Task<UpdateCarModelReturn> UpdateModel(UpdateCarModelRequest updatemodelRequest)
        {
            var modelExists = await _context.Sedans.FindAsync(updatemodelRequest.SedanToUpdate.Id);

            if (modelExists == null)
            {
                throw new DbUpdateException($"model With Id{updatemodelRequest.SedanToUpdate.Id} Does not exists");
            }

            _carModelMapper.MapFromModelToEntity(updatemodelRequest.SedanToUpdate, modelExists);

            await _context.SaveChangesAsync();

            return new UpdateCarModelReturn { UptadetedSedan = updatemodelRequest.SedanToUpdate };
        }

        public async Task<DeleteCarModelReturn> DeleteModel(DeleteCarModelRequest deletemodelRequest)
        {
            var modelDelete = await _context.Sedans.FindAsync(deletemodelRequest.Id);

            if (modelDelete == null)
            {
                throw new DbUpdateException($"model With Id{deletemodelRequest.Id}does not exists");
            }

            _context.Sedans.Remove(modelDelete);
            await _context.SaveChangesAsync();
            return new DeleteCarModelReturn();
        }
        public List<SelectListItem> EnumCategoryModel()
        {
            List<SelectListItem> categoryEnum = new List<SelectListItem>()
            {
                new SelectListItem{Value="04866ee1-f4be-4de8-6f4c-08da9a97991e", Text="model"},
                new SelectListItem{Value="0b9b89a6-2259-42da-6f4d-08da9a97991e", Text="Jeep"},
                new SelectListItem{Value="26291cbc-86e5-4f47-6f4e-08da9a97991e", Text="Coupe"},
                new SelectListItem{Value="6023ed6d-3429-440e-6f4f-08da9a97991e", Text="Hatchback"},
                new SelectListItem{Value="64270de6-6e56-413e-6f50-08da9a97991e", Text="Cabriolet"},
                new SelectListItem{Value="d92febd0-8e3e-4c15-625a-08da9a9b2b8c", Text="Pickup"}

            };

            return categoryEnum;
        }
    }
}
