using System.Xml.Serialization;

namespace CrimeMovies.Models;

public class Movie
{
    public string Id { get; set; }
    public string Title { get; set; }

    public string Year { get; set; }

    public string Rating { get; set; }
}
