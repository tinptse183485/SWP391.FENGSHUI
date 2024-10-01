using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class LifePlaceDirectionRepo : ILifePlaceDirectionRepo
    {
        public LifePalaceDirection GetLifePlaceDirectionById(string lifePalace, string direction) => LifePlaceDirectionDAO.Instance.GetLifePlaceDirectionById(lifePalace, direction);

        public List<LifePalaceDirection> GetLifePlaceDirections() => LifePlaceDirectionDAO.Instance.GetLifePlaceDirections();

        public bool AddLifePlaceDirection(LifePalaceDirection lifePlaceDirection) => LifePlaceDirectionDAO.Instance.AddLifePlaceDirection(lifePlaceDirection);

        public bool DeleteLifePlaceDirection(string lifePalace, string direction) => LifePlaceDirectionDAO.Instance.DeleteLifePlaceDirection(lifePalace, direction);

        public bool UpdateLifePlaceDirection(LifePalaceDirection lifePlaceDirection) => LifePlaceDirectionDAO.Instance.UpdateLifePlaceDirection(lifePlaceDirection);
    }
}