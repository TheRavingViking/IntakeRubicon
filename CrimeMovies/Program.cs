global using CrimeMovies.Client;
global using CrimeMovies.Models;
global using CrimeMovies.Services;
global using Microsoft.Extensions.DependencyInjection;
global using Polly;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

var builder = new ServiceCollection();
builder.AddHttpClient<IMovieApiClient, MovieApiClient>(client =>
{
    client.BaseAddress = new Uri("https://intakeopdracht-apim-euw-p.azure-api.net");
    client.DefaultRequestHeaders.Add("Authorisation", "wsdmNQwjcc9pJtrZi5wS%2Bm%2BwB4hNJuRXAaYSSpk33zc%3D");
    client.DefaultRequestHeaders.Add("Accept", "application/xml");
});

builder.AddSingleton<IMovieService, MovieService>();

var serviceProvider = builder.BuildServiceProvider();

var movieService = serviceProvider.GetRequiredService<IMovieService>();
var results = await movieService.GetMoviesByGenre("crime", 4000);

movieService.GroupAndSortMovies(results);
Console.ReadKey();
