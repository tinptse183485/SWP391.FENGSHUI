using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FengShuiKoi_Repository
{
    public class ElementColorRepo : IElementColorRepo
    {
        public ElementColor GetElementColorById(string element, string color) => ElementColorDAO.Instance.GetElementColorById(element, color);

        public List<ElementColor> GetElementColors() => ElementColorDAO.Instance.GetElementColors();

        public bool AddElementColor(ElementColor elementColor) => ElementColorDAO.Instance.AddElementColor(elementColor);

        public bool DeleteElementColor(string element, string color) => ElementColorDAO.Instance.DeleteElementColor(element, color);

        public bool UpdateElementColor(ElementColor elementColor) => ElementColorDAO.Instance.UpdateElementColor(elementColor);

        public float GetPointElementColor(string element, string color) => ElementColorDAO.Instance.GetPointElementColor(element, color);

        public ElementColor GetElementColorByColorId(string color) => ElementColorDAO.Instance.GetElementColorByColorId(color);

        public bool DeleteElementColorByColorId(string color) => ElementColorDAO.Instance.DeleteElementColorByColorId( color);
    }
}