using FengShuiKoi_BO;
using FungShuiKoi_DAO;
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
        public bool UpdateKoiVariety(string type)
        {
            bool isSuccess = false;
            KoiVariety koi = this.GetKoiVarietyByType(type);
            try
            {
                if (koi != null)
                {
                    dbContext.Entry<KoiVariety>(koi).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
