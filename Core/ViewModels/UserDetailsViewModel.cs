using PetaPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    [TableName("UserDetails")]
    public class UserDetailsViewModel
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
        [Column]
        public int QuestionsAsked { get; set; }
        [Column]
        public int QuestionsAnswered { get; set; }
        [Column]
        public int QuestionsSolved { get; set; }
        [Column]
        public int Likes { get; set; }
        [Column]
        public int Dislikes { get; set; }
    }
}
