namespace CrimeMovies.Client
{
    public interface IMovieApiClient
    {
        Task<string> GetMoviesByGenre(string genre, int index);
    }
}
