using MemoQuiz.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace MemoQuiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
        int CategoryId;
        ItemRepository _itemRepository;
       
        public ItemPage(int categoryId)
        {

            InitializeComponent();
            _itemRepository = new ItemRepository();
            CategoryId = categoryId;
          
        }
        protected async override void OnAppearing()
        {
            var itemList =await _itemRepository.GetAllAsync(CategoryId);
            postCollection.ItemsSource = itemList;
        }
    
        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddItemPage(CategoryId)));
        }

        private async void StartLearn(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NavigationPage(new QuizPage(CategoryId)));
        }
        public async void PickAndReadTextFileAsync(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.Android, new[] { "text/plain" } }
    });

            var options = new PickOptions
            {
                PickerTitle = "Please select a text file",
                FileTypes = customFileType,
            };

            try
            {
                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    using (var stream = await result.OpenReadAsync())
                    using (var reader = new StreamReader(stream))
                    {
                        string fileContent = await reader.ReadToEndAsync();
                        // Передаем содержимое файла для обработки в метод AddFile
                        await AddFile(fileContent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking and reading file: {ex.Message}");
            }
        }
        private async Task SaveInFile()
        {
           var list =  await _itemRepository.GetAllAsync(CategoryId);
            try
            {
                var filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "backUp");

                using (var writer = File.CreateText(filePath))
                {
                    foreach (var item in list)
                    {
                        writer.Write(item);
                    }
                   
                }

                //Console.WriteLine($"File '{fileName}' saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task AddFile(string fileContent)
        {
            // Разбиваем содержимое файла на строки
            string[] lines = fileContent.Split('\n');

            // Проходимся по каждой строке в файле
            for (int i = 0; i < lines.Length; i++)
            {
                // Разбиваем строку на значения (предположим, что они разделены '-')
                string[] s = lines[i].Split('-');

                // Проверяем, чтобы избежать выхода за пределы массива
                if (s.Length >= 2)
                {
                    Item item = new Item();
                    item.Term = s[0].Trim(); // Убираем возможные пробелы
                    item.Definition = s[1].Trim(); // Убираем возможные пробелы
                    item.CategoryId = CategoryId;
                    await _itemRepository.AddAsync(item);

                }
            }
        }

        //private async  void SaveInFile(object sender, EventArgs e)
        //{
            
        //    try
        //    {
        //        var filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "backUp.txt");
        //        if (filePath == null)
        //        {
        //            return;
        //        }
        //        using (var writer = new StreamWriter(filePath))
        //        {
        //            foreach (var item in await _itemRepository.GetAllAsync(CategoryId))
        //            {
        //                string s = item.Term + "-" + item.Definition;
        //                await writer.WriteLineAsync(s);
        //            }

        //        }

        //        //Console.WriteLine($"File '{fileName}' saved to: {filePath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}
    }
}