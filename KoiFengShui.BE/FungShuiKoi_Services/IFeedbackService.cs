using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
    public interface IFeedbackService
    {

        public Feedback GetFeedbackByFeedbackID(string feedbackId);

        public List<Feedback> GetFeedbackByUserID(string userId);


        public List<Feedback> GetAllFeedbacks();


        public bool AddFeedback(Feedback feedback);


        public bool UpdateFeedback(string feedbackId);


        public bool DeleteFeedback(string feedbackId);
    }
}
