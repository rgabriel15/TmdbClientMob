using TmdbClientMob.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TmdbClientMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
            BindingContext = new SplashViewModel();
			InitializeComponent();
		}
	}
}