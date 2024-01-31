using MemoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoQuiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        int CategoryId;
        ItemRepository _itemRepository;
        public AddItemPage(int categoryId)
        {
            InitializeComponent();
            _itemRepository= new ItemRepository();
            CategoryId = categoryId;
        }

        protected async void Save_Clicked(object sender, EventArgs e)
        {
            var newItem = new Item
            {
               CategoryId = CategoryId,
                Term = postCell.Text,
                Definition = titleCell.Text
            };

            try
            {
               await _itemRepository.AddAsync(newItem);
                   
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            await Navigation.PopModalAsync();
        }

        protected async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}