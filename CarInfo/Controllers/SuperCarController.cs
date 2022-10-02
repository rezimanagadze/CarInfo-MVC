using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CarInfo.Models.SuperCarModel;

namespace CarInfo.Controllers
{
    public class SuperCarController : Controller
    {
        private readonly ISuperCarService _superCarService;

        public SuperCarController(ISuperCarService superCarService)
        {
            _superCarService = superCarService;
        }

        [HttpGet]
        public async Task<IActionResult> AllSuperCar(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ModelSortParm"] = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            ViewData["CategorySortParm"] = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewData["YearsSortParm"] = String.IsNullOrEmpty(sortOrder) ? "years_desc" : "";
            ViewData["EngineSortParm"] = String.IsNullOrEmpty(sortOrder) ? "engine_desc" : "";
            ViewData["HorsePowerSortParm"] = String.IsNullOrEmpty(sortOrder) ? "horsePower_desc" : "";
            ViewData["TopSpeedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "topSpeed_desc" : "";
            ViewData["AccelerationSortParm"] = String.IsNullOrEmpty(sortOrder) ? "acceleration_desc" : "";
            ViewData["ConsumptionSortParm"] = String.IsNullOrEmpty(sortOrder) ? "consumption_desc" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";


            IEnumerable<SuperCar> SuperCars = await _superCarService.GetSuperCars(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Mark);
                    break;
                case "model_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Model);
                    break;
                case "category_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Category);
                    break;
                case "years_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Years);
                    break;
                case "engine_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Engine);
                    break;
                case "horsePower_desc":
                    SuperCars = SuperCars.OrderBy(c => c.HorsePower);
                    break;
                case "topSpeed_desc":
                    SuperCars = SuperCars.OrderBy(c => c.TopSpeed);
                    break;
                case "acceleration_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Acceleration);
                    break;
                case "price_desc":
                    SuperCars = SuperCars.OrderBy(c => c.Price);
                    break;
                default:
                    SuperCars = SuperCars.OrderBy(c => c.Consumption);
                    break;
            };

            ViewBag.category = _superCarService.EnumCategorySuperCar();

            return View(SuperCars);
        }


        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.category =_superCarService.EnumCategorySuperCar();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuperCarModel SuperCar)
        {

            CreateSuperCarReturn createSuperCarReturn = new CreateSuperCarReturn();

            if (ModelState.IsValid)
            {
                createSuperCarReturn = await _superCarService.CreateSuperCar(SuperCar);
                return RedirectToAction("AllSuperCar");
            }

            return View(createSuperCarReturn.CreatedSuperCar);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {

            ViewBag.Category = _superCarService.EnumCategorySuperCar();

            if (id == null)
            {
                return NotFound();
            }

            var getSuperCarResponse = await _superCarService.GetSuperCar(new GetSuperCarRequest() { Id = (Guid)id });

            if (getSuperCarResponse == null)
            {
                return NotFound();
            }

            return View(getSuperCarResponse.SuperCar);
        }


        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(SuperCarModel SuperCar)
        {
            if (ModelState.IsValid)
            {
                await _superCarService.UpdateSuperCar(new UpdateSuperCarRequest() { SuperCarToUpdate = SuperCar });
                return RedirectToAction("AllSuperCar");
            }

            return View(SuperCar);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteSuperCarResponse = await _superCarService.GetSuperCar(new GetSuperCarRequest() { Id = (Guid)id });
            if (deleteSuperCarResponse == null)
            {
                return NotFound();
            }

            return View(deleteSuperCarResponse.SuperCar);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid? id)
        {
            var getSuperCarResponse = await _superCarService.GetSuperCar(new GetSuperCarRequest() { Id = (Guid)id });
            if (getSuperCarResponse == null)
            {
                return NotFound();
            }
            await _superCarService.DeleteSuperCar(new DeleteSuperCarRequest() { Id = (Guid)id });
            return RedirectToAction("AllSuperCar");

        }
    }
}

