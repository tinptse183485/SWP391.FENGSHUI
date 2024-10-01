using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class ElementService
    {
        private IElementRepo iElementRepo;
        public ElementService() 
        {
             iElementRepo = new ElementRepo();
        }
        public Element GetElementAndMutualism(string element) => iElementRepo.GetElementAndMutualism(element);
    }
}
