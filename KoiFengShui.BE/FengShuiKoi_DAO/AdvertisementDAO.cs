using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class AdvertisementDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static AdvertisementDAO instance = null;
        public static AdvertisementDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new AdvertisementDAO();
                }
                return instance;
            }
        }
        public AdvertisementDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }
        public Advertisement GetAdvertisementByAdID(string AdID)
        {
            return dbContext.Advertisements.SingleOrDefault(m => m.AdId.Equals(AdID));
        }
     

        public List<Advertisement> GetAdvertisements()
        {
            return dbContext.Advertisements.ToList();
        }
        public bool AddAdvertisement(Advertisement advertisement)
        {
            bool isSuccess = false;
            Advertisement _advertisement = this.GetAdvertisementByAdID(advertisement.AdId);
            try
            {
                if (_advertisement == null)
                {
                    dbContext.Advertisements.Add(advertisement);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public bool DeleteAdvertisement(string adid)
        {
            bool isSuccess = false;
            Advertisement  advertisement = this.GetAdvertisementByAdID(adid);
            try
            {
                if (advertisement != null)
                {
                    dbContext.Advertisements.Remove(advertisement);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public bool UpdateAdvertisement(string adid)
        {
            bool isSuccess = false;
            Advertisement advertisement = this.GetAdvertisementByAdID(adid);
            try
            {
                if (advertisement != null)
                {
                    dbContext.Entry<Advertisement>(advertisement).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}

