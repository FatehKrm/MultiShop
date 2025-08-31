using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomerController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomerController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        [HttpGet]
        public IActionResult CargoCustomerResult()
        {
            var values = _cargoCustomerService.TGetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreatCargoCustomer(CreateCargoCustomerDto createCargoCustomer) // Dto
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                Name = createCargoCustomer.Name,
                Surname = createCargoCustomer.Surname,
                Phone = createCargoCustomer.Phone,
                Email = createCargoCustomer.Email,
                Address = createCargoCustomer.Address,
                District = createCargoCustomer.District,
                City = createCargoCustomer.City,
            };

            _cargoCustomerService.TInsert(cargoCustomer);
            return Ok("Kargo Müşterisi başarı ile oluşturuldu.");
        }
        [HttpDelete]
        public IActionResult RemoveCargoCustomer(int id)
        {
            _cargoCustomerService.TDelete(id);
            return Ok("Kargo Müşterisi başarı ile silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var values = _cargoCustomerService.TGetById(id);
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomer) //Dto 
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                CargoCustomerId = updateCargoCustomer.CargoCustomerId,
                Name = updateCargoCustomer.Name,
                Surname = updateCargoCustomer.Surname,
                Phone = updateCargoCustomer.Phone,
                Email = updateCargoCustomer.Email,
                Address = updateCargoCustomer.Address,
                District = updateCargoCustomer.District,
                City = updateCargoCustomer.City,
            };

            _cargoCustomerService.TUpdate(cargoCustomer);
            return Ok("Kargo Müşterisi başarı ile güncellendi.");
        }
    }
}
