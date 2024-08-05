namespace BoatRentalSystem.Application
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;

    public class CountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService( ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Task<IEnumerable<Country>> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }
        public Task<Country> GetCountryById(int id)
        {
            return _countryRepository.GetCountryById(id);

        }
        public Task AddCountry(Country country)
        {
            return _countryRepository.AddCountry(country);
        }

        public Task UpdateCountry(Country country)
        {
            return _countryRepository.UpdateCountry(country);
        }

        public Task DeleteCountry(int id)
        {
            return _countryRepository.DeleteCountry(id);
        }

    }
}
