using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ISolutionService
    {
        public void Submit(Solution solution);
        public List<SolutionDetailsViewModel> Solutions(int Id);
        public void Like(LikeDislike likeDislike);
        public void MarkBest(MarkBestSolution markBestSolution);
    }
}
