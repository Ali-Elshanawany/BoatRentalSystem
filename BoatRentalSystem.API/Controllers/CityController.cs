namespace BoatRentalSystem.API.Controllers
{
    using AutoMapper;
    using BoatRentalSystem.API.ViewModel;
    using BoatRentalSystem.Application.Services;
    using BoatRentalSystem.Core.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;
        public CityController(CityService cityService, IMapper mapper, ILogger<CityController> logger)
        {
            _cityService = cityService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityViewModel>>> Get()
        {
            //_logger.LogInformation("Hello From City Controller ");
            //_logger.LogWarning("Debug");
           
            var city = await _cityService.GetAllCities();
            var cityViewModel = _mapper.Map<IEnumerable<CityViewModel>>(city);

            return Ok(cityViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityViewModel>> GetById(int id)
        {
            var city = await _cityService.GetCityById(id);
            if (city == null)
            {
                return NotFound();
            }
            var cityViewModel = _mapper.Map<CityViewModel>(city);

            return Ok(cityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCityViewModel addCityViewModel)
        {
            var city = _mapper.Map<City>(addCityViewModel);
            await _cityService.AddCity(city);
            return CreatedAtAction(nameof(Get), new { id = city.Id }, addCityViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CityViewModel cityViewModel)
        {
            var existingCity = await _cityService.GetCityById(cityViewModel.Id);
            if (existingCity == null)
            {
                return NotFound();
            }
            // Not Working (Why???????????????????????????)
            //var city = _mapper.Map<City>(cityViewModel);
            //await _cityService.UpdateCity(city);
            //return Ok(city);
            _mapper.Map(cityViewModel, existingCity);
            await _cityService.UpdateCity(existingCity);
            return Ok(existingCity);

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCity = await _cityService.GetCityById(id);
            if (existingCity == null)
            {
                return NotFound();
            }
            await _cityService.DeleteCity(id);
            return NoContent();
        }


        //Testing Serilog
        //
        [HttpGet("test")]
        public  IActionResult Test()
        {
            //try
            //{
            //    throw new NotImplementedException();
            //}catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}
            _logger.LogInformation("Hello From City Controller ");
            //_logger.LogWarning("Debug");

            return Ok("OK");
        }
    }
    
}
