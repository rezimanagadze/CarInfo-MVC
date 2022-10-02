using CarInfo.Data;
using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfo.Models.SuperCarModel
{
    public class SuperCarService : ISuperCarService
    {
        public readonly CarInfoContext _context;
        public readonly IMapper<Entities.SuperCar, SuperCarModel> _SuperCarMapper;

        public SuperCarService(CarInfoContext context, IMapper<Entities.SuperCar, SuperCarModel> SuperCarMapper)
        {
            _SuperCarMapper = SuperCarMapper;
            _context = context;
        }

        public async Task<IEnumerable<SuperCar>> GetSuperCars(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return _context.SuperCars;
            }

            var superCars = from s in _context.SuperCars
                            select s;

            return await _context.SuperCars.AsNoTracking().Where(x => x.Mark.Equals(searchString)).ToListAsync();
        }

        public async Task<CreateSuperCarReturn> CreateSuperCar(SuperCarModel superCar)
        {

            var superCarAlreadyExists = await _context.SuperCars.AnyAsync(x => x.Id == superCar.Id);

            if (superCarAlreadyExists)
            {
                throw new DbUpdateException($"SuperCar With Id {superCar.Id} Already Exists");
            }

            var record = _context.SuperCars.Add(_SuperCarMapper.MapFromModelToEntity(superCar));
            await _context.SaveChangesAsync();

            return new CreateSuperCarReturn { CreatedSuperCar = _SuperCarMapper.MapFromEntityToModel(record.Entity) };
        }

        public async Task<GetSuperCarReturn> GetSuperCar(GetSuperCarRequest getSuperCarRequest)
        {
            var superCar = await _context.SuperCars.FindAsync(getSuperCarRequest.Id);

            return new GetSuperCarReturn { SuperCar = _SuperCarMapper.MapFromEntityToModel(superCar) };
        }

        public async Task<UpdateSuperCarReturn> UpdateSuperCar(UpdateSuperCarRequest updateSuperCarRequest)
        {
            var SuperCarExists = await _context.SuperCars.FindAsync(updateSuperCarRequest.SuperCarToUpdate.Id);

            if (SuperCarExists == null)
            {
                throw new DbUpdateException($"SuperCar With Id{updateSuperCarRequest.SuperCarToUpdate.Id} Does not exists");
            }

            _SuperCarMapper.MapFromModelToEntity(updateSuperCarRequest.SuperCarToUpdate, SuperCarExists);

            await _context.SaveChangesAsync();

            return new UpdateSuperCarReturn { UptadetedSuperCar = updateSuperCarRequest.SuperCarToUpdate };
        }

        public async Task<DeleteSuperCarReturn> DeleteSuperCar(DeleteSuperCarRequest deleteSuperCarRequest)
        {
            var superCarDelete = await _context.SuperCars.FindAsync(deleteSuperCarRequest.Id);

            if (superCarDelete == null)
            {
                throw new DbUpdateException($"SuperCar With Id{deleteSuperCarRequest.Id}does not exists");
            }

            _context.SuperCars.Remove(superCarDelete);
            await _context.SaveChangesAsync();
            return new DeleteSuperCarReturn();
        }

        public List<SelectListItem> EnumCategorySuperCar()
        {
            List<SelectListItem> categoryEnum = new List<SelectListItem>()
            {
                new SelectListItem{Value="Sedan", Text="Sedan"},
                new SelectListItem{Value="Jeep", Text="Jeep"},
                new SelectListItem{Value="Coupe", Text="Coupe"},
                new SelectListItem{Value="Hatchback", Text="Hatchback"},
                new SelectListItem{Value="Cabriolet", Text="Cabriolet"},
                new SelectListItem{Value="Pickup", Text="Pickup"}

            };

            return categoryEnum;
        }

    }
}


