using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface ILifePlaceDirectionRepo
    {
        LifePalaceDirection GetLifePlaceDirectionById(string lifePalace, string direction);
        List<LifePalaceDirection> GetLifePlaceDirections();
        bool AddLifePlaceDirection(LifePalaceDirection lifePlaceDirection);
        bool DeleteLifePlaceDirection(string lifePalace, string direction);
        bool UpdateLifePlaceDirection(LifePalaceDirection lifePlaceDirection);
    }
}