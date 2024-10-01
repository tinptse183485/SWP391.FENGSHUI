using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface IElementColorRepo
    {
        ElementColor GetElementColorById(string element, string color);
        List<ElementColor> GetElementColors();
        bool AddElementColor(ElementColor elementColor);
        bool DeleteElementColor(string element, string color);
        bool UpdateElementColor(ElementColor elementColor);
    }
}