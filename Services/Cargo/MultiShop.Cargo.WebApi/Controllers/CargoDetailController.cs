using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrate;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }
        [HttpGet]
        public IActionResult CargoDetailResult()
        {
            var values = _cargoDetailService.TGetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreatCargoDetail(CreateCargoDetailDto createCargoDetail) // Dto
        {
           CargoDetail cargo = new CargoDetail()
           {
               Barcode = createCargoDetail.Barcode,
               ReceiverCustomer = createCargoDetail.ReceiverCustomer,
               SenderCustomer = createCargoDetail.SenderCustomer,
               CargoCompanyId = createCargoDetail.CargoCompanyId
           };

            _cargoDetailService.TInsert(cargo);
            return Ok("Kargo detayları başarı ile oluşturuldu.");
        }
        [HttpDelete]
        public IActionResult RemoveCargoDetail(int id)
        {
            _cargoDetailService.TDelete(id);
            return Ok("Kargo detayları başarı ile silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyById(int id)
        {
            var values = _cargoDetailService.TGetById(id);
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetail) //Dto 
        {
            CargoDetail cargo = new CargoDetail()
            {
                Barcode = updateCargoDetail.Barcode,
                ReceiverCustomer = updateCargoDetail.ReceiverCustomer,
                SenderCustomer = updateCargoDetail.SenderCustomer,
                CargoCompanyId = updateCargoDetail.CargoCompanyId,
                CargoDetailId = updateCargoDetail.CargoDetailId
            };
            _cargoDetailService.TUpdate(cargo);
            return Ok("Kargo detayları başarı ile güncellendi.");
        }
    }
}
