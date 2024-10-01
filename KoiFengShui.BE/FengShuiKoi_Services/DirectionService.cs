using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class DirectionService : IDirectionService
    {
        private IDirectionRepo directionRepo;

        public DirectionService()
        {
            directionRepo = new DirectionRepo();
        }

        public Direction GetDirectionById(string id) => directionRepo.GetDirectionById(id);

        public List<Direction> GetDirections() => directionRepo.GetDirections();

        public bool AddDirection(Direction direction) => directionRepo.AddDirection(direction);

        public bool DeleteDirection(string id) => directionRepo.DeleteDirection(id);

        public bool UpdateDirection(Direction direction) => directionRepo.UpdateDirection(direction);
    }
}