using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class ElementColor
    {
        public string Element { get; set; } = null!;
        public string Color { get; set; } = null!;
        public double ColorPoint { get; set; }

        public virtual Color ColorNavigation { get; set; } = null!;
        public virtual Element ElementNavigation { get; set; } = null!;
    }
}
