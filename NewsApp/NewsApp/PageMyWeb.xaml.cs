using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMyWeb : ContentPage
	{
		public PageMyWeb (string text)
		{
			InitializeComponent ();
            myUri2 = text;
            
        }
        string myUri2 = null;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
          await  MyParser();

        }
        public async Task MyParser()
        {
            //  var html = "<h1>Lenta.ru</h1>" + "<a href=\"http://msdn.com/ru-ru/\">Русский MSDN</a>";
            var html = "<h1>Lenta.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
            HttpClient client = new HttpClient();

           HttpResponseMessage response = await client.GetAsync(myUri2);
            //  await prog.ProgressTo(.3, 250, Easing.Linear);
            response.EnsureSuccessStatusCode();


            //**Достаем заголовок
            string responseBody = await response.Content.ReadAsStringAsync();
              int x = responseBody.IndexOf("<h1");

              responseBody = responseBody.Substring(x);
              int x1 = responseBody.IndexOf("</h1>");
              html = responseBody.Substring(0, x1 + 5);
              responseBody = responseBody.Substring(x1 + 5);
          
             //**Достаем фото
             string varstr;
             x = responseBody.IndexOf("<img");
             varstr = responseBody.Substring(x);
             x1 = varstr.IndexOf("/div>");
             varstr = varstr.Substring(0, x1 + 5);

             html += varstr;
         
           x = responseBody.IndexOf("<p>");
           responseBody = responseBody.Substring(x);
           x1 = responseBody.LastIndexOf("p>");
           //html += responseBody.Substring(0, x1 + 4);


           html += responseBody.Substring(0, x1 + 2);

            try
            {


                string viriz;
                int s = html.IndexOf("<aside");
                // viriz = responseBody.Substring(s);
                int xs = html.IndexOf("</aside>");
                //viriz = viriz.Substring(0, xs + 8);
                int cc = xs - s;
                html = html.Remove(s, cc);
            }
            catch(Exception)
            {

            }
        
            html += "<h1>Lenta.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
            //MessageDialog messageDialog = new MessageDialog(varstr);
            //await messageDialog.ShowAsync();
            
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webLab.Source = htmlSource;

        }
    }
}