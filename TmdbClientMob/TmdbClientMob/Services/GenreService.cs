using TmdbClientMob.Models;
using System.Threading.Tasks;

namespace TmdbClientMob.Services
{
    internal sealed class GenreService : BaseService<GenreCollectionModel>
    {
        #region Constants
        private const string ServicePath = "genre/movie/list";
        #endregion

        #region Functions
        internal async Task<GenreCollectionModel> GetAsync()
        {
            var model = await BaseGetAsync(ServicePath);
            return model;
        }
        #endregion
    }
}

