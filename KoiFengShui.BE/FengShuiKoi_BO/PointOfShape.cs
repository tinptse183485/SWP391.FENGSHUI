using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class PointOfShape
    {
        public double PointOfShape1 { get; set; }
        public string Element { get; set; } = null!;
        public string Shape { get; set; } = null!;

        public virtual Element ElementNavigation { get; set; } = null!;
        public virtual Shape ShapeNavigation { get; set; } = null!;
    }
}
