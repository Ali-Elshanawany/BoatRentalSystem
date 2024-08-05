namespace BoatRentalSystem.Core.Interfaces
{
    using BoatRentalSystem.Core.Entities;

    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int id);
        Task AddCountry(Country country);
        Task UpdateCountry(Country country);
        Task DeleteCountry(int id);
    }
}
