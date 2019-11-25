using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WaldViewer.Views;
using WaldViewer.Services;

namespace WaldViewer
{
  public partial class App : Application
  {
    public static INavigationService NavigationService { get; } = new NavigationService();

    public App()
    {
      InitializeComponent();

      MainPage = new NavigationPage( new MainPage());
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
