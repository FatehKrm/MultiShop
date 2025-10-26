using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompanyController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompanyController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }
        [HttpGet]
        public IActionResult CargoCompanyResult()
        {
            var values = _cargoCompanyService.TGetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreatCargoCompany(CreateCargoCompanyDto createCargoCompany) // Dto
        {
           CargoCompany cargo = new CargoCompany()
           {
               CargoCompanyName = createCargoCompany.CargoCompanyName
           };

            _cargoCompanyService.TInsert(cargo);
            return Ok("Kargo şirketi başarı ile oluşturuldu.");
        }
        [HttpDelete]
        public IActionResult RemoveCargoCompany(int id)
        {
            _cargoCompanyService.TDelete(id);
            return Ok("Kargo şirketi başarı ile silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyById(int id)
        {
            var values = _cargoCompanyService.TGetById(id);
            return Ok(values);
        }
        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompany) //Dto 
        {
            CargoCompany cargo = new CargoCompany()
            {
                CargoCompanyId = updateCargoCompany.CargoCompanyId,
                CargoCompanyName = updateCargoCompany.CargoCompanyName,
            };
            _cargoCompanyService.TUpdate(cargo);
            return Ok("Kargo şirketi başarı ile güncellendi.");
        }
    }
}
