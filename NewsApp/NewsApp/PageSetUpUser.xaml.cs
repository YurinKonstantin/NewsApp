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
	public partial class PageSetUpUser : ContentPage
	{
		public PageSetUpUser ()
		{
			InitializeComponent ();
            fig.On = ClassSetUpUser.FigShow;
            opis.On = ClassSetUpUser.FigDesc;
            
		}

    

     

        private void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
        {
            ClassSetUpUser.FigShow = e.Value;
            ClassSetUpUser.SaveSetUp();
        }

        private void opis_OnChanged(object sender, ToggledEventArgs e)
        {
            ClassSetUpUser.FigDesc = e.Value;
            ClassSetUpUser.SaveSetUp();
        }
    }
}