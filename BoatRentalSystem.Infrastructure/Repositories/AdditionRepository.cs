namespace BoatRentalSystem.Infrastructure.Repositories
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdditionRepository : BaseRepository<Addition>, IAdditionRepository
    {
        public AdditionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
