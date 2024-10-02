using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class TypeColorRepo : ITypeColorRepo
    {
        public List<TypeColor> GetAllType() => TypeColorDAO.Instance.GetAllType();


        public List<TypeColor> GetColorsAndPercentages(string koiType) => TypeColorDAO.Instance.GetColorsAndPercentages(koiType);
        public TypeColor GetPercentage(string color, string type) => TypeColorDAO.Instance.GetPercentage(color, type);

        public List<TypeColor> GetTypeByColor(string color)  => TypeColorDAO.Instance.GetTypeByColor(color);
    }
}
