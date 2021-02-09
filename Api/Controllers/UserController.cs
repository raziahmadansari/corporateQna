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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [Authorize]
        [HttpPost, Route("verifyuser")]
        public object VerifyUser([FromBody] UserDetails userDetails)
        {
            return new { userId = UserService.VerifyUser(userDetails) };
        }

        [Route("userdetails")]
        public List<UserDetailsViewModel> UserDetails()
        {
            return UserService.UserDetails();
        }

        [Route("userdetail/{id}")]
        public UserDetailsViewModel UserDetail(int Id)
        {
            return UserService.UserDetail(Id);
        }
    }
}
