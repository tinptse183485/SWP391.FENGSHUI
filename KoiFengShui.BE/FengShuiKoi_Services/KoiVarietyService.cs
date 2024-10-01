using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class KoiVarietyService : IKoiVarietyService
    {
        private IKoiVarietyRepo ikoiVarietyRepo;

        public KoiVarietyService()
        {
            ikoiVarietyRepo = new KoiVarietyRepo();
        }
        public bool AddKoiVariety(KoiVariety variety) => ikoiVarietyRepo.AddKoiVariety(variety);
       

        public bool DeleteKoiVariety(string type) => ikoiVarietyRepo.DeleteKoiVariety(type);
        

        public List<KoiVariety> GetKoiVarieties() => ikoiVarietyRepo.GetKoiVarieties();
        

        public List<KoiVariety> GetKoiVarietiesByElemnet(string element) => ikoiVarietyRepo.GetKoiVarietiesByElemnet(element);
      

        public KoiVariety GetKoiVarietyByType(string type) => ikoiVarietyRepo.GetKoiVarietyByType(type);
        

        public bool UpdateKoiVariety(string type) => ikoiVarietyRepo.UpdateKoiVariety(type);
        
    }
}
