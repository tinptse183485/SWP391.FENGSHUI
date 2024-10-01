using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class ElementColorService : IElementColorService
    {
        private IElementColorRepo elementColorRepo;

        public ElementColorService()
        {
            elementColorRepo = new ElementColorRepo();
        }

        public ElementColor GetElementColorById(string element, string color) => elementColorRepo.GetElementColorById(element, color);

        public List<ElementColor> GetElementColors() => elementColorRepo.GetElementColors();

        public bool AddElementColor(ElementColor elementColor) => elementColorRepo.AddElementColor(elementColor);

        public bool DeleteElementColor(string element, string color) => elementColorRepo.DeleteElementColor(element, color);

        public bool UpdateElementColor(ElementColor elementColor) => elementColorRepo.UpdateElementColor(elementColor);
    }
}