using FengShuiKoi_BO;
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

        public Advertisement GetAdvertisementByAdID(string AdID)
        {
            return iAdvertisementRepo.GetAdvertisementByAdID(AdID);
        }

        public List<Advertisement> GetAdvertisements()
        {
            return iAdvertisementRepo.GetAdvertisements();
        }

        public bool UpdateAdvertisement(string adID)
        {
            return iAdvertisementRepo.UpdateAdvertisement(adID);
        }
    }
}
