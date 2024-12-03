using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.InteropServices;


namespace OU3.Models
{
    public class MovieMethods
    {
        public MovieMethods() { }

        public int InsertMovie(Movie Movie, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";
            string sqlstring = "INSERT INTO Filmer (Title, Director, Year, CopiesAvailable) VALUES (@Title, @Director, @Year, @CopiesAvailable)";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            sqlCommand.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 100).Value = Movie.Title;
            sqlCommand.Parameters.Add("@Director", System.Data.SqlDbType.NVarChar, 100).Value = Movie.Director;
            sqlCommand.Parameters.Add("@Year", System.Data.SqlDbType.Int).Value = Movie.Year;
            sqlCommand.Parameters.Add("@CopiesAvailable", System.Data.SqlDbType.Int).Value = Movie.CopiesAvailable;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Insert command failed";
                }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Movie> GetMovieList(out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            String sqlstring = "Select * From Filmer";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();

            List<Movie> movieList = new List<Movie>();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Filmer");

                int i = 0;
                int count = 0;

                count = dataSet.Tables["Filmer"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        Movie movie = new Movie();
                        movie.FilmID = Convert.ToInt16(dataSet.Tables["Filmer"].Rows[i]["FilmID"]);
                        movie.Title = dataSet.Tables["Filmer"].Rows[i]["Title"].ToString();
                        movie.Director = dataSet.Tables["Filmer"].Rows[i]["Director"].ToString();
                        movie.Year = Convert.ToInt16(dataSet.Tables["Filmer"].Rows[i]["Year"]);
                        movie.CopiesAvailable = Convert.ToInt16(dataSet.Tables["Filmer"].Rows[i]["CopiesAvailable"]);

                        i++;
                        movieList.Add(movie);
                    }
                    errormsg = "";
                    return movieList;
                }
                else
                {
                    errormsg = "No Movies, command failed";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}


