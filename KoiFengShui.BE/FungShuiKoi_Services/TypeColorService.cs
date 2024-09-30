using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class TypeColorService : ITypeColorService
    {
        private ITypeColorRepo iTypeColorRepo;

        public TypeColorService()
        {
            iTypeColorRepo = new TypeColorRepo();
        }
        public TypeColor GetPercentage(string color, string type) => iTypeColorRepo.GetPercentage(color, type);
        
    }
}
