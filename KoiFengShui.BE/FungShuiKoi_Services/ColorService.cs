using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class ColorService : IColorService
    {
        private IColorRepo colorRepo;

        public ColorService()
        {
            colorRepo = new ColorRepo();
        }

        public Color GetColorById(string id) => colorRepo.GetColorById(id);

        public List<Color> GetColors() => colorRepo.GetColors();

        public bool AddColor(Color color) => colorRepo.AddColor(color);

        public bool DeleteColor(string id) => colorRepo.DeleteColor(id);

        public bool UpdateColor(Color color) => colorRepo.UpdateColor(color);
    }
}