using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.DataModels
{
    [TableName("Categories")]
    [PrimaryKey("Id")]
    public class Category
    {
        [Column]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
    }
}
