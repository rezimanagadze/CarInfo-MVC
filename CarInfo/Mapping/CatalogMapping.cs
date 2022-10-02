using CarInfo.Entities;
using CarInfo.Models.CatalogModel;
using CarInfo.Interfaces;

namespace CarInfo.Mapping
{
    public class CatalogMapping:IMapper<Catalog, CatalogModel>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CatalogMapping(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }


        public CatalogModel MapFromEntityToModel(Catalog source) => new CatalogModel
        {
            Id = source.Id,
            Mark = source.Mark,
            Country = source.Country,
        };

        public Catalog MapFromModelToEntity(CatalogModel source)
        {
            var catalog = new Catalog();

            MapFromModelToEntity(source, catalog);

            return catalog;
        }


        public void MapFromModelToEntity(CatalogModel source, Catalog target)
        {
            string logo =UploadFile(source);

            target.Id = source.Id;
            target.Mark = source.Mark;
            target.Country = source.Country;
            target.Logo = logo;
        }

        public string UploadFile(CatalogModel catalog)
        {
            string uniqueFileName = null;

            if (catalog.Logo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + catalog.Logo.FileName;
                string filepath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    catalog.Logo.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
    }
}
