using System;
using System.Collections.Generic;
using MyListNavigation.Models;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace MyListNavigation.Views
{
    public partial class MyListPage : ContentPage
    {

       public ExampleData Users;
     
        
        public MyListPage()
        {
            InitializeComponent();
            GetProducts();
            InitSearchBar();

        }

        private void InitSearchBar()
        {
            sb_search.TextChanged += (s, e) => FilterItem(sb_search.Text);
            sb_search.SearchButtonPressed += (s, e) => FilterItem(sb_search.Text);

        }
        private void FilterItem(string filter)
        {
            
            flowlistview.BeginRefresh();
            if(string.IsNullOrEmpty(filter))
            {
                flowlistview.ItemsSource = Users.Data;
            }
            else
            {
              
                flowlistview.ItemsSource = Users.Data.Where(User => User.FirstName.ToLower().Contains(filter.ToLower()) ||  User.LastName.ToLower().Contains(filter.ToLower()));
            }
            flowlistview.EndRefresh();

        }
        private async void GetProducts()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://internshiptaskuserslist.azurewebsites.net/api/users?code=gbgu4CbgdAlsS0xIVaNkckK4vTd0qIFNxaQYzIHLaqyomquJwuy/ig==");
            Users =  JsonConvert.DeserializeObject<ExampleData>(response);
            flowlistview.ItemsSource = Users.Data;
            
        }

        
        async private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            if (flowlistview.SelectedItem == null)
                return;

            var myselecteditem = e.Item as MyListModel;



            int id = myselecteditem.Id;
            await Navigation.PushAsync(new MyPage1(id));
                              
                ((ListView)sender).SelectedItem = null;

        }
    }
}