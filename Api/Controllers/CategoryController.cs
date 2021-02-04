using Core.Models;
using Core.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryService CategoryService { get; }

        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpPost, Route("addcategory")]
        public void AddCategory([FromBody] Category category)
        {
            CategoryService.Add(category);
        }

        [AllowAnonymous]
        [Route("categories")]
        public List<CategoryDetailsViewModel> Categories()
        {
            return CategoryService.Categories();
        }
    }
}
