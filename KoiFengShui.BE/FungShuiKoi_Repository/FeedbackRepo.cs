using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    
        public class FeedbackRepo : IFeedbackRepo
        {
            public bool AddFeedback(Feedback feedback) => FeedbackDAO.Instance.AddFeedback(feedback);

            public bool DeleteFeedback(string feedbackId) => FeedbackDAO.Instance.DeleteFeedback(feedbackId);

            public Feedback GetFeedbackByFeedbackID(string feedbackId) => FeedbackDAO.Instance.GetFeedbackByFeedbackID(feedbackId);

            public List<Feedback> GetFeedbackByUserID(string userId) => FeedbackDAO.Instance.GetFeedbackByUserID(userId);

            public List<Feedback> GetAllFeedbacks() => FeedbackDAO.Instance.GetFeedbacks();

            public bool UpdateFeedback(string feedbackId) => FeedbackDAO.Instance.UpdateFeedback(feedbackId);
        }

    
}
