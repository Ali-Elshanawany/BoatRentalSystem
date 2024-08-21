using AutoMapper;
using BoatRentalSystem.API.ViewModel;
using BoatRentalSystem.Application.Services;
using BoatRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatRentalSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionController : ControllerBase
    {
        private readonly AdditionService _additionService;
        private readonly IMapper _mapper;

        public AdditionController(AdditionService additionService, IMapper mapper)
        {
            _additionService = additionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionViewModel>>> Get()
        {
            var city = await _additionService.GetAllAdditions();
            var cityViewModel = _mapper.Map<IEnumerable<AdditionViewModel>>(city);

            return Ok(cityViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionViewModel>> GetById(int id)
        {
            var city = await _additionService.GetAdditionById(id);
            if (city == null)
            {
                return NotFound();
            }
            var cityViewModel = _mapper.Map<AdditionViewModel>(city);

            return Ok(cityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddAdditionViewModel addAdditionViewModel)
        {
            var Addition = _mapper.Map<Addition>(addAdditionViewModel);
            await _additionService.AddAddition(Addition);
            return CreatedAtAction(nameof(Get), new { id = Addition.Id }, addAdditionViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Put(AdditionViewModel AdditionViewModel)
        {
            var existingAddition = await _additionService.GetAdditionById(AdditionViewModel.Id);
            if (existingAddition == null)
            {
                return NotFound();
            }
            // Not Working (Why???????????????????????????)
            //var city = _mapper.Map<City>(cityViewModel);
            //await _additionService.UpdateCity(city);
            //return Ok(city);
            _mapper.Map(AdditionViewModel, existingAddition);
            await _additionService.UpdateAddition(existingAddition);
            return Ok(existingAddition);

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCity = await _additionService.GetAdditionById(id);
            if (existingCity == null)
            {
                return NotFound();
            }
            await _additionService.DeleteAddition(id);
            return NoContent();
        }


    }
}
