using MemoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoQuiz
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private CategoryRepository _categoryRepository;
     
    
        public MainPage()
        {
            _categoryRepository= new CategoryRepository();
            InitializeComponent();
          
           
           
        }
        protected async override void OnAppearing()
        { 
            List<Category> items = await _categoryRepository.GetAllAsync(); 
           
            blobCollectionView.SelectedItem = null;

            
               

            var categoryList = await _categoryRepository.GetAllAsync();

                blobCollectionView.ItemsSource = categoryList;
            
        }

        async void blobCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Category category))
                return;

            var postsPage = new ItemPage(category.Id);
            await Navigation.PushAsync(postsPage);
        }
        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddPage()));
        }

        private void ToolbarItem_Clicked_Remove(object sender, EventArgs e)
        {

        }
    }
}
