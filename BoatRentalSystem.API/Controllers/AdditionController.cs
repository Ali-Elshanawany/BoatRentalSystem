using BoatRentalSystem.Application;
using BoatRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatRentalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionController : ControllerBase
    {
        private readonly AdditionService _additionService;

        public AdditionController(AdditionService additionService)
        {
            _additionService = additionService;
        }

        [HttpGet]
        public Task<IEnumerable<Addition>> Get()
        {
            return _additionService.GetAllAdditions();
        }
           

        [HttpGet("{id}")]

        public Task<Addition> Get(int id)
        {
            return _additionService.GetAdditionById(id);
        }

        [HttpPost]
        public Task Post(Addition addition)
        {
            return _additionService.AddAddition(addition);  
        }

        [HttpPut]
            public Task Put(Addition addition)
        {
                return _additionService.UpdateAddition(addition);

        }
        [HttpDelete]

        public Task Delete(int id)
        {
            return _additionService.DeleteAddition(id);
            }
    }
}
