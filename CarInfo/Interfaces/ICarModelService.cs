using CarInfo.Entities;
using CarInfo.Models.CarModel;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfo.Interfaces
{
    public interface ICarModelService
    {
        Task<IEnumerable<CarModel>> AllModel(String searchString);
        Task<CreateCarModelReturn> CreateModel(CarsModelModel request);
        Task<UpdateCarModelReturn> UpdateModel(UpdateCarModelRequest request);
        Task<DeleteCarModelReturn> DeleteModel(DeleteCarModelRequest request);
        Task<GetCarModelReturn> GetModel(GetCarModelRequest request);
        List<SelectListItem> EnumCategoryModel();
    }
}
