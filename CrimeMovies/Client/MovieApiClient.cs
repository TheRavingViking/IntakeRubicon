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

        public async Task<ArrayOfMovie> GetMoviesByGenre()
        {

            var response = await _httpClient.GetAsync("api/movies/genres/crime?index=1000");
            var httpResponse = response.EnsureSuccessStatusCode();

            var xml = await httpResponse.Content.ReadAsStringAsync();
            XDocument moviesResponse = XDocument.Parse(xml);
            XNamespace ns = "http://schemas.datacontract.org/2004/07/Rubicon.IntakeOpdracht.CosmosDb.Function.Models";

            var xmlresponse = moviesResponse.Descendants(ns + "Movie").Where(item => (string)item.Element(ns + "imdbid") == "tt0086356");


            return xmlresponse;
            
        }
    }
}
