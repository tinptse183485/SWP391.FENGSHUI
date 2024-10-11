using FengShuiKoi_BO;
using FengShuiKoi_DAO;
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


        public List<TypeColor> GetAllType() => iTypeColorRepo.GetAllType();
        public TypeColor GetTypeByColorID(string colorId) => iTypeColorRepo.GetTypeByColorID(colorId);
        public TypeColor GetPercentage(string color, string type) => iTypeColorRepo.GetPercentage(color, type);
        public bool DeleteTypeColorByColorId(string colorId) => iTypeColorRepo.DeleteTypeColorByColorId(colorId);
        public bool DeleteTypeColorByKoiType(string KoiType) => iTypeColorRepo.DeleteTypeColorByKoiType(KoiType);
        public List<TypeColor> GetTypeByColor(string color) => iTypeColorRepo.GetTypeByColor(color);
        public bool AddKoiTypeColor(TypeColor koiFish) => iTypeColorRepo.AddKoiTypeColor(koiFish);
        public List<TypeColor> GetColorsAndPercentages(string koiType) => iTypeColorRepo.GetColorsAndPercentages(koiType);
        public List<TypeColor> GetTypeByKoiType(string KoiType) => iTypeColorRepo.GetTypeByKoiType(KoiType);

    }
}
