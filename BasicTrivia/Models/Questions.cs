using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BasicTrivia.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string trivia { get; set; }
        public string Answer { get; set; }
        public string Choice1 { get; set; }
        public string Choice2 { get; set; }
        public string Choice3 { get; set; }
        public string Choice4 { get; set; }
        public string Choice5 { get; set; }        
    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}