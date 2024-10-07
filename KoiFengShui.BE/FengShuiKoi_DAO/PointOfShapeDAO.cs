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
            return dbContext.PointOfShapes.FirstOrDefault(p => p.ElementId == element && p.ShapeId == shape);
        }
        public PointOfShape GetPointOfShapeByShapeID( string shape)
        {
            return dbContext.PointOfShapes.FirstOrDefault(p => p.ShapeId == shape);
        }

        public List<PointOfShape> GetPointOfShapes()
        {
            return dbContext.PointOfShapes.ToList();
        }

        public bool AddPointOfShape(PointOfShape pointOfShape)
        {
            bool isSuccess = false;
            PointOfShape existingPointOfShape = this.GetPointOfShape(pointOfShape.ElementId, pointOfShape.ElementId);
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
        public bool DeletePointOfShapeByShapeID(string shapeID)
        {
            try
            {
                
                var pointsToRemove = dbContext.PointOfShapes
                    .Where(pos => pos.ShapeId == shapeID)
                    .ToList();

                if (pointsToRemove.Any())
                {
                    
                    dbContext.PointOfShapes.RemoveRange(pointsToRemove);

                  
                    var shapeToRemove = dbContext.Shapes
                        .FirstOrDefault(s => s.ShapeId == shapeID);

                    if (shapeToRemove != null)
                    {
                        dbContext.Shapes.Remove(shapeToRemove);
                    }

                    
                    var elementShapesToRemove = dbContext.PointOfShapes
                        .Where(es => es.ShapeId == shapeID)
                        .ToList();

                    if (elementShapesToRemove.Any())
                    {
                        dbContext.PointOfShapes.RemoveRange(elementShapesToRemove);
                    }

                
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi xóa Shape và các thực thể liên quan: {ex.Message}", ex);
            }
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

		public List<PointOfShape> GetGoodShapeByElemnet(string element)
		{
			List<PointOfShape> listShape = new List<PointOfShape>();

			foreach (PointOfShape item in this.GetPointOfShapes())
			{
				if (item.Point >= 0.75 && item.ElementId.Equals(element))
					listShape.Add(item);
			}

			return listShape;
		}
	}
}