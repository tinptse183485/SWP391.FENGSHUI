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
        public bool DeletePointOfShapeByShapeID(string shapeID) => PointOfShapeDAO.Instance.DeletePointOfShapeByShapeID(shapeID);
        public bool UpdatePointOfShape(PointOfShape pointOfShape) => PointOfShapeDAO.Instance.UpdatePointOfShape(pointOfShape);
        public PointOfShape GetPointOfShapeByShapeID(string shape) => PointOfShapeDAO.Instance.GetPointOfShapeByShapeID(shape);
        public List<PointOfShape> GetGoodShapeByElemnet(string element) => PointOfShapeDAO.Instance.GetGoodShapeByElemnet(element);
	}
}