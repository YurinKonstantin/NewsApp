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
	public partial class PageWorldAll : ContentPage
	{
		public PageWorldAll ()
		{
			InitializeComponent ();
            Col1.Add(new RSSFeedItem() { Title = "Загрузка новостей", FigShow = false, Description = "Мы подготавлимаем новости для вас", ButShow = false });
            phonesList.ItemsSource = Col1;
            MyComande = new ClassComande();
        }
        ClassComande MyComande { get; set; }
        public ObservableCollection<RSSFeedItem> Col1 = new ObservableCollection<RSSFeedItem>();
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            frame.IsVisible = false;
            if (Col1.Count <= 1)
            {
               await Task.Run(() => zagruzka1());
            }
        }
        ScreenMetrics metrics = DeviceDisplay.ScreenMetrics;

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
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    List<RSSFeedItem> rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = ClassURI.WrldAll, Istochnik = "Lenta.ru" }, metrics.Width)));
                Col1.Clear();
                if (rSSFeedItems.Count != 0)
                {
                   
                    foreach (var v in rSSFeedItems)
                    {
                        Col1.Add(v);
                    }
                }
                rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = @"https://www.vesti.ru/vesti.rss", Istochnik = "Vesti.ru" }, metrics.Width)));
                int poz = 0;
                if (rSSFeedItems.Count != 0)
                {
                   
                    foreach (var v in rSSFeedItems)
                    {
                        if (v.Category == "В мире")
                        {
                            if (Col1.Count > poz * 2 + 1)
                            {
                                Col1.Insert(poz * 2 + 1, v);
                            }
                            else
                            {
                                Col1.Add(v);
                            }
                            poz++;
                        }
                    }
                }
                rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = @"https://news.rambler.ru/rss/world/", Istochnik = "rambler.ru" }, metrics.Width)));
                poz = 0;
                if (rSSFeedItems.Count != 0)
                {
                  
                    foreach (var v in rSSFeedItems)
                    {

                        if (Col1.Count > poz * 2 + 1)
                        {
                            Col1.Insert(poz * 2 + 1, v);
                        }
                        else
                        {
                            Col1.Add(v);
                        }
                        poz++;

                    }
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
                {
                    if (selectedPhone.istochnic == "Lenta.ru")
                    {
                        await Navigation.PushAsync(new PageMyWeb(selectedPhone.Link));
                    }
                    else
                    {
                        await Navigation.PushAsync(new PageWebView(selectedPhone.Link));
                    }

                }
            //   await Navigation.PushModalAsync(new Page4(selectedPhone.Link));



        }
        RSSFeedItem note1 = new RSSFeedItem();

        private async void Button_Clicked(object sender, EventArgs e)
        {
            note1 = null;
            BindableObject bindableObject = sender as BindableObject;
            if (bindableObject != null)
            {
                note1 = bindableObject.BindingContext as RSSFeedItem;
            }
            frame.IsVisible = true;

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            frame.IsVisible = false;
            note1 = null;
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
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


                text += note1.Title.ToString() + "\t" + note1.Description.ToString() + "\t" + note1.Enclosure.ToString() + "\t" + note1.Link + "\n";
                File.WriteAllText(fileName, text);
                DependencyService.Get<Interface1>().LongAlert("Cохранено");
            }
            catch (Exception ex)
            {
                await DisplayAlert("В файле ошибка ", ex.ToString(), "OK");
            }
            finally
            {
                note1 = null;
                frame.IsVisible = false;
            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            try
            {
                if (note1 != null)
                    if (note1.Enclosure != null)
                        await Navigation.PushAsync(new PageWebView(note1.Link));
            }
            catch (Exception)
            {

            }
            finally
            {
                frame.IsVisible = false;
            }

        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            try
            {
                DependencyService.Get<Interface1>().Speak(note1.Description);
            }
            catch (Exception)
            {

            }
            finally
            {
                frame.IsVisible = false;
            }
        }
    }
}