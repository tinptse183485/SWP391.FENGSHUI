using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class QuantityDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static QuantityDAO instance = null;

        public static QuantityDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new QuantityDAO();
                }
                return instance;
            }
        }

        public QuantityDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Quantity getQuantityByElement(string element)
        {
            return dbContext.Quantities.SingleOrDefault(m => m.Element.Equals(element));
        }


    }
}
