using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class QuantityOfFishDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static QuantityOfFishDAO instance = null;

        public static QuantityOfFishDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new QuantityOfFishDAO();
                }
                return instance;
            }
        }

        public QuantityOfFishDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public QuantityOfFish getQuantityByElement(string element)
        {
            return dbContext.QuantityOfFishes.SingleOrDefault(m => m.Element.Equals(element));
        }


    }
}
