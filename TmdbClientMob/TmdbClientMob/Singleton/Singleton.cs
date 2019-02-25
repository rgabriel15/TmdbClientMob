using TmdbClientMob.Models;
using TmdbClientMob.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TmdbClientMob.Singleton
{
    /// <summary>
    /// Class to declare singleton objects.
    /// </summary>
    internal static class Singleton
    {
        #region Constants
        internal static readonly GenreService GenreService = null;
        internal static readonly SearchMovieService SearchMovieService = null;
        internal static readonly UpcomingMovieService UpcomingMovieService = null;
        #endregion

        #region Properties
        internal static IEnumerable<GenreModel> GenreCollection { get; private set; } = null;
        #endregion

        #region Functions
        /// <summary>
        /// Constructor.
        /// </summary>
        static Singleton()
        {
            GenreService = new GenreService();
            SearchMovieService = new SearchMovieService();
            UpcomingMovieService = new UpcomingMovieService();
        }

        /// <summary>
        /// Function to initialize all sigleton data from async functions.
        /// </summary>
        /// <returns></returns>
        internal static async Task InitAsync()
        {
            var genreCollection = await GenreService.GetAsync();
            GenreCollection = genreCollection?.GenreCollection;
        }
        #endregion
    }
}
