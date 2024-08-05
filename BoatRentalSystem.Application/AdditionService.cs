namespace BoatRentalSystem.Application
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;

    public class AdditionService
    {
        private readonly IAdditionRepository _additionRepository;

        public AdditionService(IAdditionRepository additionRepository)
        {
            _additionRepository = additionRepository;
        }

        public Task<IEnumerable<Addition>> GetAllAdditions()
        {
            return _additionRepository.GetAllAdditions();
        }
        public Task<Addition> GetAdditionById(int id)
        {
            return _additionRepository.GetAdditionById(id);

        }
        public Task AddAddition(Addition addition)
        {
            return _additionRepository.AddAddition(addition);
        }

        public Task UpdateAddition(Addition addition)
        {
            return _additionRepository.UpdateAddition(addition);
        }

        public Task DeleteAddition(int id)
        {
            return _additionRepository.DeleteAddition(id);
        }

    }
}
