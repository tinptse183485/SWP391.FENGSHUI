using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class LifePlaceDirectionService : ILifePlaceDirectionService
    {
        private ILifePlaceDirectionRepo lifePlaceDirectionRepo;

        public LifePlaceDirectionService()
        {
            lifePlaceDirectionRepo = new LifePlaceDirectionRepo();
        }

        public LifePalaceDirection GetLifePlaceDirectionById(string lifePalace, string direction) => lifePlaceDirectionRepo.GetLifePlaceDirectionById(lifePalace, direction);

        public List<LifePalaceDirection> GetLifePlaceDirections() => lifePlaceDirectionRepo.GetLifePlaceDirections();

        public bool AddLifePlaceDirection(LifePalaceDirection lifePlaceDirection) => lifePlaceDirectionRepo.AddLifePlaceDirection(lifePlaceDirection);

        public bool DeleteLifePlaceDirection(string lifePalace, string direction) => lifePlaceDirectionRepo.DeleteLifePlaceDirection(lifePalace, direction);

        public bool UpdateLifePlaceDirection(LifePalaceDirection lifePlaceDirection) => lifePlaceDirectionRepo.UpdateLifePlaceDirection(lifePlaceDirection);
    }
}