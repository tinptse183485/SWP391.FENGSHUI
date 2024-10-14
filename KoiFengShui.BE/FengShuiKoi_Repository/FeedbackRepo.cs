using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class FeedbackRepo : IFeedbackRepo
    {
        public async Task<bool> AddFeedback(Feedback feedback) => await FeedbackDAO.Instance.AddFeedback(feedback);

        public async Task<bool> DeleteFeedback(string feedbackId) => await FeedbackDAO.Instance.DeleteFeedback(feedbackId);

        public async Task<Feedback> GetFeedbackByFeedbackID(string feedbackId) => await FeedbackDAO.Instance.GetFeedbackByFeedbackID(feedbackId);

        public async Task<List<Feedback>> GetFeedbackByUserID(string userId) => await FeedbackDAO.Instance.GetFeedbackByUserID(userId);

        public async Task<List<Feedback>> GetAllFeedbacks() => await FeedbackDAO.Instance.GetFeedbacks();

        public async Task<bool> UpdateFeedback(string feedbackId) => await FeedbackDAO.Instance.UpdateFeedback(feedbackId);
    }
}