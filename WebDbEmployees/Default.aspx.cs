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
    public enum FixPayment // перечисление, добавлено для возможности вносить в базу 'T' or 'F'
    {
        F, // ставка почасовая
        T  // ставка фиксированная
    }

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
            {
                try
                {
                    if (e.CommandName.Equals("AddNew"))
                    {
                        // Проверка заполненности текстовых полей
                        if (!string.IsNullOrEmpty((gvEmployees.FooterRow.FindControl("txtLNameFooter") as TextBox).Text)
                            && !string.IsNullOrWhiteSpace((gvEmployees.FooterRow.FindControl("txtLNameFooter") as TextBox).Text) &&
                            !string.IsNullOrEmpty((gvEmployees.FooterRow.FindControl("txtFNameFooter") as TextBox).Text)
                            && !string.IsNullOrWhiteSpace((gvEmployees.FooterRow.FindControl("txtFNameFooter") as TextBox).Text) &&
                            !string.IsNullOrEmpty((gvEmployees.FooterRow.FindControl("txtCountDaysFooter") as TextBox).Text)
                            && !string.IsNullOrWhiteSpace((gvEmployees.FooterRow.FindControl("txtCountDaysFooter") as TextBox).Text) &&
                            !string.IsNullOrEmpty((gvEmployees.FooterRow.FindControl("txtRateFooter") as TextBox).Text)
                            && !string.IsNullOrWhiteSpace((gvEmployees.FooterRow.FindControl("txtRateFooter") as TextBox).Text) &&
                            ((!string.IsNullOrEmpty((gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Text)
                            && !string.IsNullOrWhiteSpace((gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Text) &&
                            (gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Visible) ||
                            !(gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Visible))
                        {
                            if (!(gvEmployees.FooterRow.FindControl("chkFixPaymentFooter") as CheckBox).Checked
                                && (gvEmployees.FooterRow.FindControl("txtRateFooter") as TextBox).Text == "0")
                            {
                                string script = "<script type=\"text/javascript\">alert('Ставка не может быть 0!');</script>";
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                            }
                            else
                            {
                                using (SqlConnection mySqlCon = new SqlConnection(connectionString))
                                {
                                    mySqlCon.Open();
                                    string queryInsert = @"INSERT INTO [Employees] (F_NAME, L_NAME, FIX_PAYMENT, COUNT_DAYS, COUNT_HOUR, RATE) 
                        VALUES(@F_NAME, @L_NAME, @FIX_PAYMENT, @COUNT_DAYS, @COUNT_HOUR, @RATE)"; // запрос в БД
                                    SqlCommand sqlCmd = new SqlCommand(queryInsert, mySqlCon);
                                    sqlCmd.Parameters.AddWithValue("@l_name", (gvEmployees.FooterRow.FindControl("txtLNameFooter") as TextBox).Text.Trim());
                                    sqlCmd.Parameters.AddWithValue("@f_name", (gvEmployees.FooterRow.FindControl("txtFNameFooter") as TextBox).Text.Trim());
                                    sqlCmd.Parameters.AddWithValue("@fix_payment", Convert.ToString((FixPayment)Convert.ToInt32((gvEmployees.FooterRow.FindControl("chkFixPaymentFooter") as CheckBox).Checked)));
                                    sqlCmd.Parameters.AddWithValue("@count_days", (gvEmployees.FooterRow.FindControl("txtCountDaysFooter") as TextBox).Text.Trim());
                                    sqlCmd.Parameters.AddWithValue("@count_hour", (gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Text.Trim());
                                    sqlCmd.Parameters.AddWithValue("@rate", Convert.ToSingle((gvEmployees.FooterRow.FindControl("txtRateFooter") as TextBox).Text.Trim().Replace(".", ",")));
                                    sqlCmd.ExecuteNonQuery();
                                    FillGridView();
                                    string script = "<script type=\"text/javascript\">alert('Новая запись добавлена!');</script>";
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                                    lblErrorMessage.Text = "";
                                }
                            }
                        }
                        else
                        {
                            string script = "<script type=\"text/javascript\">alert('Не все поля заполнены!');</script>";
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = ex.Message;
                }
            }
        }


        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int getIndex = gvEmployees.EditIndex = e.NewEditIndex;
            FillGridView();
            CheckBox chk = (CheckBox)gvEmployees.Rows[getIndex].FindControl("chkFixPayment");
            if (chk.Checked)
            {
                (gvEmployees.Rows[getIndex].FindControl("txtCountHours") as TextBox).Visible = false;
            }
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
                // Проверка заполненности текстовых полей
                if (!string.IsNullOrEmpty((gvEmployees.Rows[e.RowIndex].FindControl("txtLName") as TextBox).Text)
                    && !string.IsNullOrWhiteSpace((gvEmployees.Rows[e.RowIndex].FindControl("txtLName") as TextBox).Text) &&
                    !string.IsNullOrEmpty((gvEmployees.Rows[e.RowIndex].FindControl("txtFName") as TextBox).Text)
                    && !string.IsNullOrWhiteSpace((gvEmployees.Rows[e.RowIndex].FindControl("txtFName") as TextBox).Text) &&
                    !string.IsNullOrEmpty((gvEmployees.Rows[e.RowIndex].FindControl("txtCountDays") as TextBox).Text)
                    && !string.IsNullOrWhiteSpace((gvEmployees.Rows[e.RowIndex].FindControl("txtCountDays") as TextBox).Text) &&
                    !string.IsNullOrEmpty((gvEmployees.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text)
                    && !string.IsNullOrWhiteSpace((gvEmployees.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text) &&
                    ((!string.IsNullOrEmpty((gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Text)
                    && !string.IsNullOrWhiteSpace((gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Text) &&
                    (gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Visible) ||
                    !(gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Visible))
                {
                    if (!(gvEmployees.Rows[e.RowIndex].FindControl("chkFixPayment") as CheckBox).Checked
                                && (gvEmployees.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text == "0")
                    {
                        string script = "<script type=\"text/javascript\">alert('Ставка не может быть 0!');</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                    else
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
                            sqlCmd.Parameters.AddWithValue("@fix_payment", Convert.ToString((FixPayment)Convert.ToInt32((gvEmployees.Rows[e.RowIndex].FindControl("chkFixPayment") as CheckBox).Checked)));
                            sqlCmd.Parameters.AddWithValue("@count_days", (gvEmployees.Rows[e.RowIndex].FindControl("txtCountDays") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@count_hour", (gvEmployees.Rows[e.RowIndex].FindControl("txtCountHours") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@rate", Convert.ToSingle((gvEmployees.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text.Trim().Replace(".", ",")));
                            sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value.ToString()));
                            sqlCmd.ExecuteNonQuery();
                            gvEmployees.EditIndex = -1;
                            FillGridView();
                            string script = "<script type=\"text/javascript\">alert('Выбранная запись изменена!');</script>";
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                            lblErrorMessage.Text = "";
                        }
                    }
                }
                else
                {
                    string script = "<script type=\"text/javascript\">alert('Не все поля заполнены!');</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
            catch (Exception ex)
            {
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
                    string script = "<script type=\"text/javascript\">alert('Выбранная запись удалена!');</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    FillGridView();
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {

                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void chkFixPaymentFooter_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)gvEmployees.FooterRow.FindControl("chkFixPaymentFooter");
            if (chk != null)
            {
                if (chk.Checked) (gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Visible = false;
                else (gvEmployees.FooterRow.FindControl("txtCountHoursFooter") as TextBox).Visible = true;
            }
        }

        protected void chkFixPayment_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox chk = (CheckBox)gvEmployees.Rows[index].FindControl("chkFixPayment");
            if (chk != null)
            {
                if (chk.Checked)
                {
                    (gvEmployees.Rows[index].FindControl("txtCountHours") as TextBox).Visible = false;
                }
                else
                {
                    (gvEmployees.Rows[index].FindControl("txtCountHours") as TextBox).Visible = true;
                }
            }
        }


        protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            FillGridView();
        }

        
        static public int WorkDays(int year, int month)
        {
            // всего дней в месяце
            int allDays = DateTime.DaysInMonth(year, month);
            // дата первого дня определенного месяца в определенном году
            DateTime date = new DateTime(year, month, 1);
            int countWeekend = 0;
            while (true)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) countWeekend++;
                date = date.AddDays(1.0);
                if (date.Month != month) break;
            }
            return allDays - countWeekend;
        }


        static public float CalculateSalary(string fix, int hours, float rate)
        {
            return fix=="T" ? rate : hours * rate;
        }
    }
}