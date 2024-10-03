using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class TypeColorDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static TypeColorDAO instance = null;

        public static TypeColorDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new TypeColorDAO();
                }
                return instance;
            }
        }

        public TypeColorDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public TypeColor GetPercentage(string color, string type)
        {
            return dbContext.TypeColors.SingleOrDefault(m => m.Color.Equals(color) && m.KoiType.Equals(type));
        }

        public List<TypeColor> GetAllType()
        {
            return dbContext.TypeColors.ToList();
        }
        public List<TypeColor> GetColorsAndPercentages(string koiType)
        {
            List<TypeColor> list = new List<TypeColor>();

            foreach (TypeColor item in this.GetAllType())
            {
                if (item.KoiType == koiType)
                    list.Add(item);
            }

            return list;
        }
        public List<TypeColor> GetTypeByColor(string color)
        {
            List<TypeColor> listType = new List<TypeColor>();

            foreach (TypeColor item in this.GetAllType())
            {
                if (item.ColorId == color)
                    listType.Add(item);
            }

            return listType;
        }
    }
}
