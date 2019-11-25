using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WaldViewer.BL;
using WaldViewer.Common;
using WaldViewer.SAL;
using WaldViewer.Views;
using Xamarin.Forms;

namespace WaldViewer.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    private ObservableCollection<NewsItem> newsItems;

    public ObservableCollection<NewsItem> NewsItems
    {
      get { return newsItems; }
      private set { newsItems = value;
        OnPropertyChanged();
      }
    }


    public MainViewModel()
    {

    }

    protected override async Task Load()
    {
      var result = await Authentication.Authenticate();

      if (!result)
      {
        //TODO do something
      }

      NewsItems =  (await Api.GetItems()).ToObservableCollection();
    }
  }
}