namespace BoatRentalSystem.Infrastructure
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PackageRepository : IPackageRepository
    {
        private readonly List<Package> _packages = new List<Package>();
        public PackageRepository()
        {
            _packages = new List<Package>
            { 
                new Package { Id = 1, Name = "Test1" },
                new Package { Id = 2, Name = "Test2" },
                new Package { Id = 3, Name = "Test3" },
            };
        }
        public async Task AddPackage(Package Package)
        {
            Package.Id = _packages.Any() ? _packages.Max(c => c.Id) + 1 : 1;
            _packages.Add(Package);
            await Task.CompletedTask;
        }

        public async Task DeletePackage(int id)
        {
            var Package = _packages.FirstOrDefault(c => c.Id == id);
            if (Package != null)
                _packages.Remove(Package);

            await Task.CompletedTask;
        }
    
        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            return await Task.FromResult(_packages);
        }

        public async Task<Package> GetPackageById(int id)
        {
            var Package = _packages.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(Package);
        }

        public async Task UpdatePackage(Package Package)
        {
            var existingPackage = _packages.FirstOrDefault(x => x.Id == Package.Id);
            if (existingPackage != null)
            {
                existingPackage.Name = Package.Name;
            }
            await Task.CompletedTask;
        }
    }
}
