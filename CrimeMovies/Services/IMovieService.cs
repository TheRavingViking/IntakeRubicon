using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeMovies.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesByGenre(string genre, int amount);
        void GroupAndSortMovies(List<Movie> movies);
        //List<Movie> MapResponsesToMovieList(string[] movieResponses);
    }
}
