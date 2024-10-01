using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class TypeColor
    {
        public string KoiType { get; set; } = null!;
        public string Color { get; set; } = null!;
        public double Percentage { get; set; }

        public virtual Color ColorNavigation { get; set; } = null!;
        public virtual KoiVariety KoiTypeNavigation { get; set; } = null!;
    }
}
