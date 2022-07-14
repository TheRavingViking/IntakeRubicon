using System.Xml.Serialization;

namespace CrimeMovies.Models;

[XmlRoot(ElementName = "Movie")]
public class Movie
{

	[XmlElement(ElementName = "budget")]
	public int Budget { get; set; }

	[XmlElement(ElementName = "country")]
	public string Country { get; set; }

	[XmlElement(ElementName = "genres")]
	public string Genres { get; set; }

	[XmlElement(ElementName = "homepage")]
	public string Homepage { get; set; }

	[XmlElement(ElementName = "imdbid")]
	public string Imdbid { get; set; }

	[XmlElement(ElementName = "imdburl")]
	public string Imdburl { get; set; }

	[XmlElement(ElementName = "language")]
	public string Language { get; set; }

	[XmlElement(ElementName = "originaltitle")]
	public string Originaltitle { get; set; }

	[XmlElement(ElementName = "overview")]
	public string Overview { get; set; }

	[XmlElement(ElementName = "productioncompany")]
	public string Productioncompany { get; set; }

	[XmlElement(ElementName = "rating")]
	public string Rating { get; set; }

	[XmlElement(ElementName = "recordtype")]
	public string Recordtype { get; set; }

	[XmlElement(ElementName = "revenue")]
	public string Revenue { get; set; }

	[XmlElement(ElementName = "runtime")]
	public string Runtime { get; set; }

	[XmlElement(ElementName = "status")]
	public string Status { get; set; }

	[XmlElement(ElementName = "tagline")]
	public string Tagline { get; set; }

	[XmlElement(ElementName = "title")]
	public string Title { get; set; }

	[XmlElement(ElementName = "year")]
	public string Year { get; set; }
}
