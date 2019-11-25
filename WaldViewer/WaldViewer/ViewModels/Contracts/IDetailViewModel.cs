using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.ViewModels.Contracts
{
 public interface IDetailViewModel<TParameter>
  {
    void Prepare(TParameter parameter);
  }
}
