using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    [TableName("AnsweredQuestionDetails")]
    public class AnsweredQuestionDetailsViewModel
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public int AnsweredBy { get; set; }
        [Column]
        public int Category { get; set; }
        [Column]
        public string Question { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public DateTime TimeStamp { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int UpVotes { get; set; }
        [Column]
        public int Views { get; set; }
        [Column]
        public int Answers { get; set; }
        [Column]
        public bool Solved { get; set; }
    }
}
