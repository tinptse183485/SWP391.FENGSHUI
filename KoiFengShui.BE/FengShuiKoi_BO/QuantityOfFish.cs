﻿using System;
using System.Collections.Generic;

namespace FengShuiKoi_BO
{
    public partial class QuantityOfFish
    {
        public string Element { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual Element ElementNavigation { get; set; } = null!;
    }
}