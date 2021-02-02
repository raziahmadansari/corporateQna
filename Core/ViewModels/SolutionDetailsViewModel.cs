using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    [TableName("SolutionDetails")]
    public class SolutionDetailsViewModel
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public int AskedByUserId { get; set; }
        [Column]
        public int QuestionId { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Answer { get; set; }
        [Column]
        public DateTime TimeStamp { get; set; }
        [Column]
        public bool BestSolution { get; set; }
        [Column]
        public int Likes { get; set; }
        [Column]
        public int DisLikes { get; set; }
    }
}
