using Core.Models;
using Core.ViewModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class SolutionService : ISolutionService
    {
        private Database Db { get; }

        public SolutionService()
        {
            Db = DbService.Db;
        }

        public void Submit(Solution solution)
        {
            var sol = new Models.DataModels.Solution
            {
                UserId = solution.UserId,
                QuestionId = solution.QuestionId,
                Answer = solution.Answer,
                TimeStamp = DateTime.Now,
            };

            Db.Insert("Answers", sol);
        }

        public List<SolutionDetailsViewModel> Solutions(int Id)
        {
            return Db.Fetch<SolutionDetailsViewModel>("SELECT * FROM [SolutionDetails] WHERE [QuestionId] = @0", Id);
        }

        public void Like(LikeDislike likeDislike)
        {
            var liked = Db.FirstOrDefault<Models.DataModels.LikeState>("WHERE [UserId] = @0 AND [AnswerId] = @1", likeDislike.UserId, likeDislike.AnswerId);
            if (liked == null)
            {
                var addLike = new Models.DataModels.LikeState
                {
                    AnswerId = likeDislike.AnswerId,
                    UserId = likeDislike.UserId,
                    Sentiment = likeDislike.Sentiment
                };

                Db.Insert("LikeDislike", addLike);
            }
            else
            {
                liked.Sentiment = likeDislike.Sentiment;
                Db.Update(liked);
            }
        }

        public void MarkBest(MarkBestSolution markBestSolution)
        {
            var previousBest = Db.FirstOrDefault<Models.DataModels.Solution>("WHERE [QuestionId] = @0 AND [BestSolution] = 1", markBestSolution.QuestionId);
            if (previousBest != null)
            {
                if (previousBest.Id != markBestSolution.Id)
                {
                    previousBest.BestSolution = false;
                    Db.Update(previousBest);
                }
            }
            var currentBest = Db.FirstOrDefault<Models.DataModels.Solution>("WHERE [Id] = @0", markBestSolution.Id);
            currentBest.BestSolution = true;
            Db.Update(currentBest);
        }
    }
}
