using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class LikeDislike
    {
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public bool Sentiment { get; set; }
    }
}
