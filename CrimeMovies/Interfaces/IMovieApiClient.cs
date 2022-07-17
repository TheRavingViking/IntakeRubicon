namespace CrimeMovies.Interfaces
{
    public interface IMovieApiClient
    {
        /// <summary>
        /// Retrieves movies from the rubicon api based on genre and amount given.
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="index"></param>
        /// <returns>returns a string of XML or JSON based on AcceptHeader</returns>
        Task<string> GetMoviesByGenre(string genre, int index);
    }
}
