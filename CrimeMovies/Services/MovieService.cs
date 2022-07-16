using CrimeMovies.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Xml.Linq;

namespace CrimeMovies.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieApiClient _movieApiClient;

        public MovieService(IMovieApiClient movieApiClient)
        {
            _movieApiClient = movieApiClient;
        }
        public async Task<List<Movie>> GetMoviesByGenre(string genre, int amount)
        {
            var amountOfRecordsPerRequest = 100;

            if (amount < 100)
            {
                amount = 100;
            }

            var amountOfRequestNeeded = amount / amountOfRecordsPerRequest;

            var TaskList = new List<Task<string>>();

            for (int i = 0; i < amountOfRequestNeeded; i++)
            {
                TaskList.Add(_movieApiClient.GetMoviesByGenre(genre, i*amountOfRecordsPerRequest));
            }
            return MapXmlResponsesToMovieList(await Task.WhenAll(TaskList));
        }


        public void GroupAndSortMovies(List<Movie> movies)
        {

            var results = movies.GroupBy(movie => movie.Year)
                .Select(group => new
                {
                    Year = group.Key,
                    Movies = group.OrderByDescending(movie => movie.Rating)
                });

            foreach (var group in results.OrderByDescending(x => x.Year))
            {
                Console.WriteLine(group.Year);
                foreach (var movie in group.Movies.Take(10))
                {

                    Console.WriteLine(movie.Title + ":" + movie.Rating);
                }
            }
        }

        private List<Movie> MapXmlResponsesToMovieList(string[] movieResponses)
        {
            var MovieList = new List<Movie>();
            foreach (var movieResponse in movieResponses)
            {
                XDocument moviesResponse = XDocument.Parse(movieResponse);
                XNamespace ns = "http://schemas.datacontract.org/2004/07/Rubicon.IntakeOpdracht.CosmosDb.Function.Models";
                List<Movie> movies = (
                from mov in moviesResponse.Descendants(ns + "Movie")
                select MapXmlMovieToMovie(mov, ns)).ToList();

                MovieList.AddRange(movies);
            }

            return MovieList;
        }

        private Movie MapXmlMovieToMovie(XElement xmlMovie, XNamespace xNamespace)
        {
            return new Movie
            {
                Id = xmlMovie.Element(xNamespace + "imdbid").Value,
                Title = xmlMovie.Element(xNamespace + "title").Value,
                Year = xmlMovie.Element(xNamespace + "year").Value,
                Rating = xmlMovie.Element(xNamespace + "rating").Value,
            };
        }
    }
}
