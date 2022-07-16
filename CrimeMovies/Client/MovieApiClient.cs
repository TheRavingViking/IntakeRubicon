using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CrimeMovies.Client
{
    public class MovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;

        public MovieApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetMoviesByGenre(string genre, int index)
        {

            var response = await _httpClient.GetAsync($"api/movies/genres/{genre}?index={index}");
            var httpResponse = response.EnsureSuccessStatusCode();

            //var xml = await httpResponse.Content.ReadAsStringAsync();
            //XDocument moviesResponse = XDocument.Parse(xml);
            //XNamespace ns = "http://schemas.datacontract.org/2004/07/Rubicon.IntakeOpdracht.CosmosDb.Function.Models";

            //var xmlresponse = moviesResponse.Descendants(ns + "Movie").Where(item => (string)item.Element(ns + "imdbid") == "tt0086356");

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
