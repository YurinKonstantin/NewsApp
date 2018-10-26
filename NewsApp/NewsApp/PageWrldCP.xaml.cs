using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageWrldCP : ContentPage
	{
		public PageWrldCP ()
		{
			InitializeComponent ();
            Col1.Add(new RSSFeedItem() { Title = "Загрузка новостей", FigShow = false, Description = "Мы подготавлимаем новости для вас", ButShow = false });

            phonesList.ItemsSource = Col1;
        }
        ScreenMetrics metrics = DeviceDisplay.ScreenMetrics;
        public ObservableCollection<RSSFeedItem> Col1 = new ObservableCollection<RSSFeedItem>();
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Col1.Count <= 1)
            {
               await Task.Run(() => zagruzka1());
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Button button = (Button)sender;
            //string ID = button.CommandParameter.ToString();
            string action = await DisplayActionSheet("Действия", "Отмена", null, "Сохранить", "Открыть", "Прочесть");
            if (action == null)
                return;
            BindableObject bindableObject = sender as BindableObject;
            if (bindableObject != null)
            {
                RSSFeedItem note = bindableObject.BindingContext as RSSFeedItem;
                if (action == "Сохранить")
                {
                    try
                    {
                        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                        bool doesExist = File.Exists(fileName);
                        string text = null;
                        if (doesExist == true)
                        {
                            text = File.ReadAllText(fileName);
                        }


                        text += note.Title.ToString() + "\t" + note.Description.ToString() + "\t" + note.Enclosure.ToString() + "\t" + note.Link + "\n";
                        File.WriteAllText(fileName, text);
                        DependencyService.Get<Interface1>().LongAlert("Cохранено");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("В файле ошибка ", ex.ToString(), "OK");
                    }
                }
                else
                {
                    if (action == "Открыть")
                    {
                        if (note != null)

                            if (note.Enclosure != null)
                                //   await Navigation.PushModalAsync(new Page4(selectedPhone.Link));
                                await Navigation.PushAsync(new PageWebView(note.Link));
                    }
                    if (action == "Прочесть")
                    {
                        DependencyService.Get<Interface1>().Speak(note.Description);
                    }

                }
            }


        }
        async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            await zagruzka1();
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }
        public async Task zagruzka1()
        {

            try
            {
                // await prog.ProgressTo(.1, 250, Easing.Linear);

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(ClassURI.WrldCP);
                //  await prog.ProgressTo(.3, 250, Easing.Linear);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                //   await prog.ProgressTo(.6, 250, Easing.Linear);
                var xdoc = XDocument.Parse(responseBody);

                var id = 0;
                string title = "title";
                string description = "title";
                string link = "title";
                string pubDate = "title";
                string url1 = "title";
                string category = "Category";
                Boolean H = true;
                Double w = 1;
                Col1.Clear();
                foreach (var item in xdoc.Descendants("item"))
                {

                    try
                    {
                        title = item.Element("title").Value;
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        link = item.Element("link").Value;
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        description = item.Element("description").Value;
                    }
                    catch (Exception ex)
                    {
                        // await DisplayAlert("Получили ошибку description ", ex.Message, "OK");
                    }
                    try
                    {
                        pubDate = item.Element("pubDate").Value;
                    }
                    catch (Exception ex)
                    {
                        pubDate = "00.00.0000";

                    }

                    try
                    {
                        url1 = item.Element("enclosure").Attribute("url").Value;
                        H = true;
                    }
                    catch (Exception ex)
                    {
                        url1 = "https://icdn.lenta.ru/assets/webpack/images/04ceff52e5b673154a365683e768578e.lenta_og.png";
                        H = false;

                    }
                    try
                    {
                        category = item.Element("category").Value;
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Получили ошибку category ", ex.Message, "OK");
                    }
                    id++;

                    if (H)
                    {
                        w = 0.35 * (metrics.Width - 5);
                    }
                    else
                    {
                            w = 0.35 * (metrics.Width);
                        }
                    Col1.Add(new RSSFeedItem
                    {
                        Title = title,
                        Description = description,
                        Link = link,
                        PublishDate = pubDate,
                        Id = id,
                        Enclosure = url1,
                        Category = category,
                        FigShow= ClassSetUpUser.FigShow, 
                        FigDesc= ClassSetUpUser.FigDesc,
                        h = w,
                        istochnic = "Lenta.ru"

                    });
                    // save values to database
                }
                if (Col1.Count == 0)
                {
                    Col1.Add(new RSSFeedItem() { Title = "Новостей по данной теме нет", Description = "Потяните вниз по списку данных что бы обновить.", ButShow = false, FigShow = false });
                }
                }
                else
                {
                    Col1.Add(new RSSFeedItem() { Title = "Подключение к сети интернет ограничено или отсутствует", Description = "Потяните вниз по списку данных что бы обновить.", ButShow = false, FigShow = false });

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //   await prog.ProgressTo(1, 250, Easing.Linear);
            }
            // RSSFeedItem.h = 0.75 * phonesList.Width;
            //   await prog.ProgressTo(1, 250, Easing.Linear);
            // await prog.ProgressTo(0, 250, Easing.Linear);
        }


        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            RSSFeedItem selectedPhone = e.Item as RSSFeedItem;
            if (selectedPhone != null)

                if (selectedPhone.Enclosure != null)
                    //   await Navigation.PushModalAsync(new Page4(selectedPhone.Link));
                    await Navigation.PushAsync(new PageWebView(selectedPhone.Link));


        }
    }
}