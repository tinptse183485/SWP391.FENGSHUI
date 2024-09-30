using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class ElementDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static ElementDAO instance = null;

        public static ElementDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new ElementDAO();
                }
                return instance;
            }
        }

        public ElementDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Element GetElementAndMutualism(string element)
        {
            return dbContext.Elements.SingleOrDefault(m => m.Element1.Equals(element));
        }


    
    }
}
