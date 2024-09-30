using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class DirectionDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static DirectionDAO instance = null;
        public static DirectionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DirectionDAO();
                }
                return instance;
            }
        }

        public DirectionDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Direction GetDirectionById(string id)
        {
            return dbContext.Directions.SingleOrDefault(d => d.Direction1 == id);
        }

        public List<Direction> GetDirections()
        {
            return dbContext.Directions.ToList();
        }

        public bool AddDirection(Direction direction)
        {
            bool isSuccess = false;
            Direction existingDirection = this.GetDirectionById(direction.Direction1);
            try
            {
                if (existingDirection == null)
                {
                    dbContext.Directions.Add(direction);
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

        public bool DeleteDirection(string id)
        {
            bool isSuccess = false;
            Direction direction = this.GetDirectionById(id);
            try
            {
                if (direction != null)
                {
                    dbContext.Directions.Remove(direction);
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

        public bool UpdateDirection(Direction direction)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<Direction>(direction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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