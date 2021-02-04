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
        public IUserHandler UserHandler { get; }

        public UserController(IUserHandler userHandler)
        {
            UserHandler = userHandler;
        }

        [Authorize]
        [HttpPost, Route("verifyuser")]
        public object VerifyUser([FromBody] UserDetails userDetails)
        {
            return new { userId = UserHandler.VerifyUser(userDetails) };
        }

        [Route("userdetails")]
        public List<UserDetailsViewModel> UserDetails()
        {
            return UserHandler.UserDetails();
        }

        [Route("userdetail/{id}")]
        public UserDetailsViewModel UserDetail(int Id)
        {
            return UserHandler.UserDetail(Id);
        }
    }
}
