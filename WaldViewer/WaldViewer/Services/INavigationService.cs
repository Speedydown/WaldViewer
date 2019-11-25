using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.ViewModels;

namespace WaldViewer.Services
{
  public interface INavigationService
  {
    Task Navigate<TViewModel>() where TViewModel : BaseViewModel;
    Task Navigate<TViewModel, TParameter>(TParameter param) where TViewModel : BaseViewModel;
    Task GoBack();
  }
}
