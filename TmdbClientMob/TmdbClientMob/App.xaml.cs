using TmdbClientMob.ViewModels;
using TmdbClientMob.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sing = TmdbClientMob.Singleton.Singleton;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TmdbClientMob
{
    public partial class App : Application
    {
        #region Functions
        public App()
        {
            InitializeComponent();
            //First page is SplashPage.
            MainPage = new SplashPage();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            await Sing.InitAsync();
            var upcomingMovieViewModel = new UpcomingMovieViewModel(Sing.UpcomingMovieService);
            var upcomingMoviePage = new UpcomingMoviePage
            {
                BindingContext = upcomingMovieViewModel
            };

            //After all singleton data is loaded, load main page.
            MainPage = new NavigationPage(upcomingMoviePage);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
