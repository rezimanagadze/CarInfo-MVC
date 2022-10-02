using CarInfo.Models.CatalogModel;
using CarInfo.Entities;

namespace CarInfo.Interfaces
{
    public interface ICatalogService
    {
        
        Task<IEnumerable<Catalog>> GetCatalogs(String searchString);
        Task<CreateCatalogReturn> CreateCatalog(CatalogModel  request);
        Task<UpdateCatalogReturn> UpdateCatalog(UpdateCatalogRequest request);
        Task<DeleteCatalogReturn> DeleteCatalog(DeleteCatalogRequest request);
        Task<GetCatalogReturn> GetCatalog(GetCatalogRequest request);

    }
}
