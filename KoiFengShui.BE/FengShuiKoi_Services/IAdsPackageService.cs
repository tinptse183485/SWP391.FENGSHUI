using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public interface IAdsPackageService
    {
		Task<AdsPackage> GetAdsPackageByAdIDRankTime(string AdID, string Rank, DateTime CreateAt);
        Task<List<AdsPackage>> GetListAdsPackageByAdID(string AdID);
        Task<List<AdsPackage>> GetListAdsPackageByRank(string Rank);
        Task<Dictionary<string, double>> GetRevenueByPackage();
        Task<List<AdsPackage>> GetAdsPackages();
        Task<bool> AddAdsPackage(AdsPackage ads);
        Task<bool> UpdateAdsPackage(AdsPackage newAdsPackage);
		Task<bool> DeleteAdsPackage(string AdID, string Rank, DateTime CreateAt);
    }
}