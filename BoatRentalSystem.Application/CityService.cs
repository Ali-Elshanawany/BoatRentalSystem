namespace BoatRentalSystem.Application
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;

    public class CityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public Task<IEnumerable<City>> GetAllCities()
        {
            return _cityRepository.GetAllCities();
        }
        public Task<City> GetCityById(int id)
        {
            return _cityRepository.GetCityById(id);

        }
        public Task AddCity(City city)
        {
            return _cityRepository.AddCity(city);
        }

        public Task UpdateCity(City city)
        {
            return _cityRepository.UpdateCity(city);
        }

        public Task DeleteCity(int id)
        {
            return _cityRepository.DeleteCity(id);
        }

    }
}
