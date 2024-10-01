using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class FeedbackDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static FeedbackDAO instance = null;

        // Singleton design pattern
        public static FeedbackDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackDAO();
                }
                return instance;
            }
        }

        public FeedbackDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }

        // Method to get feedback by FeedbackID
        public Feedback GetFeedbackByFeedbackID(string feedbackId)
        {
            return dbContext.Feedbacks.SingleOrDefault(f => f.FbId.Equals(feedbackId));
        }

        // Method to get feedback by UserID
        public List<Feedback> GetFeedbackByUserID(string userId)
        {
            return dbContext.Feedbacks.Where(f => f.UserId.Equals(userId)).ToList();
        }

        // Method to retrieve all feedbacks
        public List<Feedback> GetFeedbacks()
        {
            return dbContext.Feedbacks.ToList();
        }

        // Method to add new feedback
        public bool AddFeedback(Feedback feedback)
        {
            bool isSuccess = false;
            try
            {
                dbContext.Feedbacks.Add(feedback);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }

        // Method to delete feedback by FeedbackID
        public bool DeleteFeedback(string feedbackId)
        {
            bool isSuccess = false;
            Feedback feedback = this.GetFeedbackByFeedbackID(feedbackId);
            try
            {
                if (feedback != null)
                {
                    dbContext.Feedbacks.Remove(feedback);
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

        // Method to update feedback by FeedbackID
        public bool UpdateFeedback(string feedbackId)
        {
            bool isSuccess = false;
            Feedback feedback = this.GetFeedbackByFeedbackID(feedbackId);
            try
            {
                if (feedback != null)
                {
                    dbContext.Entry<Feedback>(feedback).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
    }

}
