using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }
        [HttpGet]
        public IActionResult CargoOperationResult()
        {
            var values = _cargoOperationService.TGetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreatCargoOperation(CreateCargoOperationDto createCargoOperation) // Dto
        {

            CargoOperation cargo = new CargoOperation()
            {
                Barcode = createCargoOperation.Barcode,
                Description = createCargoOperation.Description,
                OperationDate = createCargoOperation.OperationDate,
            };

            _cargoOperationService.TInsert(cargo);
            return Ok("Kargo Operasyonu başarı ile oluşturuldu.");
        }
        [HttpDelete]
        public IActionResult RemoveCargoOperation(int id)
        {
            _cargoOperationService.TDelete(id);
            return Ok("Kargo Operasyonu başarı ile silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoOperationById(int id)
        {
            var values = _cargoOperationService.TGetById(id);
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperation) //Dto 
        {
            CargoOperation cargo = new CargoOperation()
            {
                Barcode = updateCargoOperation.Barcode,
                Description = updateCargoOperation.Description,
                OperationDate = updateCargoOperation.OperationDate,
                CargoOperationId = updateCargoOperation.CargoOperationId
            };
            _cargoOperationService.TUpdate(cargo);
            return Ok("Kargo Operasyonu başarı ile güncellendi.");
        }
    }
}
