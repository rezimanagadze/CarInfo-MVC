using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CarInfo.Models.CatalogModel;

namespace CarInfo.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
            
        [HttpGet]
        public async Task<IActionResult> AllCatalog(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CountrySortParm"] = String.IsNullOrEmpty(sortOrder) ? "country_desc" : "";

            IEnumerable<Catalog> catalogs = await _catalogService.GetCatalogs(searchString);
            
            switch (sortOrder)
            {
                case "name_desc":
                    catalogs = catalogs.OrderBy(c => c.Mark);
                    break;
                default:
                    catalogs = catalogs.OrderBy(c => c.Country);
                    break;
            }

            return View(catalogs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogModel catalog)
        {
            
            CreateCatalogReturn createCatalogReturn = new CreateCatalogReturn();
            
            if(ModelState.IsValid)
            {
                createCatalogReturn=await _catalogService.CreateCatalog(catalog);
                return RedirectToAction("AllCatalog");
            }

            return View(createCatalogReturn.CreatedCatalog);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getCatalogResponse = await _catalogService.GetCatalog(new GetCatalogRequest() { Id = (Guid)id });

            if (getCatalogResponse == null)
            {
                return NotFound();
            }

            return View(getCatalogResponse.Catalog);
        }


        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(CatalogModel catalog)
        {
            if (ModelState.IsValid)
            {
                await _catalogService.UpdateCatalog(new UpdateCatalogRequest() { CatalogToUpdate = catalog });
                return RedirectToAction("AllCatalog");
            }

            return View(catalog);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteCatalogResponse = await _catalogService.GetCatalog(new GetCatalogRequest() { Id = (Guid)id });
            if (deleteCatalogResponse == null)
            {
                return NotFound();
            }

            return View(deleteCatalogResponse.Catalog);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid? id)
        {
            var getCatalogResponse = await _catalogService.GetCatalog(new GetCatalogRequest() { Id = (Guid)id });
            if (getCatalogResponse == null)
            {
                return NotFound();
            }
            await _catalogService.DeleteCatalog(new DeleteCatalogRequest() { Id = (Guid)id });
            return RedirectToAction("AllCatalog");

        }
    }
}

