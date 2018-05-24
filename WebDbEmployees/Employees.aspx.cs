using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDbEmployees
{
    public partial class Employees : System.Web.UI.Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_Employees.mdf;Integrated Security=True";
        SqlConnection mySqlCon = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGridView();
            }
        }


        void FillGridView()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection mySqlCon = new SqlConnection(connectionString))
            {
                mySqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM [Employees] ORDER BY [L_NAME]", mySqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvEmployees.DataSource = dtbl;
                gvEmployees.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvEmployees.DataSource = dtbl;
                gvEmployees.DataBind();
                gvEmployees.Rows[0].Cells.Clear();
                gvEmployees.Rows[0].Cells.Add(new TableCell());
                gvEmployees.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvEmployees.Rows[0].Cells[0].Text = "Нет данных ..!";
                gvEmployees.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection mySqlCon = new SqlConnection(connectionString))
                    {
                        mySqlCon.Open();
                        string queryInsert = @"INSERT INTO [Employees] (F_NAME, L_NAME, FIX_PAYMENT, COUNT_DAYS, COUNT_HOUR, RATE) 
                    VALUES(@F_NAME, @L_NAME, @FIX_PAYMENT, @COUNT_DAYS, @COUNT_HOUR, @RATE)"; // запрос в БД
                        SqlCommand sqlCmd = new SqlCommand(queryInsert, mySqlCon);
                        sqlCmd.Parameters.AddWithValue("@l_name", (gvEmployees.FooterRow.FindControl("txtLNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@f_name", (gvEmployees.FooterRow.FindControl("txtFNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@fix_payment", (gvEmployees.FooterRow.FindControl("txtFixPaymentFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@count_days", (gvEmployees.FooterRow.FindControl("txtCountDaysFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@count_hour", (gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@rate", (gvEmployees.FooterRow.FindControl("txtRateFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        FillGridView();
                        lblSuccessMessage.Text = "Новая запись добавлена";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }

        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            FillGridView();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            FillGridView();
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection mySqlCon = new SqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    string queryUpdate = @"UPDATE [Employees] SET 
                                            F_NAME=@F_NAME,
                                            L_NAME=@L_NAME,
                                            FIX_PAYMENT=@FIX_PAYMENT,
                                            COUNT_DAYS=@COUNT_DAYS,
                                            COUNT_HOUR=@COUNT_HOUR,
                                            RATE= @RATE
                                            WHERE ID=@id";
                    SqlCommand sqlCmd = new SqlCommand(queryUpdate, mySqlCon);
                    sqlCmd.Parameters.AddWithValue("@l_name", (gvEmployees.Rows[e.RowIndex].FindControl("txtLName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@f_name", (gvEmployees.Rows[e.RowIndex].FindControl("txtFName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@fix_payment", (gvEmployees.Rows[e.RowIndex].FindControl("txtFixPayment") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@count_days", (gvEmployees.Rows[e.RowIndex].FindControl("txtCountDays") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@count_hour", (gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@rate", (gvEmployees.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvEmployees.EditIndex = -1;
                    FillGridView();
                    lblSuccessMessage.Text = "Выбранная запись изменена";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection mySqlCon = new SqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    string queryDelete = @"DELETE FROM [Employees] WHERE [ID]=@id";
                    SqlCommand sqlCmd = new SqlCommand(queryDelete, mySqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    FillGridView();
                    lblSuccessMessage.Text = "Выбранная запись удалена";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

    }
}