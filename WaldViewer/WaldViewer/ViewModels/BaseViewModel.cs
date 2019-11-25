using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WaldViewer.ViewModels
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    private Task _loadTask;

    public Task LoadTask
    {
      get { return _loadTask; }
      private set
      {
        _loadTask = value;
        OnPropertyChanged();
      }
    }


    protected BaseViewModel()
    {
      LoadTask = Task.Run(() => Load() );
    }

    protected virtual async Task Load()
    {

    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      var changed = PropertyChanged;
      if (changed == null)
        return;

      changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Navigation

    public void Navigate<TViewModelType>() where TViewModelType : BaseViewModel
    {
      App.NavigationService.Navigate<TViewModelType>();
    }

    #endregion
  }
}
