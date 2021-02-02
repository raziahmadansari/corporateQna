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
    [ApiController]
    public class HomeController : Controller
    {
        public IUserHandler UserHandler { get; }
        public ICategoryService CategoryService { get; }
        public IQuestionService QuestionService { get; }
        public ISolutionService SolutionService { get; }

        public HomeController(IUserHandler userHandler, ICategoryService categoryService, IQuestionService questionService, ISolutionService solutionService)
        {
            UserHandler = userHandler;
            CategoryService = categoryService;
            QuestionService = questionService;
            SolutionService = solutionService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return new JsonResult(new { Message = "This is Api" });
        }

        [Authorize]
        [Route("/secret")]
        public IActionResult Secret()
        {
            return new JsonResult(new { Message = "super secret data from api" });
        }

        [Authorize]
        [Route("/verifyuser")]
        [HttpPost]
        public object VerifyUser([FromBody] UserDetails userDetails)
        {
            var userId = UserHandler.VerifyUser(userDetails);
            return new { userId };
        }

        [Authorize]
        [Route("/addcategory")]
        [HttpPost]
        public void AddCategory([FromBody] Category category)
        {
            CategoryService.Add(category);
        }

        [Route("/categories")]
        public List<CategoryDetailsViewModel> Categories()
        {
            return CategoryService.Categories();
        }

        [Authorize]
        [Route("/submitquestion")]
        [HttpPost]
        public void SubmitQuestion([FromBody] NewQuestion newQuestion)
        {
            QuestionService.AddQuestion(newQuestion);
        }

        [Route("/questions")]
        public List<QuestionDetailsViewModel> Questions()
        {
            return QuestionService.Questions();
        }

        [Authorize]
        [Route("/submitsolution")]
        [HttpPost]
        public void SubmitSolution([FromBody] Solution solution)
        {
            this.SolutionService.Submit(solution);
        }

        [Route("/solutions/{id}")]
        public List<SolutionDetailsViewModel> Solutions(int Id)
        {
            return SolutionService.Solutions(Id);
        }

        [Authorize]
        [Route("/upvote")]
        [HttpPost]
        public void Upvote([FromBody] QuestionUpvote questionUpvote)
        {
            QuestionService.Upvote(questionUpvote);
        }

        [Authorize]
        [Route("/view")]
        [HttpPost]
        public void View([FromBody] QuestionView questionView)
        {
            QuestionService.View(questionView);
        }

        [Authorize]
        [Route("/like")]
        [HttpPost]
        public void Like([FromBody] LikeDislike likeDislike)
        {
            SolutionService.Like(likeDislike);
        }

        [Authorize]
        [Route("markbest")]
        [HttpPost]
        public void MarkBest([FromBody] MarkBestSolution markBestSolution)
        {
            SolutionService.MarkBest(markBestSolution);
        }

        [Route("/userdetails")]
        public List<UserDetailsViewModel> UserDetails()
        {
            return UserHandler.UserDetails();
        }

        [Route("/userdetail/{id}")]
        public UserDetailsViewModel UserDetail(int Id)
        {
            return UserHandler.UserDetail(Id);
        }

        [Route("/questionsansweredbyuser/{id}")]
        public List<AnsweredQuestionDetailsViewModel> QuestionsAnsweredByUser(int Id)
        {
            return QuestionService.QuestionsAnsweredByUser(Id);
        }
    }
}
