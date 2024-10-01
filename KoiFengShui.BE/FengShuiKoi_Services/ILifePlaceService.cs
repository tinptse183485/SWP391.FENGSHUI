using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public interface ILifePlaceService
    {
        LifePalace GetLifePlaceById(string id);
        List<LifePalace> GetLifePlaces();
        bool AddLifePlace(LifePalace lifePlace);
        bool DeleteLifePlace(string id);
        bool UpdateLifePlace(LifePalace lifePlace);
    }
}