using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("Questions")]
    [PrimaryKey("Id")]
    public class NewQuestion
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public int UserId { get; set; }
        [Column]
        public int Category { get; set; }
        [Column]
        public string Question { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public DateTime TimeStamp { get; set; }
    }
}
