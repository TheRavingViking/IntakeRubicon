namespace CrimeMovies.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieApiClient _movieApiClient;

        public MovieService(IMovieApiClient movieApiClient)
        {
            _movieApiClient = movieApiClient;
        }

        /// <inheritdoc/>
        public async Task<List<Movie>> GetMoviesByGenre(string genre, int amount)
        {
            var amountOfRecordsPerRequest = 100;

            var amountOfRequestNeeded = amount / amountOfRecordsPerRequest;

            if (amountOfRequestNeeded == 0)
            {
                amountOfRequestNeeded = 1;
            }

            var TaskList = new List<Task<string>>();

            for (int i = 0; i < amountOfRequestNeeded; i++)
            {
                TaskList.Add(_movieApiClient.GetMoviesByGenre(genre, i*amountOfRecordsPerRequest));
            }
            // replace this with an different method to support Json
            return MapXmlResponsesToMovieList(await Task.WhenAll(TaskList));
        }

        /// <inheritdoc/>
        public IEnumerable<(string year, IOrderedEnumerable<Movie> movies)> GroupAndOrderMovies(List<Movie> movies)
        {
            return movies
                .GroupBy(movie => movie.Year)
                .Select(group => (
                    Year: group.Key,
                    Movies: group.OrderByDescending(movie => movie.Rating)
                ));
        }

        /// <summary>
        /// Maps the returned XML string from the Api to a list Movies
        /// </summary>
        /// <param name="movieResponses"></param>
        /// <returns>List of Movies</returns>
        private List<Movie> MapXmlResponsesToMovieList(string[] movieResponses)
        {
            var MovieList = new List<Movie>();
            foreach (var movieResponse in movieResponses)
            {
                XDocument moviesResponse = XDocument.Parse(movieResponse);
                XNamespace ns = "http://schemas.datacontract.org/2004/07/Rubicon.IntakeOpdracht.CosmosDb.Function.Models";
                List<Movie> movies = moviesResponse.Descendants(ns + "Movie").Select(movie => MapXmlMovieToMovie(movie, ns)).ToList();
                MovieList.AddRange(movies);
            }
            return MovieList;
        }

        /// <summary>
        /// Maps an XElement from XML to Linq query to Movie Object
        /// </summary>
        /// <param name="xmlMovie"></param>
        /// <param name="xNamespace"></param>
        /// <returns>movie</returns>
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
