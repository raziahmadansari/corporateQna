using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("QuestionUpVotes")]
    [PrimaryKey("Id")]
    public class QuestionUpvote
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public int QuestionId { get; set; }
    }
}
