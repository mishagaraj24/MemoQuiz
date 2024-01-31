using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoQuiz
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "MemoQuiz.db";
        public static SQLiteAsyncConnection db;
        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteAsyncConnection(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
