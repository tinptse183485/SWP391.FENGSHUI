﻿using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class AdsPackageRepo : IAdsPackageRepo
    {
        public async Task<bool> AddAdsPackage(AdsPackage ads)
        {
            return await AdsPackageDAO.Instance.AddAdsPackage(ads);
        }

        public async Task<bool> DeleteAdsPackage(string AdID, string Rank, DateTime CreateAt)
        {
            return await AdsPackageDAO.Instance.DeleteAdsPackage(AdID, Rank, CreateAt);
        }

        public async Task<AdsPackage> GetAdsPackageByAdIDRankTime(string AdID, string Rank, DateTime CreateAt)
		{
            return await AdsPackageDAO.Instance.GetAdsPackageByAdIDRankTime(AdID, Rank, CreateAt);
        }

        public async Task<Dictionary<string, double>> GetRevenueByPackage()
        {
            return await AdsPackageDAO.Instance.GetRevenueByPackage();
        }

        public async Task<List<AdsPackage>> GetAdsPackages()
        {
            return await AdsPackageDAO.Instance.GetAdsPackages();
        }

        public async Task<List<AdsPackage>> GetListAdsPackageByAdID(string AdID)
        {
            return await AdsPackageDAO.Instance.GetListAdsPackageByAdID(AdID);
        }
        public async Task<AdsPackage> GetAdsPackageByAdID(string AdID) => await AdsPackageDAO.Instance.GetAdsPackageByAdID(AdID);

		public async  Task<List<AdsPackage>> GetListAdsPackageByRank(string Rank)
        {
            return await AdsPackageDAO.Instance.GetListAdsPackageByRank(Rank);
        }

        public async Task<bool> UpdateAdsPackage(AdsPackage newAdsPackage)
        {
            return await AdsPackageDAO.Instance.UpdateAdsPackage(newAdsPackage);
        }
        public async Task<Dictionary<string, double>> GetTotalRevenueByMonth(int year, int month)
        {
            return await AdsPackageDAO.Instance.GetTotalRevenueByMonth(year, month);
        }
        public async Task<Dictionary<DateTime, double>> GetDailyRevenueToDate()
        {
            return await AdsPackageDAO.Instance.GetDailyRevenueToDate();
        }
    }
}