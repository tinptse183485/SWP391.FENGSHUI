using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public interface IElementColorService
    {
        ElementColor GetElementColorById(string element, string color);
        List<ElementColor> GetElementColors();
        bool AddElementColor(ElementColor elementColor);
        bool DeleteElementColor(string element, string color);
        bool UpdateElementColor(ElementColor elementColor);
    }
}