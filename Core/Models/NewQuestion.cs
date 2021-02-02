using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class NewQuestion
    {
        public int UserId { get; set; }
        public int Category { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
    }
}
