using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class LifePlaceDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static LifePlaceDAO instance = null;
        public static LifePlaceDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LifePlaceDAO();
                }
                return instance;
            }
        }

        public LifePlaceDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public LifePalace GetLifePlaceById(string id)
        {
            return dbContext.LifePalaces.SingleOrDefault(lp => lp.LifePalace1 == id);
        }

        public List<LifePalace> GetLifePlaces()
        {
            return dbContext.LifePalaces.ToList();
        }

        public bool AddLifePlace(LifePalace lifePlace)
        {
            bool isSuccess = false;
            LifePalace existingLifePlace = this.GetLifePlaceById(lifePlace.LifePalace1);
            try
            {
                if (existingLifePlace == null)
                {
                    dbContext.LifePalaces.Add(lifePlace);
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

        public bool DeleteLifePlace(string id)
        {
            bool isSuccess = false;
            LifePalace lifePlace = this.GetLifePlaceById(id);
            try
            {
                if (lifePlace != null)
                {
                    dbContext.LifePalaces.Remove(lifePlace);
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

        public bool UpdateLifePlace(LifePalace lifePlace)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<LifePalace>(lifePlace).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}