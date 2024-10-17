using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using FengShuiKoi_Repository;
using System.Collections.Generic;
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

        public async Task<bool> AddFeedback(Feedback feedback)
        {
            return await feedbackRepo.AddFeedback(feedback);
        }

        public async Task<bool> DeleteFeedback(string feedbackId)
        {
            return await feedbackRepo.DeleteFeedback(feedbackId);
        }

        public async Task<Feedback> GetFeedbackByFeedbackID(string feedbackId)
        {
            return await feedbackRepo.GetFeedbackByFeedbackID(feedbackId);
        }

        public async Task<List<Feedback>> GetFeedbackByUserID(string userId)
        {
            return await feedbackRepo.GetFeedbackByUserID(userId);
        }

        public async Task<List<Feedback>> GetAllFeedbacks()
        {
            return await feedbackRepo.GetAllFeedbacks();
        }

        public async Task<bool> UpdateFeedback(string feedbackId)
        {
            return await feedbackRepo.UpdateFeedback(feedbackId);
        }
        public async Task<List<Feedback>> GetFeedbackByAdId(string AdId)
        {
            return await feedbackRepo.GetFeedbackByAdId(AdId);
        }
        public async Task<string> GetLastFBId()
        {
            return await feedbackRepo.GetLastFBId();
        }
        public async Task<List<Feedback>> GetFeedbackByAdIdAndSortByRate(string AdId) => await feedbackRepo.GetFeedbackByAdIdAndSortByRate(AdId);
        public async Task<List<Feedback>> GetFeedbackByAdIdAndRate(string AdId, byte Rate) => await feedbackRepo.GetFeedbackByAdIdAndRate(AdId, Rate);
    }
}