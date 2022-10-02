using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CarInfo.Models.CarModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfo.Controllers
{
    public class ModelController : Controller
    {
        private readonly ICarModelService _carModelService;

        public ModelController(ICarModelService sedanService)
        {
            _carModelService = sedanService;
        }


        [HttpGet]
        public async Task<IActionResult> AllModel(string currentFilter, string searchString, string sortOrder)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewData["ModelSortParm"] = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            ViewData["YearsSortParm"] = String.IsNullOrEmpty(sortOrder) ? "years_desc" : "";


            IEnumerable<CarModel> sedans = await _carModelService.AllModel(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    sedans = sedans.OrderBy(s => s.Mark);
                    break;
                case "model_desc":
                    sedans = sedans.OrderBy(s => s.Model);
                    break;
                case "years_desc":
                    sedans = sedans.OrderBy(s => s.Years);
                    break;
                default:
                    sedans = sedans.OrderBy(s => s.CarTypes.Category);
                    break;
            }

            ViewBag.TypeSearch = _carModelService.EnumCategoryModel();
            
            return View(sedans);

        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Type = _carModelService.EnumCategoryModel();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarsModelModel sedan)
        {


            CreateCarModelReturn createsedanReturn = new CreateCarModelReturn();

            if (ModelState.IsValid)
            {
                createsedanReturn = await _carModelService.CreateModel(sedan);
                return RedirectToAction("AllModel");
            }

            return View(createsedanReturn.CreatedSedan);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewBag.EditType = _carModelService.EnumCategoryModel();

            if (id == null)
            {
                return NotFound();
            }

            var getSedanResponse = await _carModelService.GetModel(new GetCarModelRequest() { Id = (Guid)id });

            if (getSedanResponse == null)
            {
                return NotFound();
            }

            return View(getSedanResponse.Sedan);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(CarsModelModel sedan)
        {
            if (ModelState.IsValid)
            {
                await _carModelService.UpdateModel(new UpdateCarModelRequest() { SedanToUpdate = sedan });
                return RedirectToAction("AllModel");
            }

            return View(sedan);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteSedanResponse = await _carModelService.GetModel(new GetCarModelRequest() { Id = (Guid)id });
            if (deleteSedanResponse == null)
            {
                return NotFound();
            }

            return View(deleteSedanResponse.Sedan);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid? id)
        {
            var getSedanResponse = await _carModelService.GetModel(new GetCarModelRequest() { Id = (Guid)id });
            if (getSedanResponse == null)
            {
                return NotFound();
            }
            await _carModelService.DeleteModel(new DeleteCarModelRequest() { Id = (Guid)id });
            return RedirectToAction("AllModel");

        }
    }
}
