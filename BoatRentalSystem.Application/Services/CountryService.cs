namespace BoatRentalSystem.Application.Services
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;

    public class CountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Task<IEnumerable<Country>> GetAllCountries()
        {
            return _countryRepository.GetAllAsync();
        }
        public Task<Country> GetCountryById(int id)
        {
            return _countryRepository.GetByIdAsync(id);

        }
        public Task AddCountry(Country country)
        {
            return _countryRepository.AddAsync(country);
        }

        public Task UpdateCountry(Country country)
        {
            return _countryRepository.UpdateAsync(country.Id, country);
        }

        public Task DeleteCountry(int id)
        {
            return _countryRepository.DeleteAsync(id);
        }

    }
}
