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
            return dbContext.LifePalaceDirections.SingleOrDefault(lpd => lpd.LifePalaceId == lifePalace && lpd.DirectionId == direction);
        }

        public List<LifePalaceDirection> GetLifePlaceDirections()
        {
            return dbContext.LifePalaceDirections.ToList();
        }

        public bool AddLifePlaceDirection(LifePalaceDirection lifePlaceDirection)
        {
            bool isSuccess = false;
            LifePalaceDirection existingLifePlaceDirection = this.GetLifePlaceDirectionById(lifePlaceDirection.LifePalaceId, lifePlaceDirection.DirectionId);
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


        public List<LifePalaceDirection> GetGoodDirectionByLifePalace(string LifePalace)
        {
            List<LifePalaceDirection> listDirection = new List<LifePalaceDirection>();

            foreach (LifePalaceDirection item in this.GetLifePlaceDirections())
            {
                if (item.PointOfDirection == 1 && item.LifePalaceId.Equals(LifePalace))
                    listDirection.Add(item);
            }
            return listDirection;
        }

        public List<(string EightMansions, string Description)> GetEightMansionsAndDescriptions()
            {
          
            List<string> sortOrder = new List<string>
    {
        "Sinh khí", "Thiên y", "Diên niên", "Phục vị",
        "Tuyệt mệnh", "Ngũ quỷ", "Lục sát", "Họa hại"
    };

           
            Dictionary<string, string> uniquePairs = new Dictionary<string, string>();

            foreach (LifePalaceDirection item in this.GetLifePlaceDirections())
            {
               
                uniquePairs[item.EightMansions] = item.Description;
                if (uniquePairs.Count == 8)
                    break;
            }

            
            return sortOrder
                .Where(mansion => uniquePairs.ContainsKey(mansion))
                .Select(mansion => (mansion, uniquePairs[mansion]))
                .ToList();
        }
    }

}
