using Polly.Retry;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CrimeMovies.Client
{
    public class MovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;
        private AsyncRetryPolicy retryPolicy;
        private int maxRetries = 6;
        public MovieApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            retryPolicy = Policy.Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
                onRetry: (exception, sleepDuration, attemptNumber, context) =>
                {
                    Console.WriteLine($"Too many requests. Retrying in {sleepDuration}. {attemptNumber} / {maxRetries}");
                });
                  
        }

        public async Task<string> GetMoviesByGenre(string genre, int index)
        {
            return await retryPolicy.ExecuteAsync(async () =>
            {
                Console.WriteLine($"requesting for {genre} and index {index}");
                var response = await _httpClient.GetAsync($"api/movies/genres/{genre}?index={index}");
                var httpResponse = response.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadAsStringAsync();
            });
        }
    }
}
