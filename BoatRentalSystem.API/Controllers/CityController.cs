namespace BoatRentalSystem.API.Controllers
{
    using BoatRentalSystem.Application;
    using BoatRentalSystem.Core.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public Task<IEnumerable<City>> Get()
        {
            return _cityService.GetAllCities();
        }

        [HttpGet("{id}")]
        public Task<City> Get(int id)
        {
            return _cityService.GetCityById(id);
        }

        [HttpPost]
        public Task Post(City city)
        {
            return _cityService.AddCity(city);
        }

        [HttpPut]
        public Task Put(City city)
        {
            return _cityService.UpdateCity(city);

        }
        [HttpDelete]
        public Task Delete(int id)
        {
            return _cityService.DeleteCity(id);
        }
    }
}
