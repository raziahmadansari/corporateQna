using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IQuestionService
    {
        public void AddQuestion(NewQuestion newQuestion);
        public List<QuestionDetailsViewModel> Questions();
        public void Upvote(QuestionUpvote questionUpvote);
        public void View(QuestionView questionView);
        public List<AnsweredQuestionDetailsViewModel> QuestionsAnsweredByUser(int id);
    }
}
