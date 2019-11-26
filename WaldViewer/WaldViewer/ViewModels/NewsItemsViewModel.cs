using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WaldViewer.BL;
using WaldViewer.Common;
using WaldViewer.SAL;
using WaldViewer.Views;
using Xamarin.Forms;

namespace WaldViewer.ViewModels
{
  public class NewsItemsViewModel : BaseViewModel
  {
    private bool _isRefreshing;
    public bool IsRefreshing
    {
      get { return _isRefreshing; }
      set
      {
        _isRefreshing = value;
        OnPropertyChanged();
      }
    }

    private ObservableCollection<NewsItem> newsItems;

    public ObservableCollection<NewsItem> NewsItems
    {
      get { return newsItems; }
      private set { newsItems = value;
        OnPropertyChanged();
      }
    }

    protected override async Task Load()
    {
      try
      {
        IsLoading = true;

        var result = await Authentication.Authenticate();

        if (!result)
        {
          //TODO do something
          return;
        }

        await DoLoadItems();
      }
      finally
      {
        IsLoading = false;
      }
      
    }

    private async Task DoLoadItems()
    {
      NewsItems = (await Api.GetItems()).ToObservableCollection();
      newsItems.FirstOrDefault().Highlighted = true; //TODO
    }

    #region Commands

    public Command C_ItemTapped { get; private set; }
    public Command C_Refresh { get; private set; }

    protected override void CreateCommands()
    {
      C_ItemTapped = new Command(DoItemTapped);
      C_Refresh = new Command(DoRefresh);
    }

    private async void DoRefresh(object obj)
    {
      try
      {
        IsRefreshing = true;
        await DoLoadItems();
      }
      finally
      {
        IsRefreshing = false;
      }

    }

    private void DoItemTapped(object obj)
    {
      Navigate<TestViewModel>();
    }

    #endregion
  }
}