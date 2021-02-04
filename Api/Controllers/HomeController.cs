using Core.Models;
using Core.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IQuestionService QuestionService { get; }
        public ISolutionService SolutionService { get; }

        public HomeController(IQuestionService questionService, ISolutionService solutionService)
        {
            QuestionService = questionService;
            SolutionService = solutionService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return new JsonResult(new { Message = "This is Api" });
        }

        [Route("secret")]
        public IActionResult Secret()
        {
            return new JsonResult(new { Message = "super secret data from api" });
        }
        
        [HttpPost, Route("submitquestion")]
        public void SubmitQuestion([FromBody] NewQuestion newQuestion)
        {
            QuestionService.AddQuestion(newQuestion);
        }

        [AllowAnonymous]
        [Route("questions")]
        public List<QuestionDetailsViewModel> Questions()
        {
            return QuestionService.Questions();
        }

        [HttpPost, Route("submitsolution")]
        public void SubmitSolution([FromBody] Solution solution)
        {
            this.SolutionService.Submit(solution);
        }

        [AllowAnonymous]
        [Route("solutions/{id}")]
        public List<SolutionDetailsViewModel> Solutions(int Id)
        {
            return SolutionService.Solutions(Id);
        }

        [HttpPost, Route("upvote")]
        public void Upvote([FromBody] QuestionUpvote questionUpvote)
        {
            QuestionService.Upvote(questionUpvote);
        }

        [HttpPost, Route("view")]
        public void View([FromBody] QuestionView questionView)
        {
            QuestionService.View(questionView);
        }

        [HttpPost, Route("like")]
        public void Like([FromBody] LikeDislike likeDislike)
        {
            SolutionService.Like(likeDislike);
        }

        [HttpPost, Route("markbest")]
        public void MarkBest([FromBody] MarkBestSolution markBestSolution)
        {
            SolutionService.MarkBest(markBestSolution);
        }

        [AllowAnonymous]
        [Route("questionsansweredbyuser/{id}")]
        public List<AnsweredQuestionDetailsViewModel> QuestionsAnsweredByUser(int Id)
        {
            return QuestionService.QuestionsAnsweredByUser(Id);
        }
    }
}
