using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System.Collections.Generic;

namespace FengShuiKoi_Services
{
    public class LifePlaceService : ILifePlaceService
    {
        private ILifePlaceRepo lifePlaceRepo;

        public LifePlaceService()
        {
            lifePlaceRepo = new LifePlaceRepo();
        }

        public LifePalace GetLifePlaceById(string id) => lifePlaceRepo.GetLifePlaceById(id);

        public List<LifePalace> GetLifePlaces() => lifePlaceRepo.GetLifePlaces();

        public bool AddLifePlace(LifePalace lifePlace) => lifePlaceRepo.AddLifePlace(lifePlace);

        public bool DeleteLifePlace(string id) => lifePlaceRepo.DeleteLifePlace(id);

        public bool UpdateLifePlace(LifePalace lifePlace) => lifePlaceRepo.UpdateLifePlace(lifePlace);
    }
}