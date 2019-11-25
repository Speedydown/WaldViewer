using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.Exceptions
{
  public class CouldNotFindViewException : Exception
  {
    public CouldNotFindViewException(Type ViewModelType) : base($"Could not find view for viewmodel with type { ViewModelType.FullName}")
    {
     
    }
  }
}
