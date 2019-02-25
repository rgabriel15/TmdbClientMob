using TmdbClientMob.Models;
using TmdbClientMob.Services;
using TmdbClientMob.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sing = TmdbClientMob.Singleton.Singleton;
using Xamarin.Forms.Extended;

namespace TmdbClientMob.ViewModels
{
    /// <summary>
    /// ViewModel class to UpcomingPage.
    /// </summary>
    public sealed class UpcomingMovieViewModel : BaseViewModel
    {
        #region Variables
        private static UpcomingMovieService _upcomingMovieService = null;
        private uint totalPages = 0;
        #endregion

        #region Properties
        public InfiniteScrollCollection<MovieModel> ListView_ItemsSource { get; set; }
        public Command ListView_RefreshCommand { get; set; }
        public Command ToolbarItemSearch_Command { get; set; }

        private MovieModel listView_SelectedItem = null;
        public MovieModel ListView_SelectedItem
        {
            get { return listView_SelectedItem; }
            set
            {
                SetProperty(ref listView_SelectedItem, value);
                ListView_SelectedItem_HandlerAsync();
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="upcomingMovieService">Service object.</param>
        internal UpcomingMovieViewModel(UpcomingMovieService upcomingMovieService)
        {
            _upcomingMovieService = upcomingMovieService ?? throw new ArgumentException("upcomingMovieService");

            Title = "TMDb Upcoming Movies";
            ListView_ItemsSource = new InfiniteScrollCollection<MovieModel>
            {
                OnLoadMore = async() =>
                {
                    try
                    {
                        if (IsBusy)
                            return null;

                        IsBusy = true;
                        var page = (uint)(ListView_ItemsSource.Count / PageSize) + 1;

                        if (totalPages > 0
                            && page >= totalPages)
                        {
                            IsBusy = false;
                            return null;
                        }

                        var model = await GetMovieCollectionAsync(page);
                        totalPages = (uint)model.TotalPages;
                        IsBusy = false;
                        return model.MovieCollection;
                    }
                    catch (Exception ex)
                    {
                        IsBusy = false;
                        Debug.WriteLine(ex);
                        throw;
                    }
                }
            };

            ListView_RefreshCommand = new Command(async() => await ExecuteListView_RefreshCommand_HandlerAsync());
            ToolbarItemSearch_Command = new Command(async() => await ToolbarItemSearch_Command_HandlerAsync());
            _ExecuteListView_RefreshCommand_HandlerAsync();
        }

        /// <summary>
        /// Function to call the API service.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <returns></returns>
        private async Task<UpcomingMovieModel> GetMovieCollectionAsync(uint page = 1)
        {
            var model = await _upcomingMovieService.GetAsync(page);
            return model;
        }

        /// <summary>
        /// Function to populate the ListView.
        /// </summary>
        /// <returns>Task</returns>
        private async Task ExecuteListView_RefreshCommand_HandlerAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                ListView_ItemsSource.Clear();
                var model = await GetMovieCollectionAsync();
                ListView_ItemsSource.AddRange(model.MovieCollection);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Function to populate the ListView. Returns void to be executed on constructor.
        /// </summary>
        private async void _ExecuteListView_RefreshCommand_HandlerAsync()
        {
            await ExecuteListView_RefreshCommand_HandlerAsync();
        }

        /// <summary>
        /// ListView SelectedItem handler.
        /// </summary>
        private async void ListView_SelectedItem_HandlerAsync()
        {
            try
            {
                if (IsBusy
                    || ListView_SelectedItem == null)
                    return;

                IsBusy = true;

                var viewModel = new MovieDetailViewModel(ListView_SelectedItem);
                var page = new MovieDetailPage()
                {
                    BindingContext = viewModel
                };

                await ((NavigationPage)Application.Current.MainPage).Navigation.PushAsync(page);
                ListView_SelectedItem = null;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Search button click handler
        /// </summary>
        /// <returns></returns>
        private async Task ToolbarItemSearch_Command_HandlerAsync()
        {
            var viewModel = new SearchMovieViewModel(Sing.SearchMovieService);
            var page = new SearchMoviePage
            {
                BindingContext = viewModel
            };
            await ((NavigationPage)Application.Current.MainPage).PushAsync(page);
        }
        #endregion
    }
}