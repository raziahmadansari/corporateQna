using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ICategoryService
    {
        public void Add(Category category);
        public List<CategoryDetailsViewModel> Categories();
    }
}
