using MyListNavigation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyListNavigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPage1 : ContentPage
    {
        string id;
        
        public MyPage1(int Name)
        {
            InitializeComponent();
            id = Name.ToString();
            GetInfo();
            
        }

      async private void GetInfo()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://internshiptaskuserslist.azurewebsites.net/api/users/"+ id + "?code=9XuCxWZqJavOAWHPcWD/97mMeJkK0mSVMA9A6MQ9n4R1B/6fpsxGqw==");
            User users = new User();
            UserDetails szczegoly = new UserDetails();
           
            users = JsonConvert.DeserializeObject<User>(response);
            
            FirstName.Text = users.Data.FirstName +" "+ users.Data.LastName;
            Age.Text = "Wiek: " + users.Data.Age.ToString();
            City.Text = users.Data.City;
       
        }
    }
}