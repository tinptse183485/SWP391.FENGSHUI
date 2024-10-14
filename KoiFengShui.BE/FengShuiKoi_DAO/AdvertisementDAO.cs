using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Advertisement> GetAdvertisementByAdID(string AdID)
        {
            return await dbContext.Advertisements.SingleOrDefaultAsync(m => m.AdId.Equals(AdID));
        }


        public List<Advertisement> GetAdvertisementByUserIdAndStatus(string userId, string status)
        {
            return dbContext.Advertisements
                .Where(m => m.UserId.Equals(userId) && m.Status.Equals(status))
                .ToList();
        }

           public async Task<List<Advertisement>> GetAdvertisements()
        {
            return await dbContext.Advertisements.ToListAsync();
        }

        public List<Advertisement> GetAdvertisementByUserID(string userdID)
        {
            return dbContext.Advertisements
                .Where(m => m.UserId.Equals(userdID))
                .ToList();
        }
        

            public async Task<List<Advertisement>> GetAdvertisementStatus(string status)
        {
            return await dbContext.Advertisements.Where(m => m.Status.Equals(status)).ToListAsync();
        }

        public async Task<List<Advertisement>> GetAdvertisementByUserIdAndStatus(string userId, string status)
        {
            return await dbContext.Advertisements.Where(m => m.UserId.Equals(userId) && m.Status.Equals(status)).ToListAsync();
        }

        public async Task<bool> AddAdvertisement(Advertisement advertisement)
        {
            try
            {
                await dbContext.Advertisements.AddAsync(advertisement);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAdvertisement(string adID)
        {
            try
            {
                var advertisement = await GetAdvertisementByAdID(adID);
                if (advertisement != null)
                {
                    dbContext.Advertisements.Remove(advertisement);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAdvertisement(Advertisement updatedAdvertisement)
        {
            bool isSuccess = false;

            try
            {
                var advertisement = await GetAdvertisementByAdID(updatedAdvertisement.AdId);
                if (advertisement != null)
                {
                    advertisement.Heading = updatedAdvertisement.Heading;
                    advertisement.Image = updatedAdvertisement.Image;
                    advertisement.Link = updatedAdvertisement.Link;
                    advertisement.Status = updatedAdvertisement.Status;
                    advertisement.ElementId = updatedAdvertisement.ElementId;
                    dbContext.Entry(advertisement).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }


        

        public List<Advertisement> GetExpiredAdvertisements()
        {
            return dbContext.Advertisements
                .Where(a => a.Status != "Expired" && a.AdsPackages.Any(ap => ap.ExpiredDate < DateTime.Now))
                .ToList();
        }
    }
}

