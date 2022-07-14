namespace CrimeMovies.Client
{
    public interface IMovieApiClient
    {
        Task<ArrayOfMovie> GetMoviesByGenre();
    }
}
