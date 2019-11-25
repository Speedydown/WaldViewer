using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WaldViewer.BL;

namespace WaldViewer.Common
{
  public static class Extensions
  {
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
    {
      return new ObservableCollection<T>(items);
    }
  }
}
