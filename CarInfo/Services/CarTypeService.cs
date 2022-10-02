using Microsoft.EntityFrameworkCore;
using CarInfo.Interfaces;
using CarInfo.Data;
using CarInfo.Entities;


namespace CarInfo.Models.CarTypeModel
{
    public class CarTypeService : ICarTypeService
    {
        public readonly CarInfoContext _context;
        public readonly IMapper<CarType, CarTypeModel> _carTypeMapper;

        public CarTypeService(CarInfoContext context, IMapper<Entities.CarType, CarTypeModel> carTypeMapper)
        {
            _carTypeMapper = carTypeMapper;
            _context = context;
        }

        public async Task<IEnumerable<CarType>> GetCarTypes(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return _context.CarTypes;
            }
            return await _context.CarTypes.Where(x => x.Category.Equals(searchString)).ToListAsync();
        }

        public async Task<CreateCarTypeReturn> CreateCarType(CarTypeModel carType)
        {
            var carTypeAlreadyExists = _context.CarTypes.Any(x => x.Id == carType.Id);

            if (carTypeAlreadyExists)
            {
                throw new DbUpdateException($"CarType With Id {carType.Id} Already Exists");
            }

            var record = _context.CarTypes.Add(_carTypeMapper.MapFromModelToEntity(carType));
            await _context.SaveChangesAsync();

            return new CreateCarTypeReturn { CreatedCarType = _carTypeMapper.MapFromEntityToModel(record.Entity) };
        }

        public async Task<GetCarTypeReturn> GetCarType(GetCarTypeRequest getCarTypeRequest)
        {
            var carType = await _context.CarTypes.FindAsync(getCarTypeRequest.Id);

            return new GetCarTypeReturn { CarType = _carTypeMapper.MapFromEntityToModel(carType) };
        }

        public async Task<UpdateCarTypeReturn> UpdateCarType(UpdateCarTypeRequest updateCarTypeRequest)
        {
            var carTypeExists = await _context.CarTypes.FindAsync(updateCarTypeRequest.CarTypeToUpdate.Id);

            if (carTypeExists == null)
            {
                throw new DbUpdateException($"CarType With Id{updateCarTypeRequest.CarTypeToUpdate.Id} Does not exists");
            }

            _carTypeMapper.MapFromModelToEntity(updateCarTypeRequest.CarTypeToUpdate, carTypeExists);

            await _context.SaveChangesAsync();

            return new UpdateCarTypeReturn { UptadetedCarType = updateCarTypeRequest.CarTypeToUpdate };
        }


        public async Task<DeleteCarTypeReturn> DeleteCarType(DeleteCarTypeRequest deleteCarTypeRequest)
        {
            var carTypeDelete = await _context.CarTypes.FindAsync(deleteCarTypeRequest.Id);

            if (carTypeDelete == null)
            {
                throw new DbUpdateException($"CarType With Id{deleteCarTypeRequest.Id}does not exists");
            }

            _context.CarTypes.Remove(carTypeDelete);
            await _context.SaveChangesAsync();
            return new DeleteCarTypeReturn();
        }
    }
}
