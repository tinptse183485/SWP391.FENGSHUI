using FengShuiKoi_BO;
using FungShuiKoi_DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class KoiVarietyDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;

        private static KoiVarietyDAO instance = null;

        public static KoiVarietyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KoiVarietyDAO();
                }
                return instance;
            }
        }
        public KoiVarietyDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }
        public KoiVariety GetKoiVarietyByType(string type)
        {
            return dbContext.KoiVarieties.SingleOrDefault(m => m.KoiType.Equals(type));
        }

        public List<KoiVariety> GetKoiVarieties()
        {
            return dbContext.KoiVarieties.ToList();
        }

        public List<KoiVariety> GetKoiVarietiesByElemnet(string element)
        {
            List<KoiVariety> listKoi = new List<KoiVariety>();

            foreach (KoiVariety item in this.GetKoiVarieties())
            {
                if (item.Element == element)
                    listKoi.Add(item);
            }

            return listKoi;
        }

        public bool AddKoiVariety(KoiVariety variety)
        {
            bool isSuccess = false;
            KoiVariety koivariety = this.GetKoiVarietyByType(variety.KoiType);
            try
            {
                if (koivariety == null)
                {
                    dbContext.KoiVarieties.Add(variety);
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
        public bool DeleteKoiVariety(string type)
        {
            bool isSuccess = false;
            KoiVariety Koi = this.GetKoiVarietyByType(type);
            try
            {
                if (Koi != null)
                {
                    dbContext.KoiVarieties.Remove(Koi);
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
        public bool UpdateKoiVariety(KoiVariety updatedKoi)
        {
            try
            {
                var existingKoi = dbContext.KoiVarieties.FirstOrDefault(k => k.KoiType == updatedKoi.KoiType);
                if (existingKoi == null)
                {
                    return false; 
                }

               
                existingKoi.Image = updatedKoi.Image;
                existingKoi.Description = updatedKoi.Description;
                existingKoi.Element = updatedKoi.Element;

                dbContext.Entry(existingKoi).State = EntityState.Modified;
                int affectedRows = dbContext.SaveChanges();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error updating KoiVariety: {ex.Message}");
                return false;
            }
        }
    }
}
