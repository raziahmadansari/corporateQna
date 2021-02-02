using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class MarkBestSolution
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool BestSolution { get; set; }
    }
}
