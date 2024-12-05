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
    }   
}
