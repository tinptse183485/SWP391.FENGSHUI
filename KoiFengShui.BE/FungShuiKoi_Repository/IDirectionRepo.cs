using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public interface IDirectionRepo
    {
        Direction GetDirectionById(string id);
        List<Direction> GetDirections();
        bool AddDirection(Direction direction);
        bool DeleteDirection(string id);
        bool UpdateDirection(Direction direction);
    }
}