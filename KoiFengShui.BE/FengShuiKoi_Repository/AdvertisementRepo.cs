using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class AdvertisementRepo : IAdvertisementRepo
    {
        public bool AddAdvertisement(Advertisement advertisement)
        {
            return AdvertisementDAO.Instance.AddAdvertisement(advertisement);
        }

        public bool DeleteAdvertisement(string adID)
        {
            return AdvertisementDAO.Instance.DeleteAdvertisement(adID);
        }

        public Advertisement GetAdvertisementByAdID(string AdID)
        {
            return AdvertisementDAO.Instance.GetAdvertisementByAdID(AdID);
        }

        public List<Advertisement> GetAdvertisements()
        {
            return AdvertisementDAO.Instance.GetAdvertisements();
        }

        public bool UpdateAdvertisement(string adID)
        {
            return AdvertisementDAO.Instance.UpdateAdvertisement(adID);
        }
    }
}
