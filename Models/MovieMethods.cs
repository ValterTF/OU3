using Microsoft.Data.SqlClient;
using System.Linq.Expressions;


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
    }
}


