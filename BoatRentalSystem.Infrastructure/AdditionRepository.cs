namespace BoatRentalSystem.Infrastructure
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdditionRepository : IAdditionRepository
    {
        private readonly List<Addition> _additions = new List<Addition>();
        public AdditionRepository()
        {
            _additions = new List<Addition>
            { 
                new Addition { Id = 1, Name = "Test1" },
                new Addition { Id = 2, Name = "Test2" },
                new Addition { Id = 3, Name = "Test3" },
            };
        }
        public async Task AddAddition(Addition Addition)
        {
            Addition.Id = _additions.Any() ? _additions.Max(c => c.Id) + 1 : 1;
            _additions.Add(Addition);
            await Task.CompletedTask;
        }

        public async Task DeleteAddition(int id)
        {
            var Addition = _additions.FirstOrDefault(c => c.Id == id);
            if (Addition != null)
                _additions.Remove(Addition);

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Addition>> GetAllAdditions()
        {
            return await Task.FromResult(_additions);
        }


        public async Task<Addition> GetAdditionById(int id)
        {
            var Addition = _additions.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(Addition);
        }

        public async Task UpdateAddition(Addition Addition)
        {
            var existingAddition = _additions.FirstOrDefault(x => x.Id == Addition.Id);
            if (existingAddition != null)
            {
                existingAddition.Name = Addition.Name;
            }
            await Task.CompletedTask;
        }
    }
}
