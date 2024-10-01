using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class QuantityService : IQuantityService
    {
        private IQuantityRepo iquantityRepo;
        public QuantityService()
        {
            iquantityRepo = new QuantityRepo();
        }
        public Quantity getQuantityByElement(string element) => iquantityRepo.getQuantityByElement(element);
        
    }
}
