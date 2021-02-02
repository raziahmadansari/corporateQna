﻿using Core.Models;
using Core.ViewModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class QuestionService : IQuestionService
    {
        private Database Db { get; }

        public QuestionService(Database database)
        {
            Db = database;
        }

        public void AddQuestion(NewQuestion newQuestion)
        {
            var question = new Models.DataModels.NewQuestion
            {
                UserId = newQuestion.UserId,
                Category = newQuestion.Category,
                Question = newQuestion.Question,
                Description = newQuestion.Description
            };
            question.TimeStamp = DateTime.Now;

            Db.Insert("Questions", question);
        }

        public List<QuestionDetailsViewModel> Questions()
        {
            return Db.Fetch<QuestionDetailsViewModel>("select * from [QuestionDetails]");
        }

        public void Upvote(QuestionUpvote questionUpvote)
        {
            var voteExists = Db.FirstOrDefault<Models.DataModels.QuestionUpvote>("select * from [QuestionUpVotes] where [UserId]=@0 AND [QuestionId]=@1", questionUpvote.UserId, questionUpvote.QuestionId);
            if (voteExists == null)
            {
                var newVote = new Models.DataModels.QuestionUpvote
                {
                    UserId = questionUpvote.UserId,
                    QuestionId = questionUpvote.QuestionId
                };
                Db.Insert("QuestionUpVotes", newVote);
            }
        }

        public void View(QuestionView questionView)
        {
            var userViewed = Db.FirstOrDefault<Models.DataModels.QuestionView>("select * from [QuestionViews] where [UserId]=@0 AND [QuestionId]=@1", questionView.UserId, questionView.QuestionId);
            if (userViewed == null)
            {
                var view = new Models.DataModels.QuestionView
                {
                    UserId = questionView.UserId,
                    QuestionId = questionView.QuestionId
                };
                Db.Insert("QuestionViews", view);
            }
        }

        public List<AnsweredQuestionDetailsViewModel> QuestionsAnsweredByUser(int id)
        {
            return Db.Fetch<AnsweredQuestionDetailsViewModel>("select * from [AnsweredQuestionDetails] where [AnsweredBy]=@0", id);
        }
    }
}
