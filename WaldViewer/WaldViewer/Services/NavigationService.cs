using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.Exceptions;
using WaldViewer.ViewModels;
using WaldViewer.ViewModels.Contracts;
using WaldViewer.Views;
using Xamarin.Forms;

namespace WaldViewer.Services
{
  public class NavigationService : INavigationService
  {
    private Dictionary<Type, Type> ViewsContainer { get; set; }

    public NavigationService()
    {
      var views = GetType().Assembly.DefinedTypes.Where(i => !i.IsAbstract && i.IsSubclassOf(typeof(Page))).ToArray();
      ViewsContainer = GetType().Assembly.DefinedTypes.Where(i => !i.IsAbstract && i.IsSubclassOf(typeof(BaseViewModel))).Select(i =>
      {
        var viewType = views.FirstOrDefault(view => GetViewModelTypeTypeFromVieww(view) == i);

        if (viewType == null)
        {
          return null;
        }

        return new Tuple<Type, Type>(i.AsType(), viewType.AsType());
      }).Where(i => i != null).ToDictionary(tuple =>  tuple.Item1 , tuple =>  tuple.Item2 );
    }

    private Type GetViewModelTypeTypeFromVieww(Type viewType)
    {
      var type = viewType.GetGenericArguments().FirstOrDefault(i => i.IsSubclassOf(typeof(BaseViewModel)));

      if (type != null)
      {
        return type;
      }

      if (viewType.BaseType != null)
      {
        return GetViewModelTypeTypeFromVieww(viewType.BaseType);
      }

      return null;
    }

    private INavigation FormsService => App.Current.MainPage.Navigation;

    public async Task Navigate<TViewModel>() where TViewModel : BaseViewModel
    {
      if (!ViewsContainer.ContainsKey(typeof(TViewModel)))
      {
        throw new CouldNotFindViewException(typeof(TViewModel));
      }

      var mapping = ViewsContainer.FirstOrDefault(i => i.Key == typeof(TViewModel));


      await FormsService.PushModalAsync(Activator.CreateInstance(mapping.Value) as Page);
    }

    public async Task Navigate<TViewModel, TParameter>(TParameter param) where TViewModel : BaseViewModel
    {
      if (!ViewsContainer.ContainsKey(typeof(TViewModel)))
      {
        throw new CouldNotFindViewException(typeof(TViewModel));
      }

      var mapping = ViewsContainer.FirstOrDefault(i => i.Key == typeof(TViewModel));

      var basePage = Activator.CreateInstance(mapping.Value) as IBaseContentPage;
      await FormsService.PushModalAsync(basePage as Page);

      if (basePage.ViewModel is IDetailViewModel<TParameter> dvm)
      {
        dvm.Prepare(param);
      }
    }

    public async Task GoBack()
    {
      if (FormsService.NavigationStack.Any())
      {
        await FormsService.PopModalAsync();
      }
    }
  }
}
