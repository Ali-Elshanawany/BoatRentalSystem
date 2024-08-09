namespace BoatRentalSystem.Infrastructure.Repositories
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PackageRepository : BaseRepository<Package>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
