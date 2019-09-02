using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;

        public MasterDetailPage1Master()
        {
            InitializeComponent();
           
            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
           
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }
            
            public MasterDetailPage1MasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Главное", TargetType=typeof(TabbedPageGlavnoe) },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Россия", TargetType=typeof(TabbedPageRus)},
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Мир", TargetType= typeof(TabbedPageWorld) },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Спорт", TargetType= typeof(TabbedPageSport) },
                    new MasterDetailPage1MenuItem { Id = 4, Title = "Экономика", TargetType= typeof(TabbedPageEconomica) },
                    new MasterDetailPage1MenuItem { Id = 5, Title = "Закладки", TargetType= typeof(PageZakladka) }
                    //  new MasterDetailPage1MenuItem { Id = 6, Title = "Настройки", TargetType= typeof(PageSetUpUser) }
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
        
            try
            {
              await  GeoLoc();
            }
            catch (Exception)
            {

            }
        }
        public async Task GeoLoc()
        {
            try
            {
                bool myValue = Preferences.Get("PofodaShow", true);
                if (myValue)
                {


                    // var location = await Geolocation.GetLastKnownLocationAsync();
                    var request = new GeolocationRequest(GeolocationAccuracy.Low);
                    var location = await Geolocation.GetLocationAsync(request);
                    //   search? request = lat % 3D - 122.1215134 % 26lon % 3D47.67398834

                    var placemarks = await Geocoding.GetPlacemarksAsync(location);

                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        var geocodeAddress =
                  $"AdminArea:       {placemark.AdminArea}\n" +
                  $"CountryCode:     {placemark.CountryCode}\n" +
                  $"CountryName:     {placemark.CountryName}\n" +
                  $"FeatureName:     {placemark.FeatureName}\n" +
                  $"Locality:        {placemark.Locality}\n" +
                  $"PostalCode:      {placemark.PostalCode}\n" +
                  $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                  $"SubLocality:     {placemark.SubLocality}\n" +
                  $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                  $"Thoroughfare:    {placemark.Thoroughfare}\n";
                        labGorod.Text = placemark.Locality.ToString();




                    }


                    if (location != null)
                    {
                        HttpClient client = new HttpClient();






                        var html = "<h1 style=\"color:red;\">Привет!</h1>" + "<a href=\"http://msdn.com/ru-ru/\">Русский MSDN</a>";
                        Uri myUri2 = new Uri("https://yandex.ru/pogoda/" + ClassCiti.Citi(placemark.Locality));



                        HttpResponseMessage response = await client.GetAsync(myUri2);
                        //  await prog.ProgressTo(.3, 250, Easing.Linear);
                        // response.EnsureSuccessStatusCode();

                        string responseBody = await response.Content.ReadAsStringAsync();
                        html = responseBody;

                        // muGeo.tempNow=
                        int x = responseBody.IndexOf("fact__temp-val");




                        responseBody = responseBody.Substring(x);

                        int x1 = responseBody.IndexOf("</span>");

                        var temp = responseBody.Substring(16, 10);

                        labTemp.Text = temp.Split('<')[0];
                        responseBody = responseBody.Substring(x1 + 5);

                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("В файле ошибка ", fnsEx.ToString(), "OK");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("В файле ошибка1 ", fneEx.ToString(), "OK");
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("В файле ошибка2 ", pEx.ToString(), "OK");
                // Handle permission exception
            }
            catch (Exception ex)
            {
                await DisplayAlert("В файле ошибка3 ", ex.ToString(), "OK");
                // Unable to get location
            }
        }
       

    }
}