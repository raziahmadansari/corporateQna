using Core.Models;
using Core.ViewModels;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private Database Db { get; }

        public CategoryService(Database database)
        {
            Db = database;
        }

        public void Add(Category category)
        {
            var categoryExists = Db.FirstOrDefault<Models.DataModels.Category>("select * from [Categories] where [Name]=@0", category.Name);
            if (categoryExists == null)
            {
                var newCategory = new Models.DataModels.Category
                {
                    Name = category.Name,
                    Description = category.Description
                };
                Db.Insert("Categories", newCategory);
            }
        }

        public List<CategoryDetailsViewModel> Categories()
        {
            return Db.Fetch<CategoryDetailsViewModel>("select * from [CategoryDetails]");
        }
    }
}
