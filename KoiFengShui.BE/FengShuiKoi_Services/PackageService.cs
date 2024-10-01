using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class PackageService : IPackageService
    {
        private IPackageRepo packageRepo;

        public PackageService()
        {
            packageRepo = new PackageRepo();
        }

        public Package GetPackageByRank(string rank) => packageRepo.GetPackageByRank(rank);

        public List<Package> GetPackages() => packageRepo.GetPackages();

        public bool AddPackage(Package package) => packageRepo.AddPackage(package);

        public bool DeletePackage(string rank) => packageRepo.DeletePackage(rank);

        public bool UpdatePackage(Package package) => packageRepo.UpdatePackage(package);
    }
}