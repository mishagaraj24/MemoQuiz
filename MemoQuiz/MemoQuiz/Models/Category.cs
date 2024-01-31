using SQLite;
using System.Collections.Generic;

namespace MemoQuiz.Models
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement, Column("_CategoryId")]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
