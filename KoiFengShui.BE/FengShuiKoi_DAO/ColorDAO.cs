using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class ColorDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static ColorDAO instance = null;
        public static ColorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ColorDAO();
                }
                return instance;
            }
        }

        public ColorDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public Color GetColorById(string id)
        {
            return dbContext.Colors.SingleOrDefault(c => c.Color1 == id);
        }

        public List<Color> GetColors()
        {
            return dbContext.Colors.ToList();
        }

        public bool AddColor(Color color)
        {
            bool isSuccess = false;
            Color existingColor = this.GetColorById(color.Color1);
            try
            {
                if (existingColor == null)
                {
                    dbContext.Colors.Add(color);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }

        public bool DeleteColor(string id)
        {
            bool isSuccess = false;
            Color color = this.GetColorById(id);
            try
            {
                if (color != null)
                {
                    dbContext.Colors.Remove(color);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }

        public bool UpdateColor(Color color)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<Color>(color).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
    }
}