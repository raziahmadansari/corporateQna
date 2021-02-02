using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("QuestionViews")]
    [PrimaryKey("Id")]
    public class QuestionView
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int QuestionId { get; set; }
        [Column]
        public int UserId { get; set; }
    }
}
