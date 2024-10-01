using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class PackageRepo : IPackageRepo
    {
        public Package GetPackageByRank(string rank) => PackageDAO.Instance.GetPackageByRank(rank);

        public List<Package> GetPackages() => PackageDAO.Instance.GetPackages();

        public bool AddPackage(Package package) => PackageDAO.Instance.AddPackage(package);

        public bool DeletePackage(string rank) => PackageDAO.Instance.DeletePackage(rank);

        public bool UpdatePackage(Package package) => PackageDAO.Instance.UpdatePackage(package);
    }
}