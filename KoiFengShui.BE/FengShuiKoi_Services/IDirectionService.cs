using FengShuiKoi_BO;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public interface IDirectionService
    {
        Direction GetDirectionById(string id);
        List<Direction> GetDirections();
        bool AddDirection(Direction direction);
        bool DeleteDirection(string id);
        bool UpdateDirection(Direction direction);
    }
}