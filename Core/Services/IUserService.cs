using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IUserService
    {
        public int VerifyUser(UserDetails userDetails);
        public List<UserDetailsViewModel> UserDetails();
        public UserDetailsViewModel UserDetail(int id);
    }
}
