using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class AdsPackageDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;

        private static AdsPackageDAO instance = null;

        public static AdsPackageDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new AdsPackageDAO();
                }
                return instance;
            }
        }
        public AdsPackageDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Dictionary<string, double> GetRevenueByPackage()
        {
            try
            {
                var adsPackages = dbContext.AdsPackages.AsNoTracking().ToList();
                var revenueByPackage = new Dictionary<string, double>();

                foreach (var package in adsPackages)
                {
                    if (!revenueByPackage.ContainsKey(package.Rank))
                    {
                        revenueByPackage[package.Rank] = 0;
                    }
                    revenueByPackage[package.Rank] += package.Total;
                }
                return revenueByPackage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong GetRevenueByPackage: {ex.Message}");
                return new Dictionary<string, double>();
            }
        }


        public List<AdsPackage> GetAdsPackages()
        {
            return dbContext.AdsPackages.ToList();
        }
        public AdsPackage GetAdsPackageByAdIDRank(string AdID, string Rank)
        {
            return dbContext.AdsPackages.SingleOrDefault(m => m.AdId.Equals(AdID) && m.Rank.Equals(Rank));
        }
        public List<AdsPackage> GetListAdsPackageByAdID(string AdID)
        {
            return dbContext.AdsPackages.Where(m => m.AdId.Equals(AdID)).ToList();
        }
        public List<AdsPackage> GetListAdsPackageByRank(string Rank)
        {
            return dbContext.AdsPackages.Where(m => m.Rank.Equals(Rank)).ToList();
        }
        public bool AddAdsPackage(AdsPackage ads)
        {
            bool isSuccess = false;
            AdsPackage adsPackage = this.GetAdsPackageByAdIDRank(ads.AdId, ads.Rank);
            try
            {
                if (adsPackage == null)
                {
                    dbContext.AdsPackages.Add(ads);
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
        public bool UpdateAdsPackage(AdsPackage newAdsPackage)
        {
            bool isSuccess = false;
            AdsPackage ads = this.GetAdsPackageByAdIDRank(newAdsPackage.AdId, newAdsPackage.Rank);
            ads.StartDate = newAdsPackage.StartDate;
            ads.ExpiredDate = newAdsPackage.ExpiredDate;
            ads.Quantity = newAdsPackage.Quantity;
            ads.Total = newAdsPackage.Total;
            try
            {
                if (ads != null)
                {
                    dbContext.Entry<AdsPackage>(ads).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool DeleteAdsPackage(string AdID, string Rank)
        {
            bool isSuccess = false;
            AdsPackage ads = this.GetAdsPackageByAdIDRank(AdID, Rank);
            try
            {
                if (ads != null)
                {
                    dbContext.AdsPackages.Remove(ads);
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


    }
}
