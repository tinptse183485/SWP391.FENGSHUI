using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class KoiVarietyRepo : IKoiVarietyRepo
    {
        public bool AddKoiVariety(KoiVariety variety) => KoiVarietyDAO.Instance.AddKoiVariety(variety);
       

        public bool DeleteKoiVariety(string type) => KoiVarietyDAO.Instance.DeleteKoiVariety(type);
       

        public List<KoiVariety> GetKoiVarieties() => KoiVarietyDAO.Instance.GetKoiVarieties();
      

        public List<KoiVariety> GetKoiVarietiesByElemnet(string element) => KoiVarietyDAO.Instance.GetKoiVarietiesByElemnet(element);


        public KoiVariety GetKoiVarietyByType(string type) => KoiVarietyDAO.Instance.GetKoiVarietyByType(type);
       

        public bool UpdateKoiVariety(string type) => KoiVarietyDAO.Instance.UpdateKoiVariety(type);
       
    }
}
