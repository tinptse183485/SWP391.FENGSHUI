using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public interface ITypeColorService
    {
        public TypeColor GetPercentage(string color, string type);
    }
}
