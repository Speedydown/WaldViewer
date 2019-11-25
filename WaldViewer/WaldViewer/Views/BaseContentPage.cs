using System;
using System.Collections.Generic;
using System.Text;
using WaldViewer.ViewModels;
using Xamarin.Forms;

namespace WaldViewer.Views
{
  public interface IBaseContentPage
  {
    BaseViewModel ViewModel { get; }
  }

  public abstract class BaseContentPage<TViewModel> : ContentPage, IBaseContentPage where TViewModel : BaseViewModel, new()
  {

    public TViewModel ViewModel { get; protected set; }

    BaseViewModel IBaseContentPage.ViewModel => ViewModel;

    protected BaseContentPage()
    {
      ViewModel = new TViewModel();


     
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      BindingContext = ViewModel;
    }

    
  }
}
