using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class TypeColorDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static TypeColorDAO instance = null;

        public static TypeColorDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new TypeColorDAO();
                }
                return instance;
            }
        }

        public TypeColorDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public TypeColor GetPercentage(string color, string type)
        {
            return dbContext.TypeColors.SingleOrDefault(m => m.Color.Equals(color) && m.KoiType.Equals(type));
        }

        public List<TypeColor> GetAllType()
        {
            return dbContext.TypeColors.ToList();
        }
        public List<TypeColor> GetColorsAndPercentages(string koiType)
        {
            List<TypeColor> list = new List<TypeColor>();

            foreach (TypeColor item in this.GetAllType())
            {
                if (item.KoiType == koiType)
                    list.Add(item);
            }

            return list;
        }
        public TypeColor GetTypeByColorID(string colorId)
        {
            return dbContext.TypeColors.FirstOrDefault(tc => tc.ColorId == colorId);
        }
        public List<TypeColor> GetTypeByColor(string color)
        {
            List<TypeColor> listType = new List<TypeColor>();

            foreach (TypeColor item in this.GetAllType())
            {
                if (item.ColorId == color)
                    listType.Add(item);
            }

            return listType;
        }
        public bool DeleteTypeColorsByColorId(string colorId)
        {
            try
            {
                var colorsToRemove = dbContext.TypeColors
                    .Where(tc => tc.ColorId == colorId)
                    .ToList();

                if (colorsToRemove.Any())
                {
                   
                    var koiTypesToRemove = colorsToRemove.Select(tc => tc.KoiType).Distinct().ToList();

                    var typeKoisToRemove = dbContext.TypeColors
                        .Where(tk => koiTypesToRemove.Contains(tk.KoiType))
                        .ToList();

                    dbContext.TypeColors.RemoveRange(typeKoisToRemove);
                    dbContext.TypeColors.RemoveRange(colorsToRemove);
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi xóa màu và loại Koi: {ex.Message}", ex);
            }
        }

        public bool DeleteTypeColorsByKoiType(string koiType)
        {
            try
            {
               
                var typeColorsToDelete = dbContext.TypeColors
                    .Where(tc => tc.KoiType == koiType)
                    .ToList();

                if (typeColorsToDelete.Any())
                {
                   
                    dbContext.TypeColors.RemoveRange(typeColorsToDelete);
                    dbContext.SaveChanges();

                   
                    return true ;
                }

                
                return false;
            }
            catch (Exception ex)
            {
              
                return false;
            }
        }

        public bool AddKoiTypeColor(TypeColor koiFish)
        {
            try
            {
                if (koiFish == null)
                {
                    return false;
                }

                var koiTypeColor = new TypeColor
                {
                    KoiType = koiFish.KoiType,
                    ColorId = koiFish.ColorId,
                    Percentage = koiFish.Percentage
                };

            
                dbContext.TypeColors.Add(koiTypeColor);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
           
                return false;
            }
        }
    }
}
