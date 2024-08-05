namespace BoatRentalSystem.Application
{
    using BoatRentalSystem.Core.Entities;
    using BoatRentalSystem.Core.Interfaces;

    public class PackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public Task<IEnumerable<Package>> GetAllPackages()
        {
            return _packageRepository.GetAllPackages();
        }
        public Task<Package> GetPackageById(int id)
        {
            return _packageRepository.GetPackageById(id);

        }
        public Task AddPackage(Package package)
        {
            return _packageRepository.AddPackage(package);
        }

        public Task UpdatePackage(Package package)
        {
            return _packageRepository.UpdatePackage(package);
        }

        public Task DeletePackage(int id)
        {
            return _packageRepository.DeletePackage(id);
        }

    }
}
