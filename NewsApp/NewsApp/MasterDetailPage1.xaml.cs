using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            ClassSetUpUser.OpenSetUp();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;
            

                 var page = (Page)Activator.CreateInstance(item.TargetType);
               // var page=new MasterDetailPage1Detail();
                page.Title = item.Title;

                Detail = new NavigationPage(page);
            IsPresented = false;
          MasterPage.ListView.SelectedItem = null;
        }

        private async void MenuItem1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSetUpUser());
        }
    }
}