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
        public TypeColor GetTypeByColorID(string colorId) => TypeColorDAO.Instance.GetTypeByColorID(colorId);
        public List<TypeColor> GetTypeByColor(string color) => TypeColorDAO.Instance.GetTypeByColor(color);
        public bool DeleteTypeColorByColorId(string colorId) => TypeColorDAO.Instance.DeleteTypeColorsByColorId(colorId);
        public bool AddKoiTypeColor(TypeColor koiFish) => TypeColorDAO.Instance.AddKoiTypeColor(koiFish);
        public bool DeleteTypeColorByKoiType(string KoiType) => TypeColorDAO.Instance.DeleteTypeColorsByKoiType(KoiType);
        public List<TypeColor> GetTypeByKoiType(string KoiType) => TypeColorDAO.Instance.GetTypeByKoiType(KoiType);
    }
}
