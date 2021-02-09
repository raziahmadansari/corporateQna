using Core.Models;
using Core.ViewModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private Database Db { get; }

        public UserService()
        {
            Db = DbService.Db;
        }

        public int VerifyUser(UserDetails userDetails)
        {
            var user = Db.FirstOrDefault<Models.DataModels.User>("WHERE [Username] = @0", userDetails.Username);
            
            if (user != null)
            {
                return user.Id;
            }

            user = new Models.DataModels.User
            {
                Username = userDetails.Username,
                Name = userDetails.Name,
                Designation = userDetails.Designation,
                Team = userDetails.Team,
                Category = userDetails.Category,
                Location = userDetails.Location
            };

            Db.Insert("Users", user);
            user = Db.FirstOrDefault<Models.DataModels.User>("WHERE [Username] = @0", userDetails.Username);
            
            return user.Id;
        }

        public List<UserDetailsViewModel> UserDetails()
        {
            return Db.Fetch<UserDetailsViewModel>("SELECT * FROM [UserDetails]");
        }

        public UserDetailsViewModel UserDetail(int id)
        {
            return Db.FirstOrDefault<UserDetailsViewModel>("SELECT * FROM [UserDetails] WHERE [Id]=@0", id);
        }
    }
}
