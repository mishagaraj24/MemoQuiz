using MemoQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoQuiz
{
    public class CategoryRepository    {
        protected Context db;
        public CategoryRepository()
        {
            db = new Context();
        



        }

        public async Task<List<Category>>GetAllAsync()
        {
            
            return await db.Categories.ToListAsync();

        }

        public async Task AddAsync(Category item)
        {
             await db.Categories.AddAsync(item);
            await db.SaveChangesAsync();
        }
        public async Task<Category> GetItemAsync(int id)
        {
            return await db.Categories
              .Where(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateItemAsync(Category item)
        {
            db.Categories.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
           var category =  await db.Categories
              .Where(item => item.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
        }

    }
}
