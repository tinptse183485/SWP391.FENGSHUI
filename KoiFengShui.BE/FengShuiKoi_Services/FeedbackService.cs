using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepo feedbackRepo;

        public FeedbackService()
        {
            feedbackRepo = new FeedbackRepo();
        }

        public bool AddFeedback(Feedback feedback)
        {
            return feedbackRepo.AddFeedback(feedback);
        }

        public bool DeleteFeedback(string feedbackId)
        {
            return feedbackRepo.DeleteFeedback(feedbackId);
        }

        public Feedback GetFeedbackByFeedbackID(string feedbackId)
        {
            return feedbackRepo.GetFeedbackByFeedbackID(feedbackId);
        }

        public List<Feedback> GetFeedbackByUserID(string userId)
        {
            return feedbackRepo.GetFeedbackByUserID(userId);
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return feedbackRepo.GetAllFeedbacks();
        }

        public bool UpdateFeedback(string feedbackId)
        {
            return feedbackRepo.UpdateFeedback(feedbackId);
        }
    }

}
