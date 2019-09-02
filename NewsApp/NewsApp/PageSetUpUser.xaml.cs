using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//PofodaShow - показывает прогноз погоды
namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageSetUpUser : ContentPage
	{
		public PageSetUpUser ()
		{
			InitializeComponent ();
            fig.On = ClassSetUpUser.FigShow;
            opis.On = ClassSetUpUser.FigDesc;
            MyWeb.On = ClassSetUpUser.MyWebShow;
           // var myValue = Preferences.Get("PofodaShow", "True");

        }

    

     

        private async void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
        {
            ClassSetUpUser.FigShow = e.Value;
          await  ClassSetUpUser.SaveSetUp();
        }

        private async void opis_OnChanged(object sender, ToggledEventArgs e)
        {
            ClassSetUpUser.FigDesc = e.Value;
           await ClassSetUpUser.SaveSetUp();
        }

        private async void MyWeb_OnChanged(object sender, ToggledEventArgs e)
        {
            ClassSetUpUser.MyWebShow = e.Value;
           await ClassSetUpUser.SaveSetUp();
        }

        private void Fig_OnChanged(object sender, ToggledEventArgs e)
        {

        }

        private async void Gps_OnChanged(object sender, ToggledEventArgs e)
        {
            await DisplayAlert("В файле ошибка ", e.Value.ToString(), "OK");
            Preferences.Set("PofodaShow", e.Value);
        }
    }
}