using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("LikeDislike")]
    [PrimaryKey("Id")]
    public class LikeDislike
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int AnswerId { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public bool Sentiment { get; set; }
    }
}
