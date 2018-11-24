﻿using System;
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
	public partial class PageWebView : ContentPage
	{
		public PageWebView (string text)
		{
                InitializeComponent();
                textUri = text;
                webLab.Source = text;
        }
            string textUri = null;
   
    }
}