using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public interface IAdsPackageRepo
    {
        Task<AdsPackage> GetAdsPackageByAdIDRank(string AdID, string Rank);
        Task<List<AdsPackage>> GetListAdsPackageByAdID(string AdID);
        Task<List<AdsPackage>> GetListAdsPackageByRank(string Rank);
        Task<List<AdsPackage>> GetAdsPackages();
        Task<bool> AddAdsPackage(AdsPackage ads);
        Task<bool> UpdateAdsPackage(AdsPackage newAdsPackage);
        Task<Dictionary<string, double>> GetRevenueByPackage();
        Task<bool> DeleteAdsPackage(string AdID, string Rank);
    }
}