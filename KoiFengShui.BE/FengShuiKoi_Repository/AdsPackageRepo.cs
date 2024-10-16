using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class AdsPackageRepo : IAdsPackageRepo
    {
        public Task<bool> AddAdsPackage(AdsPackage ads)
        {
            return AdsPackageDAO.Instance.AddAdsPackage(ads);
        }

        public Task<bool> DeleteAdsPackage(string AdID, string Rank, DateTime CreateAt)
        {
            return AdsPackageDAO.Instance.DeleteAdsPackage(AdID, Rank, CreateAt);
        }

        public Task<AdsPackage> GetAdsPackageByAdIDRankTime(string AdID, string Rank, DateTime CreateAt)
		{
            return AdsPackageDAO.Instance.GetAdsPackageByAdIDRankTime(AdID, Rank, CreateAt);
        }

        public Task<Dictionary<string, double>> GetRevenueByPackage()
        {
            return AdsPackageDAO.Instance.GetRevenueByPackage();
        }

        public Task<List<AdsPackage>> GetAdsPackages()
        {
            return AdsPackageDAO.Instance.GetAdsPackages();
        }

        public Task<List<AdsPackage>> GetListAdsPackageByAdID(string AdID)
        {
            return AdsPackageDAO.Instance.GetListAdsPackageByAdID(AdID);
        }

        public Task<List<AdsPackage>> GetListAdsPackageByRank(string Rank)
        {
            return AdsPackageDAO.Instance.GetListAdsPackageByRank(Rank);
        }

        public Task<bool> UpdateAdsPackage(AdsPackage newAdsPackage)
        {
            return AdsPackageDAO.Instance.UpdateAdsPackage(newAdsPackage);
        }
        public Task<Dictionary<string, double>> GetTotalRevenueByMonth(int year, int month)
        {
            return AdsPackageDAO.Instance.GetTotalRevenueByMonth(year, month);
        }
    }
}