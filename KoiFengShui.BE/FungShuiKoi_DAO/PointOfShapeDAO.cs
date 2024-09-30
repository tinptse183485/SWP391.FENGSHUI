using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class PointOfShapeDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static PointOfShapeDAO instance = null;
        public static PointOfShapeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PointOfShapeDAO();
                }
                return instance;
            }
        }

        public PointOfShapeDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public PointOfShape GetPointOfShape(string element, string shape)
        {
            return dbContext.PointOfShapes.SingleOrDefault(p => p.Element == element && p.Shape == shape);
        }

        public List<PointOfShape> GetPointOfShapes()
        {
            return dbContext.PointOfShapes.ToList();
        }

        public bool AddPointOfShape(PointOfShape pointOfShape)
        {
            bool isSuccess = false;
            PointOfShape existingPointOfShape = this.GetPointOfShape(pointOfShape.Element, pointOfShape.Shape);
            try
            {
                if (existingPointOfShape == null)
                {
                    dbContext.PointOfShapes.Add(pointOfShape);
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

        public bool DeletePointOfShape(string element, string shape)
        {
            bool isSuccess = false;
            PointOfShape pointOfShape = this.GetPointOfShape(element, shape);
            try
            {
                if (pointOfShape != null)
                {
                    dbContext.PointOfShapes.Remove(pointOfShape);
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

        public bool UpdatePointOfShape(PointOfShape pointOfShape)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<PointOfShape>(pointOfShape).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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