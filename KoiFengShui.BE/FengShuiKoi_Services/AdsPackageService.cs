using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class AdsPackageService : IAdsPackageService
    {
        private IAdsPackageRepo iAdsPackageRepo;
        public AdsPackageService()
        {
            iAdsPackageRepo = new AdsPackageRepo();
        }
        public async Task<bool> AddAdsPackage(AdsPackage ads)
        {
            return await iAdsPackageRepo.AddAdsPackage(ads);
        }
        public async Task<Dictionary<string, double>> GetRevenueByPackage()
        {
            return await iAdsPackageRepo.GetRevenueByPackage();
        }

        public async Task<bool> DeleteAdsPackage(string AdID, string Rank)
        {
            return await iAdsPackageRepo.DeleteAdsPackage(AdID, Rank);
        }

        public async Task<AdsPackage> GetAdsPackageByAdIDRank(string AdID, string Rank)
        {
            return await iAdsPackageRepo.GetAdsPackageByAdIDRank(AdID, Rank);
        }

        public async Task<List<AdsPackage>> GetAdsPackages()
        {
            return await iAdsPackageRepo.GetAdsPackages();
        }

        public async Task<List<AdsPackage>> GetListAdsPackageByAdID(string AdID)
        {
            return await iAdsPackageRepo.GetListAdsPackageByAdID(AdID);
        }
        public async Task<List<AdsPackage>> GetListAdsPackageByRank(string Rank)
        {
            return await iAdsPackageRepo.GetListAdsPackageByRank(Rank);
        }
        public async Task<bool> UpdateAdsPackage(AdsPackage newAdsPackage)
        {
            return await iAdsPackageRepo.UpdateAdsPackage(newAdsPackage);
        }
    }
}