using Microsoft.Data.SqlClient;
using System.Data;

namespace OU3.Models
{
    public class MovieLoanMethods
    {
        public List<MovieLoan> GetMoviesAndLoans(out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            String sqlstring = "SELECT f.FilmID, f.Title, f.Director, f.Year, l.BorrowerName, l.LoanDate, l.ReturnDate FROM Filmer f LEFT JOIN Utlån l ON f.FilmID = l.FilmID";

            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            List<MovieLoan> movieLoanList = new List<MovieLoan>();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "MovieLoan");

                foreach (DataRow row in dataSet.Tables["MovieLoan"].Rows)
                {
                    MovieLoan movieLoan = new MovieLoan
                    {
                        FilmID = Convert.ToInt32(row["FilmID"]),
                        Title = row["Title"].ToString(),
                        Director = row["Director"].ToString(),
                        Year = Convert.ToInt32(row["Year"]),
                        BorrowerName = row["BorrowerName"] != DBNull.Value ? row["BorrowerName"].ToString() : null,
                        LoanDate = row["LoanDate"] != DBNull.Value ? Convert.ToDateTime(row["LoanDate"]) : default,
                        ReturnDate = row["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(row["ReturnDate"]) : (DateTime?)null
                    };
                    movieLoanList.Add(movieLoan);
                }

                errormsg = "";
                return movieLoanList;
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
