using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class ShapeRepo : IShapeRepo
    {
        public Shape GetShapeById(string id) => ShapeDAO.Instance.GetShapeById(id);

        public List<Shape> GetShapes() => ShapeDAO.Instance.GetShapes();

        public bool AddShape(Shape shape) => ShapeDAO.Instance.AddShape(shape);

        public bool DeleteShape(string id) => ShapeDAO.Instance.DeleteShape(id);

        public bool UpdateShape(Shape shape) => ShapeDAO.Instance.UpdateShape(shape);
    }
}