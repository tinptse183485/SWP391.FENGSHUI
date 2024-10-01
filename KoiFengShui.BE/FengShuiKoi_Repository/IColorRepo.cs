using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface IColorRepo
    {
        public Color GetColorById(string id);
        public List<Color> GetColors();
        public bool AddColor(Color color);
        public bool DeleteColor(string id);
        public bool UpdateColor(Color color);
    }
}