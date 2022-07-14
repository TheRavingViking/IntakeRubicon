namespace CrimeMovies.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieApiClient _movieApiClient;

        public MovieService(IMovieApiClient movieApiClient)
        {
            _movieApiClient = movieApiClient;
        }
        public async Task<ArrayOfMovie> GetMoviesByGenre(string genre)
        {
            return await _movieApiClient.GetMoviesByGenre();
        }
    }
}
