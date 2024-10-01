using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class DirectionRepo : IDirectionRepo
    {
        public Direction GetDirectionById(string id) => DirectionDAO.Instance.GetDirectionById(id);

        public List<Direction> GetDirections() => DirectionDAO.Instance.GetDirections();

        public bool AddDirection(Direction direction) => DirectionDAO.Instance.AddDirection(direction);

        public bool DeleteDirection(string id) => DirectionDAO.Instance.DeleteDirection(id);

        public bool UpdateDirection(Direction direction) => DirectionDAO.Instance.UpdateDirection(direction);
    }
}