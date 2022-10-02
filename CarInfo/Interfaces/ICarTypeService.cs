using CarInfo.Models.CarTypeModel;
using CarInfo.Entities;

namespace CarInfo.Interfaces
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarType>> GetCarTypes(String searchString);
        Task<CreateCarTypeReturn> CreateCarType(CarTypeModel request);
        Task<UpdateCarTypeReturn> UpdateCarType(UpdateCarTypeRequest request);
        Task<DeleteCarTypeReturn> DeleteCarType(DeleteCarTypeRequest request);
        Task<GetCarTypeReturn> GetCarType(GetCarTypeRequest request);
    }
}
