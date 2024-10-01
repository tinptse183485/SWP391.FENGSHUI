using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class LifePalaceDirection
    {
        public string LifePalace { get; set; } = null!;
        public string Direction { get; set; } = null!;
        public string EightMansions { get; set; } = null!;
        public double PointOfDirection { get; set; }
        public string Description { get; set; } = null!;

        public virtual Direction DirectionNavigation { get; set; } = null!;
        public virtual LifePalace LifePalaceNavigation { get; set; } = null!;
    }
}
