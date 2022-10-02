using CarInfo.Entities;
using CarInfo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CarInfo.Models.CarTypeModel;

namespace CarInfo.Controllers
{
    public class CarTypeController : Controller
    {
        private readonly ICarTypeService _carTypeService;

        public CarTypeController(ICarTypeService carTypeService)
        {
            _carTypeService = carTypeService;

        }

        [HttpGet]
        public async Task<IActionResult> AllCarType(string searchString)
        {
            IEnumerable<CarType> carType = await _carTypeService.GetCarTypes(searchString);

            return View(carType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarTypeModel carType)
        {
            CreateCarTypeReturn createCarTypeReturn = new CreateCarTypeReturn();

            if (ModelState.IsValid)
            {
                createCarTypeReturn = await _carTypeService.CreateCarType(carType);
                return RedirectToAction("AllCarType");
            }

            return View(createCarTypeReturn.CreatedCarType);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getCarTypeResponse = await _carTypeService.GetCarType(new GetCarTypeRequest() { Id = (Guid)id });

            if (getCarTypeResponse == null)
            {
                return NotFound();
            }

            return View(getCarTypeResponse.CarType);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(CarTypeModel carType)
        {
            if (ModelState.IsValid)
            {
                await _carTypeService.UpdateCarType(new UpdateCarTypeRequest() { CarTypeToUpdate = carType });
                return RedirectToAction("AllCarType");
            }

            return View(carType);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteCarTypeResponse = await _carTypeService.GetCarType(new GetCarTypeRequest() { Id = (Guid)id });
            if (deleteCarTypeResponse == null)
            {
                return NotFound();
            }

            return View(deleteCarTypeResponse.CarType);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid? id)
        {
            var getCarTypeResponse = await _carTypeService.GetCarType(new GetCarTypeRequest() { Id = (Guid)id });
            if (getCarTypeResponse == null)
            {
                return NotFound();
            }
            await _carTypeService.DeleteCarType(new DeleteCarTypeRequest() { Id = (Guid)id });
            return RedirectToAction("AllCarType");

        }
    }
}
