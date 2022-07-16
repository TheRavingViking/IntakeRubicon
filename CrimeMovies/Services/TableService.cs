using CrimeMovies.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeMovies.Services
{
    /// <summary>
    /// TableService, used to creates a datatable to be logged to the console window
    /// </summary>
    internal class TableService : ITableService
    {
        public void ShowTable(string dataTableName, List<Movie> movies)
        {

            DataTable moviesTable = CreateMoviesTable(dataTableName);
            InsertMovieData(moviesTable, movies);

            foreach (DataColumn col in moviesTable.Columns)
            {
                Console.Write("{0,-30}", col.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in moviesTable.Rows)
            {
                foreach (DataColumn col in moviesTable.Columns)
                {

                    Console.Write("{0,-30}", row[col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static DataTable CreateMoviesTable(string dataTableName)
        {
            DataTable movieTable = new DataTable(dataTableName);

            DataColumn[] cols =
                {
            new DataColumn(nameof(Movie.Id),typeof(string)),
            new DataColumn(nameof(Movie.Year),typeof(string)),
            new DataColumn(nameof(Movie.Title),typeof(string)),
            new DataColumn(nameof(Movie.Rating),typeof(string)),
        };

            movieTable.Columns.AddRange(cols);
            return movieTable;
        }
        private static void InsertMovieData(DataTable movieDataTable, List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                var dataRow = movieDataTable.NewRow();
                dataRow[nameof(Movie.Id)] = movie.Id;
                dataRow[nameof(Movie.Year)] = movie.Year;
                dataRow[nameof(Movie.Title)] = movie.Title;
                dataRow[nameof(Movie.Rating)] = movie.Rating;
                movieDataTable.Rows.Add(dataRow);
            }
        }
        
    }
}
