namespace CrimeMovies.Interfaces
{
    public interface ITableService
    {
        /// <summary>
        /// Creates a DataTable based on the properties of Movie object to be displayed in the console log
        /// </summary>
        /// <param name="dataTableName"></param>
        /// <param name="movies"></param>
        void ShowTable(string dataTableName, List<Movie> movies);
    }
}
