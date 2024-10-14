using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private IAdvertisementRepo iAdvertisementRepo;
        public AdvertisementService()
        {
            iAdvertisementRepo = new AdvertisementRepo();
        }
        public bool AddAdvertisement(Advertisement advertisement)
        {
            return iAdvertisementRepo.AddAdvertisement(advertisement);
        }

        public bool DeleteAdvertisement(string adID)
        {
            return iAdvertisementRepo.DeleteAdvertisement(adID);
        }
        public List<Advertisement> GetAdvertisementStatus(string status) =>iAdvertisementRepo.GetAdvertisementStatus(status);
        public Advertisement GetAdvertisementByAdID(string AdID)
        {
            return iAdvertisementRepo.GetAdvertisementByAdID(AdID);
        }
		public List<Advertisement> GetAdvertisementByUserID(string userdID) => iAdvertisementRepo.GetAdvertisementByUserID(userdID);
		public List<Advertisement> GetAdvertisementByUserIdAndStatus(string userId, string status)
		{
			return iAdvertisementRepo.GetAdvertisementByUserIdAndStatus(userId, status);
		}

		public List<Advertisement> GetAdvertisements()
        {
            return iAdvertisementRepo.GetAdvertisements();
        }

		public bool UpdateAdvertisement(Advertisement updatedAdvertisement)
		{
            return iAdvertisementRepo.UpdateAdvertisement(updatedAdvertisement);
        }

		public void UpdateExpiredAdvertisements()
		{
			var expiredAds = iAdvertisementRepo.GetExpiredAdvertisements();
			foreach (var ad in expiredAds)
			{
				ad.Status = "Expired";
				iAdvertisementRepo.UpdateAdvertisement(ad);
			}
		}

	}
}
