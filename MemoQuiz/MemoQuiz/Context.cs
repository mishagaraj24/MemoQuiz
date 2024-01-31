using MemoQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Xamarin.Essentials;

namespace MemoQuiz
{
    public class Context: DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public object Item { get; internal set; }

        public Context()
        {
            //SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "db.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
