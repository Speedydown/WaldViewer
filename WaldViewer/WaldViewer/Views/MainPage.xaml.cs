﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WaldViewer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaldViewer.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MainPage : TabbedPage
  {
    public MainPage()
    {
      InitializeComponent();

    
    }
  }
}
