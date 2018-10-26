﻿using System;
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
	public partial class PageWrldPolitika : ContentPage
	{
		public PageWrldPolitika ()
		{
			InitializeComponent ();
            Col1.Add(new RSSFeedItem() { Title = "Загрузка новостей", FigShow = false, Description = "Мы подготавлимаем новости для вас", ButShow = false });

            phonesList.ItemsSource = Col1;
            MyComande = new ClassComande();

        }
        ScreenMetrics metrics = DeviceDisplay.ScreenMetrics;
        ClassComande MyComande { get; set; }
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

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    List<RSSFeedItem> rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = ClassURI.WrldPolitika, Istochnik = "Lenta.ru" }, metrics.Width)));
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
                        if (v.Category == "Политика")
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
                rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = @"https://www.vedomosti.ru/rss/articles", Istochnik = "Vedomosti.ru" }, metrics.Width)));
                poz = 0;
                if (rSSFeedItems.Count != 0)
                {
                    foreach (var v in rSSFeedItems)
                    {
                        if (v.Category == "Политика")
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
            catch
            {

            }
          
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