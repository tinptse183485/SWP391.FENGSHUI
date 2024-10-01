using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class TypeColorRepo : ITypeColorRepo
    {
        public TypeColor GetPercentage(string color, string type) => TypeColorDAO.Instance.GetPercentage(color, type);
      
    }
}
