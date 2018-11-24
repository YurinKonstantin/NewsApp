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
        string ist;

        public PageMyWeb (RSSFeedItem rSSFeedItem)
		{
			InitializeComponent ();
            myUri2 = rSSFeedItem.Link;
            ist = rSSFeedItem.istochnic;


        }
        string myUri2 = null;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(ist=="Lenta.ru")
            {
                await MyParser();
            }
            if (ist == "Vedomosti.ru")
            {
                await MyParserVedomosti();
            }
            if (ist == "Vesti.ru")
            {
                await MyParserVesti();
            }
            if(ist== "rambler.ru")
            {
                webLab.Source = myUri2;
            }


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
        public async Task MyParserVedomosti()
        {
            //  var html = "<h1>Lenta.ru</h1>" + "<a href=\"http://msdn.com/ru-ru/\">Русский MSDN</a>";
            var html = "<h1>Vedomosti.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
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
            try
            {
                string varstr;
                x = responseBody.IndexOf("<img");
                varstr = responseBody.Substring(x);
                x1 = varstr.IndexOf("/div>");
                varstr = varstr.Substring(0, x1 + 5);

                html += varstr;
            }
            catch (Exception)
            {

            }

            x = responseBody.IndexOf("<p");
            responseBody = responseBody.Substring(x);
            x1 = responseBody.IndexOf("/p>");
            //html += responseBody.Substring(0, x1 + 4);


            html += responseBody.Substring(0, x1 + 2);

            //responseBody= responseBody.Substring(0, x1 + 2);

            x = responseBody.IndexOf("<p");
            responseBody = responseBody.Substring(x);
            x1 = responseBody.IndexOf("/p>");
            //html += responseBody.Substring(0, x1 + 4);


            html += responseBody.Substring(0, x1 + 2);

            try
            {
                string viriz;
                int s = html.IndexOf("<aside");
                // viriz = responseBody.Substring(s);
                int xs = html.IndexOf("</aside>");
                //viriz = viriz.Substring(0, xs + 8);
                html = html.Remove(s, xs - s);
            }
            catch (Exception)
            {

            }


            html += "<h1>Vedomosti.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
            //MessageDialog messageDialog = new MessageDialog(varstr);
            //await messageDialog.ShowAsync();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webLab.Source = htmlSource;
        }
        public async Task MyParserVesti()
        {
            //  var html = "<h1>Lenta.ru</h1>" + "<a href=\"http://msdn.com/ru-ru/\">Русский MSDN</a>";
            var html = "<h1>Vedomosti.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(myUri2);
            //  await prog.ProgressTo(.3, 250, Easing.Linear);
            response.EnsureSuccessStatusCode();


            //**Достаем заголовок
            string responseBody = await response.Content.ReadAsStringAsync();
            int x = responseBody.IndexOf("<h3");

            responseBody = responseBody.Substring(x);
            int x1 = responseBody.IndexOf("</h3>");
            html = responseBody.Substring(0, x1 + 5);
            responseBody = responseBody.Substring(x1 + 5);
            try
            {
       string varstr;
            x = responseBody.IndexOf("<img");
            varstr = responseBody.Substring(x);
            x1 = varstr.IndexOf("/div>");
            varstr = varstr.Substring(0, x1 + 5);
                html += varstr;
            }
            catch (Exception)
            {

            }
            //**Достаем фото

            try
            {

            }
            catch (Exception)
            {

            }
           
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
            html = html.Remove(s, xs - s);
            }
            catch(Exception)
            {

            }
           

            html += "<h1>Vesti.ru</h1>" + @"<a href=" + myUri2 + @">Перейти на страницу статьи</a>";
            //MessageDialog messageDialog = new MessageDialog(varstr);
            //await messageDialog.ShowAsync();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webLab.Source = htmlSource;
        }
    }
}