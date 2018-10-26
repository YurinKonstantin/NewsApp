using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageZakladka : ContentPage
    {
        public ObservableCollection<RSSFeedItem> Col1 = new ObservableCollection<RSSFeedItem>();
        public PageZakladka()
        {
            InitializeComponent();
            phonesList.ItemsSource = Col1;
           
            
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                Button button = (Button)sender;
                string ID = button.CommandParameter.ToString();
                BindableObject bindableObject = sender as BindableObject;
                if (bindableObject != null)
                {
                    RSSFeedItem note = bindableObject.BindingContext as RSSFeedItem;

                    int x = 0;
                    foreach (var cl in Col1)
                    {
                        if (cl.Title == note.Title)
                        {
                            Col1.RemoveAt(x);
                            break;

                        }
                        x++;
                    }

                    string text = String.Empty;
                    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                    bool doesExist = File.Exists(fileName);
                    foreach (var cl in Col1)
                    {
                        text += cl.Title.ToString() + "\t" + cl.Description.ToString() + "\t" + cl.Enclosure.ToString() + "\t" + cl.Link + "\n";
                    }
                    File.WriteAllText(fileName, text);
                    DependencyService.Get<Interface1>().LongAlert("Удалено");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("В файле ошибка ", ex.ToString(), "OK");
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Col1.Count == 0)
            {
                Task.Run(() => openLoad());
            }
        }
        async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
           await  openLoad();
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }
        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            RSSFeedItem selectedPhone = e.Item as RSSFeedItem;
            if (selectedPhone != null)

                if (selectedPhone.Enclosure != null)
                    //   await Navigation.PushModalAsync(new Page4(selectedPhone.Link));
                    await Navigation.PushAsync(new PageWebView(selectedPhone.Link));


        }
         public async Task openLoad()
        {
            try
            {
                Col1.Clear();
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                bool doesExist = File.Exists(fileName);
                if (doesExist == true)
                {
                    string text = File.ReadAllText(fileName);
                    string[] line = text.Split('\n');
             
                    for (int i = 0; i < line.Length-1; i++)
                    {
                        textNoNews.IsVisible = false;
                        try
                        {
                            string[] classD = line[i].Split('\t');
                            if (classD.Length == 4)
                                Col1.Add(new RSSFeedItem() { Title = classD[0].ToString(), Description = classD[1].ToString(), Enclosure = classD[2].ToString(), Link = classD[3] });
                            else
                            {
                                if (classD.Length == 3)
                                    Col1.Add(new RSSFeedItem() { Title = classD[0].ToString(), Description = classD[1].ToString(), Enclosure = classD[2].ToString() });
                            }
                        }
                        catch(Exception)
                        {

                        }
                    }
                }
                else
                {
                    await DisplayAlert("Сохраненые данные", "У вас нет сохраненных данных", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.ToString(), "OK");
            }

        }

    }
}