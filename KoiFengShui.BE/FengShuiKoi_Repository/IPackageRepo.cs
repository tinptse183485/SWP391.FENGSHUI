using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface IPackageRepo
    {
        Package GetPackageByRank(string rank);
        List<Package> GetPackages();
        bool AddPackage(Package package);
        bool DeletePackage(string rank);
        bool UpdatePackage(Package package);
    }
}