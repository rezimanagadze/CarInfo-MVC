using CarInfo.Models.SuperCarModel;
using CarInfo.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfo.Interfaces
{
    public interface ISuperCarService
    {

        Task<IEnumerable<SuperCar>> GetSuperCars(String searchString);
        Task<CreateSuperCarReturn> CreateSuperCar(SuperCarModel request);
        Task<UpdateSuperCarReturn> UpdateSuperCar(UpdateSuperCarRequest request);
        Task<DeleteSuperCarReturn> DeleteSuperCar(DeleteSuperCarRequest request);
        Task<GetSuperCarReturn> GetSuperCar(GetSuperCarRequest request);
        List<SelectListItem>EnumCategorySuperCar();

    }
}