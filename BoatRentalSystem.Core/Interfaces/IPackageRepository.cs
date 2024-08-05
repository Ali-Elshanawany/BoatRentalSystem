using BoatRentalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRentalSystem.Core.Interfaces
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackages();
        Task<Package> GetPackageById(int id);
        Task AddPackage(Package package);
        Task UpdatePackage(Package package);
        Task DeletePackage(int id);

    }
}
