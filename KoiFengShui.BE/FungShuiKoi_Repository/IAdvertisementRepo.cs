using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public interface IAdvertisementRepo
    {
       
        public Advertisement GetAdvertisementByAdID(string AdID);

        public List<Advertisement> GetAdvertisements();
        public bool AddAdvertisement(Advertisement advertisement);
        public bool DeleteAdvertisement(string adID);
        public bool UpdateAdvertisement(string adID);
    }
}
