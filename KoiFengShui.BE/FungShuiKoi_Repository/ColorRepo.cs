using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class ColorRepo : IColorRepo
    {
        public Color GetColorById(string id) => ColorDAO.Instance.GetColorById(id);

        public List<Color> GetColors() => ColorDAO.Instance.GetColors();

        public bool AddColor(Color color) => ColorDAO.Instance.AddColor(color);

        public bool DeleteColor(string id) => ColorDAO.Instance.DeleteColor(id);

        public bool UpdateColor(Color color) => ColorDAO.Instance.UpdateColor(color);
    }
}