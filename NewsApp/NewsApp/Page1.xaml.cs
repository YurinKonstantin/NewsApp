﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public ObservableCollection<RSSFeedItem> Col1 = new ObservableCollection<RSSFeedItem>();
        public Page1()
        {
            InitializeComponent();



            phonesList.ItemsSource = Col1;
            MyComande = new ClassComande();
        }
      
        ClassComande MyComande { get; set; }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            frame.IsVisible = false;
            try
            {
                Col1.Add(new RSSFeedItem() { Title = "Загрузка новостей", FigShow = false, Description = "Мы подготавлимаем новости для вас", ButShow = false });
                if (Col1.Count <= 1)
                {
                    await Task.Run(() => zagruzka1(this.Title.ToString()));

                }
            }
            catch (Exception)
            {

            }

        }
        async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            await zagruzka1(this.Title.ToString());
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }
        public async Task zagruzka1(string titlestring)
        {
            var metrics = DeviceDisplay.MainDisplayInfo;
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Connection to internet is available

                    List<string> arayUri = new List<string>();
                    //var Urr = ClassURI.News24;
                    // await prog.ProgressTo(.1, 250, Easing.Linear);
                    switch (titlestring)
                    {


                        case "Спорт":
                            arayUri.Add(ClassURI.SportAll);
                            break;
                        case "Футбол":
                            arayUri.Add(ClassURI.Sportfootball);
                            break;
                        case "Хоккей":
                            arayUri.Add(ClassURI.Sporthockey);
                            break;
                        case "Бокс и ММА":
                            arayUri.Add(ClassURI.Sportboxing);
                            break;
                        case "Зимние виды":
                            arayUri.Add(ClassURI.Sportwinter);
                            break;
                        case "Летние виды":
                            arayUri.Add(ClassURI.Sportother);
                            break;
                        case "Госэкономика":
                            arayUri.Add(ClassURI.EconomicaGos);
                            break;
                        case "Бизнес":
                            arayUri.Add(ClassURI.EconomicaBisnes);
                            break;
                        case "Рынки":
                            arayUri.Add(ClassURI.EconomicaRinoc);
                            break;
                        case "Деньги":
                            arayUri.Add(ClassURI.EconomicaMany);
                            break;

                    }


                    List<RSSFeedItem> rSSFeedItems;
                    int d = 0;
                    foreach (var Urr in arayUri)
                    {
                        


                            rSSFeedItems = await Task.Run(() => (MyComande.zagruzka1(new ClassIstochnik() { Urr = Urr, Istochnik = "Lenta.ru" }, metrics.Width)));

                            if (rSSFeedItems.Count != 0)
                            {
                                Col1.Clear();
                                d++;
                                foreach (var v in rSSFeedItems)
                                {
                                    v.ButShow = true;
                                    Col1.Add(v);


                                }
                            }
                         
                        

                    }
                    if (d == 0)
                    {
                        Col1.Clear();
                        Col1.Add(new RSSFeedItem() { Title = "Новостей по данной теме нет", Description = "Потяните вниз по списку данных что бы обновить.", ButShow = false, FigShow = false });

                    }
                }
                else
                {
                    Col1.Clear();
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
                   if(!ClassSetUpUser.MyWebShow)
                    {
await Navigation.PushAsync(new PageMyWeb(selectedPhone));
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

            Image.Source = note1.Enclosure;
            TextTitle.Text = note1.Title;
            TextDesc.Text = note1.Description;
            TextIst.Text = note1.istochnic;
            //  ImageFrame.HeightRequest = note1.h;
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
                    {
                        if (!ClassSetUpUser.MyWebShow)
                        {
                            await Navigation.PushAsync(new PageMyWeb(note1));
                        }
                        else
                        {
                            await Navigation.PushAsync(new PageWebView(note1.Link));
                        }
                    }

            }
            catch (Exception)
            {

            }
            finally
            {
                frame.IsVisible = false;
            }

        }

        CancellationTokenSource cts;
        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? false)
                return;

            cts.Cancel();
        }
        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            try
            {
                ButStop.IsVisible = true;
                //  DependencyService.Get<Interface1>().Speak(note1.Description);
                await SpeakNowDefaultSettings(note1.Description);
                ButStop.IsVisible = false;

            }
            catch (Exception)
            {

            }
            finally
            {
                // frame.IsVisible = false;
            }
        }
        public async Task SpeakNowDefaultSettings(string text)
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            var locale = locales.FirstOrDefault();

            var settings = new SpeechOptions()
            {
                Volume = 10 / 14,
                Pitch = 1,
                Locale = locale
            };
            cts = new CancellationTokenSource();
            await TextToSpeech.SpeakAsync(text, cancelToken: cts.Token);

            // This method will block until utterance finishes.
        }

        public void SpeakNowDefaultSettings2(string text)
        {
            TextToSpeech.SpeakAsync(text).ContinueWith((t) =>
            {
                // Logic that will run after utterance finishes.

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ButStop_Clicked(object sender, EventArgs e)
        {
            CancelSpeech();
        }
    }
}