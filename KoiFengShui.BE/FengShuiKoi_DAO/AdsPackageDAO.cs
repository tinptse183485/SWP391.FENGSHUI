using FengShuiKoi_BO;
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
        
        

        public List<AdsPackage> GetAdsPackages()
        {
            return dbContext.AdsPackages.ToList();
        }
        public AdsPackage GetAdsPackageByAdIDRank(string AdID, string Rank)
        {
            return dbContext.AdsPackages.SingleOrDefault(m => m.AdId.Equals(AdID) && m.Rank.Equals(Rank));
        }

        public bool AddAdsPackage(AdsPackage ads)
        {
            bool isSuccess = false;
            AdsPackage adsPackage = this.GetAdsPackageByAdIDRank(ads.AdId,ads.Rank);
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
        public bool UpdateAdsPackage(string AdID, string Rank)
        {
            bool isSuccess = false;
            AdsPackage ads = this.GetAdsPackageByAdIDRank(AdID, Rank);
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
            AdsPackage ads = this.GetAdsPackageByAdIDRank(AdID,Rank);
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
