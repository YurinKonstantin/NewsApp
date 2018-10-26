﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Россия", TargetType=typeof(TabbedPageRus) },
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

       

    }
}