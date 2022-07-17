namespace CrimeMovies.Clients
{
    public class MovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly int maxRetries = 6;
        private AsyncRetryPolicy retryPolicy;
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

        /// <inheritdoc/>
        public async Task<string> GetMoviesByGenre(string genre, int index)
        {
            return await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await _httpClient.GetAsync($"api/movies/genres/{genre}?index={index}");
                var httpResponse = response.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadAsStringAsync();
            });
        }
    }
}
