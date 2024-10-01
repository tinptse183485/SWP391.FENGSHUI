using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class LifePlaceDirectionDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static LifePlaceDirectionDAO instance = null;
        public static LifePlaceDirectionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LifePlaceDirectionDAO();
                }
                return instance;
            }
        }

        public LifePlaceDirectionDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public LifePalaceDirection GetLifePlaceDirectionById(string lifePalace, string direction)
        {
            return dbContext.LifePalaceDirections.SingleOrDefault(lpd => lpd.LifePalace == lifePalace && lpd.Direction == direction);
        }

        public List<LifePalaceDirection> GetLifePlaceDirections()
        {
            return dbContext.LifePalaceDirections.ToList();
        }

        public bool AddLifePlaceDirection(LifePalaceDirection lifePlaceDirection)
        {
            bool isSuccess = false;
            LifePalaceDirection existingLifePlaceDirection = this.GetLifePlaceDirectionById(lifePlaceDirection.LifePalace, lifePlaceDirection.Direction);
            try
            {
                if (existingLifePlaceDirection == null)
                {
                    dbContext.LifePalaceDirections.Add(lifePlaceDirection);
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

        public bool DeleteLifePlaceDirection(string lifePalace, string direction)
        {
            bool isSuccess = false;
            LifePalaceDirection lifePlaceDirection = this.GetLifePlaceDirectionById(lifePalace, direction);
            try
            {
                if (lifePlaceDirection != null)
                {
                    dbContext.LifePalaceDirections.Remove(lifePlaceDirection);
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

        public bool UpdateLifePlaceDirection(LifePalaceDirection lifePlaceDirection)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<LifePalaceDirection>(lifePlaceDirection).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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