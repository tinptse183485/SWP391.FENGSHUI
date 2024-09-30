using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class PointOfShapeRepo : IPointOfShapeRepo
    {
        public PointOfShape GetPointOfShape(string element, string shape) => PointOfShapeDAO.Instance.GetPointOfShape(element, shape);

        public List<PointOfShape> GetPointOfShapes() => PointOfShapeDAO.Instance.GetPointOfShapes();

        public bool AddPointOfShape(PointOfShape pointOfShape) => PointOfShapeDAO.Instance.AddPointOfShape(pointOfShape);

        public bool DeletePointOfShape(string element, string shape) => PointOfShapeDAO.Instance.DeletePointOfShape(element, shape);

        public bool UpdatePointOfShape(PointOfShape pointOfShape) => PointOfShapeDAO.Instance.UpdatePointOfShape(pointOfShape);
    }
}