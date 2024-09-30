using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public interface IShapeService
    {
        Shape GetShapeById(string id);
        List<Shape> GetShapes();
        bool AddShape(Shape shape);
        bool DeleteShape(string id);
        bool UpdateShape(Shape shape);
    }
}