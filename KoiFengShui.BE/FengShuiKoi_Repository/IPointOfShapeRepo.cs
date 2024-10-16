﻿using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface IPointOfShapeRepo
    {
        PointOfShape GetPointOfShape(string element, string shape);
        List<PointOfShape> GetPointOfShapes();
        bool AddPointOfShape(PointOfShape pointOfShape);
        bool DeletePointOfShape(string element, string shape);
        bool UpdatePointOfShape(PointOfShape pointOfShape);
        public List<PointOfShape> GetGoodShapeByElemnet(string element);
        public PointOfShape GetPointOfShapeByShapeID(string shape);
        public bool DeletePointOfShapeByShapeID(string shapeID);

    }
}