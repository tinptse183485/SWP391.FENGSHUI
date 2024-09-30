using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class AdsPackageRepo : IAdsPackageRepo
    {
        public bool AddAdsPackage(AdsPackage ads)
        {
           return AdsPackageDAO.Instance.AddAdsPackage(ads);
        }

        public bool DeleteAdsPackage(string AdID, string Rank)
        {
            return AdsPackageDAO.Instance.DeleteAdsPackage(AdID, Rank);
        }

        public AdsPackage GetAdsPackageByAdIDRank(string AdID, string Rank)
        {
            return AdsPackageDAO.Instance.GetAdsPackageByAdIDRank(AdID, Rank);
        }

        public List<AdsPackage> GetAdsPackages()
        {
            return AdsPackageDAO.Instance.GetAdsPackages();
        }

        public bool UpdateAdsPackage(string AdID, string Rank)
        {
            return AdsPackageDAO.Instance.UpdateAdsPackage(AdID, Rank);
        }
    }
}
