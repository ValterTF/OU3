using Microsoft.Data.SqlClient;
using System.Data;


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

        public Movie GetMovieDetails(int FilmID, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            String sqlstring = "Select * From Filmer Where FilmID = @FilmID";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            sqlCommand.Parameters.Add("FilmID", SqlDbType.Int).Value = FilmID;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Filmer");

                if (dataSet.Tables["Filmer"].Rows.Count > 0)
                {
                    DataRow row = dataSet.Tables["Filmer"].Rows[0];

                    Movie movie = new Movie
                    {
                        FilmID = Convert.ToInt16(row["FilmID"]),
                        Title = row["Title"].ToString(),
                        Director = row["Director"].ToString(),
                        Year = Convert.ToInt16(row["Year"]),
                        CopiesAvailable = Convert.ToInt16(row["CopiesAvailable"])
                    };

                    errormsg = "";
                    return movie;
                }
                else
                {
                    errormsg = "Movie not found";
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

        public int UpdateMovie(Movie movie, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            string sqlstring = "UPDATE Filmer SET Title = @Title, Director = @Director, Year = @Year, CopiesAvailable = @CopiesAvailable WHERE FilmID = @FilmID";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = movie.Title;
            sqlCommand.Parameters.Add("@Director", SqlDbType.NVarChar, 100).Value = movie.Director;
            sqlCommand.Parameters.Add("@Year", SqlDbType.Int).Value = movie.Year;
            sqlCommand.Parameters.Add("@CopiesAvailable", SqlDbType.Int).Value = movie.CopiesAvailable;
            sqlCommand.Parameters.Add("@FilmID", SqlDbType.Int).Value = movie.FilmID;

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Update failed, no rows affected.";
                }
                return rowsAffected;
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

        public int DeleteMovie(int FilmID, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            string sqlstring = "DELETE FROM Filmer WHERE FilmID = @FilmID";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            sqlCommand.Parameters.Add("@FilmID", SqlDbType.Int).Value = FilmID;

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {

                    string getLastIDSQL = "SELECT ISNULL(MAX(FilmID), 0) AS LastID FROM Filmer";
                    SqlCommand getLastIDCommand = new SqlCommand(getLastIDSQL, sqlConnection);
                    int lastExistingID = (int)getLastIDCommand.ExecuteScalar();

                    string resetIdentitySQL = "DBCC CHECKIDENT ('Filmer', RESEED, @LastID)";
                    SqlCommand resetCommand = new SqlCommand(resetIdentitySQL, sqlConnection);
                    resetCommand.Parameters.Add("@LastID", SqlDbType.Int).Value = lastExistingID;
                    resetCommand.ExecuteNonQuery();

                    errormsg = ""; 
                }
                else
                {
                    errormsg = "Delete failed";
                }

                return rowsAffected;
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


    }
}


