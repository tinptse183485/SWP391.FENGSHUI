using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class PointOfShapeService : IPointOfShapeService
    {
        private IPointOfShapeRepo pointOfShapeRepo;

        public PointOfShapeService()
        {
            pointOfShapeRepo = new PointOfShapeRepo();
        }

        public PointOfShape GetPointOfShape(string element, string shape) => pointOfShapeRepo.GetPointOfShape(element, shape);

        public List<PointOfShape> GetPointOfShapes() => pointOfShapeRepo.GetPointOfShapes();

        public bool AddPointOfShape(PointOfShape pointOfShape) => pointOfShapeRepo.AddPointOfShape(pointOfShape);

        public bool DeletePointOfShape(string element, string shape) => pointOfShapeRepo.DeletePointOfShape(element, shape);

        public bool UpdatePointOfShape(PointOfShape pointOfShape) => pointOfShapeRepo.UpdatePointOfShape(pointOfShape);
    }
}