using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaldViewer.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TestPage : BaseContentPage<TestViewModel>
  {
    public TestPage()
    {
      InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
       var result = base.OnBackButtonPressed();

     // Navigation.PopAsync();

      return true;
    }
  }
}