using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;

namespace FengShuiKoi_Repository
{
    public class LifePlaceRepo : ILifePlaceRepo
    {
        public LifePalace GetLifePlaceById(string id) => LifePlaceDAO.Instance.GetLifePlaceById(id);

        public List<LifePalace> GetLifePlaces() => LifePlaceDAO.Instance.GetLifePlaces();

        public bool AddLifePlace(LifePalace lifePlace) => LifePlaceDAO.Instance.AddLifePlace(lifePlace);

        public bool DeleteLifePlace(string id) => LifePlaceDAO.Instance.DeleteLifePlace(id);

        public bool UpdateLifePlace(LifePalace lifePlace) => LifePlaceDAO.Instance.UpdateLifePlace(lifePlace);
    }
}