using MemoQuiz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoQuiz
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuizPage : ContentPage
	{
        
        ItemRepository _itemRepository;
         Random random = new Random(); 
        int randomIndex;
        int newRandomIndex;
        int categoryId;
        int count;
        List<Item> items = new List<Item>();
        public  QuizPage (int id)
		{
			InitializeComponent ();
            
            categoryId = id;
            BindingContext = new Item();
           // NextWord(this, new EventArgs()); //null
            _itemRepository = new ItemRepository();
            
        }
        protected async override void OnAppearing()
        {
            items = await _itemRepository.GetAllAsync(categoryId);
            wordCount.Text = items.Count().ToString();
             NextWord(this, new EventArgs());

        }
        public  void NextWord(object sender, EventArgs e)
        { 
            randomIndex =  GetRandomIndex();
           var  item =  items.FirstOrDefault(x => x.Id == randomIndex);
            term.Text = item.Term;
            definition.Text = string.Empty;
            count = items.Count();
            count--;
            wordCount.Text = count.ToString(); 
        }

        public  void ShowWord(object sender, EventArgs e)
        {

            var item =  items.FirstOrDefault(x => x.Id == randomIndex);
            definition.Text = item.Definition;

        }

        private  int GetRandomIndex()
        {
            var allIds = items.Select(entity => entity.Id).ToList();
            
            int randomW = 0;
            if (allIds != null)
            {

                 randomW = allIds.OrderBy(id => random.Next()).FirstOrDefault();
            }
            return randomW;
        }

        private  void Button_Clicked(object sender, EventArgs e)
        {
            items.Remove(items.FirstOrDefault(x => x.Id == randomIndex));
        }
    }
}