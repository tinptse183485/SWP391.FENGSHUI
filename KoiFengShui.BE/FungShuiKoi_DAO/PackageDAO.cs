using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class PackageDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static PackageDAO instance = null;
        public static PackageDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PackageDAO();
                }
                return instance;
            }
        }

        public PackageDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Package GetPackageByRank(string rank)
        {
            return dbContext.Packages.SingleOrDefault(p => p.Rank == rank);
        }

        public List<Package> GetPackages()
        {
            return dbContext.Packages.ToList();
        }

        public bool AddPackage(Package package)
        {
            bool isSuccess = false;
            Package existingPackage = this.GetPackageByRank(package.Rank);
            try
            {
                if (existingPackage == null)
                {
                    dbContext.Packages.Add(package);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }

        public bool DeletePackage(string rank)
        {
            bool isSuccess = false;
            Package package = this.GetPackageByRank(rank);
            try
            {
                if (package != null)
                {
                    dbContext.Packages.Remove(package);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }

        public bool UpdatePackage(Package package)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<Package>(package).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}