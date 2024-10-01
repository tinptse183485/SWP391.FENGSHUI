using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
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
            return dbContext.TypeColors.SingleOrDefault(m => m.Color.Equals(color) && m.KoiType.Equals(type) );
        }

    }
}
