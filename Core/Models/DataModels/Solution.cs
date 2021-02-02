using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("Answers")]
    [PrimaryKey("Id")]
    public class Solution
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public int QuestionId { get; set; }
        [Column]
        public string Answer { get; set; }
        [Column]
        public DateTime TimeStamp { get; set; }
        [Column]
        public bool BestSolution { get; set; }
    }
}
