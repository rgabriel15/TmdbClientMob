using TmdbClientMob.Models;
using System;

namespace TmdbClientMob.ViewModels
{
    /// <summary>
    /// ViewModel class to MovieDatailPage.
    /// </summary>
    public sealed class MovieDetailViewModel : BaseViewModel
    {
        #region Variables
        //private static MovieService _movieService = null;
        #endregion

        #region Properties
        private MovieModel _movie = null;
        /// <summary>
        /// Movie property to populate MovieDateilPage.
        /// </summary>
        public MovieModel Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="movie">MovieModel object to populate the screen.</param>
        public MovieDetailViewModel(MovieModel movie = null/*, MovieService movieService*/)
        {
            //_movieService = movieService ?? throw new ArgumentException("movieService");
            Movie = movie ?? throw new ArgumentException("movie");
            Title = Movie.OriginalTitle;
        }
        #endregion
    }
}