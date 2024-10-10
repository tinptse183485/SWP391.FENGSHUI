using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class QuantityOfFishService : IQuantityOfFishService
    {
        private IQuantityOfFishRepo iquantityRepo;
        public QuantityOfFishService()
        {
            iquantityRepo = new QuantityOfFishRepo();
        }
        public QuantityOfFish getQuantityByElement(string element) => iquantityRepo.getQuantityByElement(element);
        
    }
}
