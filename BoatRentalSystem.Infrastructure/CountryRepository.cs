namespace BoatRentalSystem.Infrastructure
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CountryRepository : ICountryRepository
    {
        private readonly List<Country> _countries = new List<Country>();
        public CountryRepository()
        {
            _countries = new List<Country>
            { 
                new Country { Id = 1, Name = "Test1" },
                new Country { Id = 2, Name = "Test2" },
                new Country { Id = 3, Name = "Test3" },
            };
        }
        public async Task AddCountry(Country country)
        {
            country.Id = _countries.Any() ? _countries.Max(c => c.Id) + 1 : 1;
            _countries.Add(country);
            await Task.CompletedTask;
        }

        public async Task DeleteCountry(int id)
        {
            var country = _countries.FirstOrDefault(c => c.Id == id);
            if (country != null)
                _countries.Remove(country);

            await Task.CompletedTask;
        }
    
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await Task.FromResult(_countries);
        }


        public async Task<Country> GetCountryById(int id)
        {
            var country = _countries.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(country);
        }

        public async Task UpdateCountry(Country country)
        {
            var existingCountry = _countries.FirstOrDefault(x => x.Id == country.Id);
            if (existingCountry != null)
            {
                existingCountry.Name = country.Name;
            }
            await Task.CompletedTask;
        }
    }
}
