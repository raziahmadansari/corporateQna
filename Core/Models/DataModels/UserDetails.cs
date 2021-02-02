using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("Users")]
    [PrimaryKey("Id")]
    public class UserDetails
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string Username { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Designation { get; set; }
        [Column]
        public string Team { get; set; }
        [Column]
        public string Category { get; set; }
        [Column]
        public string Location { get; set; }
    }
}
