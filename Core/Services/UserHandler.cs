using Core.Models;
using Core.ViewModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class UserHandler : IUserHandler
    {
        private Database Db { get; }

        public UserHandler(Database database)
        {
            Db = database;
        }

        public int VerifyUser(UserDetails userDetails)
        {
            var user = Db.FirstOrDefault<Models.DataModels.UserDetails>("select * from [Users] where[Username]=@0", userDetails.Username);
            
            if (user != null)
            {
                return user.Id;
            }

            user = new Models.DataModels.UserDetails
            {
                Username = userDetails.Username,
                Name = userDetails.Name,
                Designation = userDetails.Designation,
                Team = userDetails.Team,
                Category = userDetails.Category,
                Location = userDetails.Location
            };

            Db.Insert("Users", user);
            user = Db.FirstOrDefault<Models.DataModels.UserDetails>("select * from [Users] where[Username]=@0", userDetails.Username);
            
            return user.Id;
        }

        public List<UserDetailsViewModel> UserDetails()
        {
            return Db.Fetch<UserDetailsViewModel>("select * from [UserDetails]");
        }

        public UserDetailsViewModel UserDetail(int id)
        {
            return Db.FirstOrDefault<UserDetailsViewModel>("select * from [UserDetails] where [Id]=@0", id);
        }
    }
}
