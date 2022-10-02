using CarInfo.Data;
using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarInfo.Models.CatalogModel
{
    public class CatalogService : ICatalogService
    {
        public readonly CarInfoContext _context;
        public readonly IMapper<Entities.Catalog, CatalogModel> _catalogMapper;

        public CatalogService(CarInfoContext context, IMapper<Entities.Catalog, CatalogModel> catalogMapper)
        {
            _catalogMapper =catalogMapper;
            _context = context;
        }

        public async Task<IEnumerable<Catalog>> GetCatalogs(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return _context.Catalogs;
            }

            var catalogs = from s in _context.Catalogs
                           select s;

            return await _context.Catalogs.AsNoTracking().Where(x => x.Mark.Equals(searchString) || x.Country.Equals(searchString)).ToListAsync();
        }

        public async Task<CreateCatalogReturn> CreateCatalog(CatalogModel catalog)
        {

            var catalogAlreadyExists = await _context.Catalogs.AnyAsync(x => x.Id == catalog.Id);

            if (catalogAlreadyExists)
            {
                throw new DbUpdateException($"Catalog With Id {catalog.Id} Already Exists");
            }

            var record = _context.Catalogs.Add(_catalogMapper.MapFromModelToEntity(catalog));
            await _context.SaveChangesAsync();

            return new CreateCatalogReturn { CreatedCatalog = _catalogMapper.MapFromEntityToModel(record.Entity) };
        }
      
        public async Task<GetCatalogReturn> GetCatalog(GetCatalogRequest getCatalogRequest)
        {
            var catalog = await _context.Catalogs.FindAsync(getCatalogRequest.Id);

            return new GetCatalogReturn { Catalog = _catalogMapper.MapFromEntityToModel(catalog) };
        }      

        public async Task<UpdateCatalogReturn> UpdateCatalog(UpdateCatalogRequest updateCatalogRequest)
        {
            var catalogExists = await _context.Catalogs.FindAsync(updateCatalogRequest.CatalogToUpdate.Id);

            if (catalogExists == null)
            {
                throw new DbUpdateException($"Catalog With Id{updateCatalogRequest.CatalogToUpdate.Id} Does not exists");
            }

            _catalogMapper.MapFromModelToEntity(updateCatalogRequest.CatalogToUpdate, catalogExists);

            await _context.SaveChangesAsync();

            return new UpdateCatalogReturn { UptadetedCatalog = updateCatalogRequest.CatalogToUpdate };
        }

        public async Task<DeleteCatalogReturn> DeleteCatalog(DeleteCatalogRequest deleteCatalogRequest)
        {
            var catalogDelete = await _context.Catalogs.FindAsync(deleteCatalogRequest.Id);

            if (catalogDelete == null)
            {
                throw new DbUpdateException($"Catalog With Id{deleteCatalogRequest.Id}does not exists");
            }

            _context.Catalogs.Remove(catalogDelete);
            await _context.SaveChangesAsync();
            return new DeleteCatalogReturn();
        } 
        
    }
}


