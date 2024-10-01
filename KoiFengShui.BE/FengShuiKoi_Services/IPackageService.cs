using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public interface IPackageService
    {
        Package GetPackageByRank(string rank);
        List<Package> GetPackages();
        bool AddPackage(Package package);
        bool DeletePackage(string rank);
        bool UpdatePackage(Package package);
    }
}