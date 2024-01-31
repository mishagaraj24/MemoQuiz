using SQLite;
using System.Collections.Generic;

namespace MemoQuiz.Models
{
    [Table("Item")]
    public class Item
    {

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public bool NeedToRepeat { get; set; } = true;
        public int CategoryId { get; set; }

        // Навигационное свойство, представляющее связь с моделью CategoryViewModel

        public Category Category { get; set; }

    }
    
}
