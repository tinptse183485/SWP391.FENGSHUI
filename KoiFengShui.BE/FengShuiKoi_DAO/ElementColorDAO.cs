﻿using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiKoi_DAO
{
    public class ElementColorDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static ElementColorDAO instance = null;
        public static ElementColorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ElementColorDAO();
                }
                return instance;
            }
        }

        public ElementColorDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        public ElementColor GetElementColorById(string element, string color)
        {
            return dbContext.ElementColors.SingleOrDefault(ec => ec.ElementId == element && ec.ColorId == color);
        }
        public ElementColor GetElementColorByColorId(string color)
        {
            return dbContext.ElementColors.FirstOrDefault(ec => ec.ColorId == color);
        }
        public bool DeleteElementColorByColorId(string colorId)
        {
            try
            {
                var elementsToRemove = dbContext.ElementColors
                    .Where(ec => ec.ColorId == colorId)
                    .ToList();

                if (elementsToRemove.Any())
                {
                    dbContext.ElementColors.RemoveRange(elementsToRemove);
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while getting the color point: {ex.Message}", ex);
            }
        }
        public List<ElementColor> GetElementColors()
        {
            return dbContext.ElementColors.ToList();
        }


        public float GetPointElementColor(string element, string color)
        {
            try
            {

                ElementColor existingElementColor = this.GetElementColorById(element, color);


                if (existingElementColor != null)
                {
                    return (float)existingElementColor.ColorPoint;
                }


                return 0.0f;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while getting the color point: {ex.Message}", ex);
            }
        }


        public bool AddElementColor(ElementColor elementColor)
        {
            bool isSuccess = false;
            ElementColor existingElementColor = this.GetElementColorById(elementColor.ElementId, elementColor.ColorId);
            try
            {
                if (existingElementColor == null)
                {
                    dbContext.ElementColors.Add(elementColor);
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

        public bool DeleteElementColor(string element, string color)
        {
            bool isSuccess = false;
            ElementColor elementColor = this.GetElementColorById(element, color);
            try
            {
                if (elementColor != null)
                {
                    dbContext.ElementColors.Remove(elementColor);
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

        public bool UpdateElementColor(ElementColor elementColor)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Entry<ElementColor>(elementColor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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