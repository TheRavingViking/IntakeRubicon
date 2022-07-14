
using System.Xml.Serialization;

namespace CrimeMovies.Models;

[XmlRoot(ElementName = "ArrayOfMovie")]
public class ArrayOfMovie
{

	[XmlElement(ElementName = "Movie")]
	public List<Movie> Movie { get; set; }

	[XmlAttribute(AttributeName = "xmlns")]
	public string Xmlns { get; set; }

	[XmlAttribute(AttributeName = "i")]
	public string I { get; set; }

	[XmlText]
	public string Text { get; set; }
}

