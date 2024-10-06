using FengShuiKoi_BO;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public interface ITypeColorRepo
    {
        public TypeColor GetPercentage(string color, string type);
        public TypeColor GetTypeByColorID(string colorId) ;
        public List<TypeColor> GetAllType();
        public List<TypeColor> GetTypeByColor(string color);
        public List<TypeColor> GetColorsAndPercentages(string koiType);
    }
}
