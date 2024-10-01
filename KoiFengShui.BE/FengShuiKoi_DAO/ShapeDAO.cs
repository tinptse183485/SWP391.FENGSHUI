using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class ShapeDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static ShapeDAO instance = null;
        public static ShapeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShapeDAO();
                }
                return instance;
            }
        }

        public ShapeDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Shape GetShapeById(string id)
        {
            return dbContext.Shapes.SingleOrDefault(s => s.Shape1 == id);
        }

        public List<Shape> GetShapes()
        {
            return dbContext.Shapes.ToList();
        }

        public bool AddShape(Shape shape)
        {
            bool isSuccess = false;
            Shape existingShape = this.GetShapeById(shape.Shape1);
            try
            {
                if (existingShape == null)
                {
                    dbContext.Shapes.Add(shape);
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

        public bool DeleteShape(string id)
        {
            bool isSuccess = false;
            Shape shape = this.GetShapeById(id);
            try
            {
                if (shape != null)
                {
                    dbContext.Shapes.Remove(shape);
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

        public bool UpdateShape(Shape shape)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<Shape>(shape).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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