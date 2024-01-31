using MemoQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoQuiz
{
    public class ItemRepository
    {
        protected Context db;
        public ItemRepository()
        {
            db = new Context();




        }

        public async Task<List<Item>> GetAllAsync(int categoryId)
        {

            return await db.Items.Where(x =>x.CategoryId == categoryId).ToListAsync();

        }

        public async Task AddAsync(Item item)
        {
            await db.Items.AddAsync(item);
            await db.SaveChangesAsync();
        }
        public async Task<Item> GetItemAsync(int id)
        {
            return await db.Items
              .Where(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            db.Items.Update(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await db.Items
               .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                db.Items.Remove(item);
                await db.SaveChangesAsync();
            }
        }
    }
}
