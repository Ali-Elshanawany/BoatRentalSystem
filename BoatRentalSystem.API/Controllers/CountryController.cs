using AutoMapper;
using BoatRentalSystem.API.ViewModel;
using BoatRentalSystem.Application.Services;
using BoatRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatRentalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly CountryService _countryService;
        private readonly IMapper _mapper;
        public CountryController(CountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryViewModel>>> Get()
        {
            var city = await _countryService.GetAllCountries();
            var cityViewModel = _mapper.Map<IEnumerable<CountryViewModel>>(city);

            return Ok(cityViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryViewModel>> GetById(int id)
        {
            var city = await _countryService.GetCountryById(id);
            if (city == null)
            {
                return NotFound();
            }
            var cityViewModel = _mapper.Map<CountryViewModel>(city);

            return Ok(cityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddCountryViewModel addcountryViewModel)
        {
            var country = _mapper.Map<Country>(addcountryViewModel);
            await _countryService.AddCountry(country);
            return CreatedAtAction(nameof(Get), new { id = country.Id }, addcountryViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CountryViewModel countryViewModel)
        {
            var existingCountry = await _countryService.GetCountryById(countryViewModel.Id);
            if (existingCountry == null)
            {
                return NotFound();
            }
            // Not Working (Why???????????????????????????)
            //var city = _mapper.Map<City>(cityViewModel);
            //await _countryService.UpdateCity(city);
            //return Ok(city);
            _mapper.Map(countryViewModel, existingCountry);
            await _countryService.UpdateCountry(existingCountry);
            return Ok(existingCountry);

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCity = await _countryService.GetCountryById(id);
            if (existingCity == null)
            {
                return NotFound();
            }
            await _countryService.DeleteCountry(id);
            return NoContent();
        }



    }
}
