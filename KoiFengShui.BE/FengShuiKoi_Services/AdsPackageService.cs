using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
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
        public bool AddAdsPackage(AdsPackage ads)
        {
            return iAdsPackageRepo.AddAdsPackage(ads);
        }

        public bool DeleteAdsPackage(string AdID, string Rank)
        {
            return iAdsPackageRepo.DeleteAdsPackage(AdID, Rank);
        }

        public AdsPackage GetAdsPackageByAdIDRank(string AdID, string Rank)
        {
            return iAdsPackageRepo.GetAdsPackageByAdIDRank(AdID, Rank);
        }

        public List<AdsPackage> GetAdsPackages()
        {
            return iAdsPackageRepo.GetAdsPackages();
        }

        public List<AdsPackage> GetListAdsPackageByAdID(string AdID)
        {
           return iAdsPackageRepo.GetListAdsPackageByAdID(AdID); 
        }
		public List<AdsPackage> GetListAdsPackageByRank(string Rank)
        {
			return iAdsPackageRepo.GetListAdsPackageByRank(Rank);
		}
		public bool UpdateAdsPackage(AdsPackage newAdsPackage)
		{
            return iAdsPackageRepo.UpdateAdsPackage(newAdsPackage);
        }
    }
}
