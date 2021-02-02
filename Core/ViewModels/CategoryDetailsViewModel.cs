using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    [TableName("CategoryDetails")]
    public class CategoryDetailsViewModel
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public int ThisWeek { get; set; }
        [Column]
        public int ThisMonth { get; set; }
        [Column]
        public int Total { get; set; }
    }
}
