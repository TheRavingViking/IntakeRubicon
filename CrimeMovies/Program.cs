
// configure dependencies 
var serviceProvider = ConfigureServices();

// Get Services
var movieService = serviceProvider.GetRequiredService<IMovieService>();
var tableService = serviceProvider.GetRequiredService<ITableService>();

var results = await movieService.GetMoviesByGenre("crime", 2500);
var groupedResults = movieService.GroupAndOrderMovies(results);

foreach (var group in groupedResults.OrderByDescending(x => x.year))
{
    tableService.ShowTable(group.year, group.movies.Take(10).ToList());
}
Console.WriteLine("Retrieved, Grouped and Ordered Records");
Console.ReadKey();


 static ServiceProvider ConfigureServices()
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false);

    IConfiguration config = builder.Build();

    var services = new ServiceCollection();
    services.AddHttpClient<IMovieApiClient, MovieApiClient>(client =>
    {
        client.BaseAddress = new Uri(config["BaseUrl"]);
        client.DefaultRequestHeaders.Add("Authorisation", config["AutorisationHeader"]);
        client.DefaultRequestHeaders.Add("Accept", "application/xml");
    });

    services.AddSingleton<IMovieService, MovieService>();
    services.AddSingleton<ITableService, TableService>();

    return services.BuildServiceProvider();
}