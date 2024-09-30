using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class Element
    {
        public Element()
        {
            Advertisements = new HashSet<Advertisement>();
            ElementColors = new HashSet<ElementColor>();
            PointOfShapes = new HashSet<PointOfShape>();
            Shapes = new HashSet<Shape>();
        }

        public string Element1 { get; set; } = null!;
        public string Mutualism { get; set; } = null!;

        public virtual Quantity? Quantity { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<ElementColor> ElementColors { get; set; }
        public virtual ICollection<PointOfShape> PointOfShapes { get; set; }

        public virtual ICollection<Shape> Shapes { get; set; }
    }
}
