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
    public class PackageController : ControllerBase
    {

        private readonly PackageService _packageService;
        private readonly IMapper _mapper;
        public PackageController(IMapper mapper, PackageService packageService)
        {
            _mapper = mapper;
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageViewModel>>> Get()
        {
            var city = await _packageService.GetAllPackages();
            var cityViewModel = _mapper.Map<IEnumerable<PackageViewModel>>(city);

            return Ok(cityViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PackageViewModel>> GetById(int id)
        {
            var city = await _packageService.GetPackageById(id);
            if (city == null)
            {
                return NotFound();
            }
            var cityViewModel = _mapper.Map<PackageViewModel>(city);

            return Ok(cityViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddPackageViewModel addPackageViewModel)
        {
            var Package = _mapper.Map<Package>(addPackageViewModel);
            await _packageService.AddPackage(Package);
            return CreatedAtAction(nameof(Get), new { id = Package.Id }, addPackageViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PackageViewModel PackageViewModel)
        {
            var existingPackage = await _packageService.GetPackageById(PackageViewModel.Id);
            if (existingPackage == null)
            {
                return NotFound();
            }
            // Not Working (Why???????????????????????????)
            //var city = _mapper.Map<City>(cityViewModel);
            //await _packageService.UpdateCity(city);
            //return Ok(city);
            _mapper.Map(PackageViewModel, existingPackage);
            await _packageService.UpdatePackage(existingPackage);
            return Ok(existingPackage);

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCity = await _packageService.GetPackageById(id);
            if (existingCity == null)
            {
                return NotFound();
            }
            await _packageService.DeletePackage(id);
            return NoContent();
        }



    }
}
