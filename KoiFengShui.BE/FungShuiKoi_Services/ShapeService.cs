using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class ShapeService : IShapeService
    {
        private IShapeRepo shapeRepo;

        public ShapeService()
        {
            shapeRepo = new ShapeRepo();
        }

        public Shape GetShapeById(string id) => shapeRepo.GetShapeById(id);

        public List<Shape> GetShapes() => shapeRepo.GetShapes();

        public bool AddShape(Shape shape) => shapeRepo.AddShape(shape);

        public bool DeleteShape(string id) => shapeRepo.DeleteShape(id);

        public bool UpdateShape(Shape shape) => shapeRepo.UpdateShape(shape);
    }
}