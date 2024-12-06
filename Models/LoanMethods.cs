using Microsoft.Data.SqlClient;
using System.Data;

namespace OU3.Models
{
    public class LoanMethods
    {
        public LoanMethods() { }

        public int InsertLoan(Loan loan, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";
            string sqlstring = "INSERT INTO Utlån (FilmID, BorrowerName, LoanDate, ReturnDate) VALUES (@FilmID, @BorrowerName, @LoanDate, @ReturnDate)";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            sqlCommand.Parameters.Add("@FilmID", System.Data.SqlDbType.Int).Value = loan.FilmID;
            sqlCommand.Parameters.Add("@BorrowerName", System.Data.SqlDbType.NVarChar, 100).Value = loan.BorrowerName;
            sqlCommand.Parameters.Add("@LoanDate", System.Data.SqlDbType.Date).Value = loan.LoanDate;

            if (loan.ReturnDate.HasValue)
            {
                sqlCommand.Parameters.Add("@ReturnDate", System.Data.SqlDbType.Date).Value = loan.ReturnDate.Value;
            }
            else
            {
                sqlCommand.Parameters.Add("@ReturnDate", System.Data.SqlDbType.Date).Value = DBNull.Value;
            }
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
        public List<Loan> GetLoanList(out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            string sqlstring = "SELECT * FROM Utlån";
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();

            List<Loan> loanList = new List<Loan>();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Utlån");

                int i = 0;
                int count = dataSet.Tables["Utlån"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        Loan loan = new Loan();
                        loan.LoanID = Convert.ToInt32(dataSet.Tables["Utlån"].Rows[i]["LoanID"]);
                        loan.FilmID = Convert.ToInt32(dataSet.Tables["Utlån"].Rows[i]["FilmID"]);
                        loan.BorrowerName = dataSet.Tables["Utlån"].Rows[i]["BorrowerName"].ToString();
                        loan.LoanDate = Convert.ToDateTime(dataSet.Tables["Utlån"].Rows[i]["LoanDate"]);
                        loan.ReturnDate = dataSet.Tables["Utlån"].Rows[i]["ReturnDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataSet.Tables["Utlån"].Rows[i]["ReturnDate"]);

                        i++;
                        loanList.Add(loan);
                    }
                    errormsg = "";
                    return loanList;
                }
                else
                {
                    errormsg = "No Loans, command failed";
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

        public Loan GetLoanDetails(int loanID, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";

            string sqlString = "SELECT * FROM Utlån WHERE LoanID = @LoanID";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);

            sqlCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanID;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Utlån");

                if (dataSet.Tables["Utlån"].Rows.Count > 0)
                {
                    DataRow row = dataSet.Tables["Utlån"].Rows[0];
                    Loan loan = new Loan
                    {
                        LoanID = Convert.ToInt16(row["LoanID"]),
                        FilmID = Convert.ToInt16(row["FilmID"]),
                        BorrowerName = row["BorrowerName"].ToString(),
                        LoanDate = Convert.ToDateTime(row["LoanDate"]),
                        ReturnDate = row["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(row["ReturnDate"]) : (DateTime?)null
                    };
                    errormsg = "";
                    return loan;
                }
                else
                {
                    errormsg = "Loan not found.";
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

        public int UpdateLoan(Loan loan, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";
            string sqlString = "UPDATE Utlån SET BorrowerName = @BorrowerName, LoanDate = @LoanDate, ReturnDate = @ReturnDate WHERE LoanID = @LoanID";

            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loan.LoanID;
            sqlCommand.Parameters.Add("@BorrowerName", SqlDbType.NVarChar, 100).Value = loan.BorrowerName;
            sqlCommand.Parameters.Add("@LoanDate", SqlDbType.DateTime).Value = loan.LoanDate;
            sqlCommand.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = (object)loan.ReturnDate ?? DBNull.Value;

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                errormsg = rowsAffected > 0 ? "" : "Update failed.";
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

        public int DeleteLoan(int loanID, out string errormsg)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OU2;Integrated Security=True;Encrypt=True";
            string sqlString = "DELETE FROM Utlån WHERE LoanID = @LoanID";

            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanID;

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                errormsg = rowsAffected > 0 ? "" : "Delete failed.";
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
