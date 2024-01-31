using MemoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoQuiz
{
    
    public partial class AddPage : ContentPage
    {
        private CategoryRepository _categoryRepository;
        public AddPage()
        {
            InitializeComponent();
            _categoryRepository= new CategoryRepository();
          //BindingContext = 
           
    }
        async void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            var category = new Category { Name = categoryName.Text };

           await _categoryRepository.AddAsync(category);

             await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
           
                await this.Navigation.PopAsync();
           
           
        }
    }
}