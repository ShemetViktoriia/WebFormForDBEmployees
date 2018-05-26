<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebDbEmployees.Employees" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Сотрудники</title>
    <link href="/favicon.ico" rel="shortcut icon" runat="server" type="image/x-icon" />
    <link href="/favicon.ico" rel="icon" runat="server" type="image/ico" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Учет ЗП сотрудников за месяц</h1>
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" 
                OnRowCommand="gvEmployees_RowCommand" OnRowEditing="gvEmployees_RowEditing" OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
                OnRowUpdating="gvEmployees_RowUpdating" OnRowDeleting="gvEmployees_RowDeleting" 
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Double" BorderWidth="2px" ShowFooter="True"
                AllowPaging="True" OnPageIndexChanging="gvEmployees_PageIndexChanging">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" BorderStyle="None" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" BorderWidth="2px" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>
                    <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("L_NAME") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLName" Text='<%# Eval("L_NAME") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLNameFooter" runat="server" />
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("F_NAME") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFName" Text='<%# Eval("F_NAME") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFNameFooter" runat="server"/>
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Фикс. ставка" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkFixPayment" runat="server"  
                                AutoPostBack="true" OnCheckedChanged="chkFixPayment_CheckedChanged" Enabled ="false"
                                Checked='<%# Convert.ToBoolean
                                        (Convert.ToInt32
                                            (Enum.Parse(typeof(WebDbEmployees.FixPayment),Convert.ToString(Eval("FIX_PAYMENT")))))%>' />  
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkFixPayment" runat="server"  
                                AutoPostBack="true" OnCheckedChanged="chkFixPayment_CheckedChanged"
                                Checked='<%# Convert.ToBoolean
                                        (Convert.ToInt32
                                            (Enum.Parse(typeof(WebDbEmployees.FixPayment),Convert.ToString(Eval("FIX_PAYMENT")))))%>' />  
                        </EditItemTemplate>
                        <FooterTemplate>
                             <asp:CheckBox ID="chkFixPaymentFooter" runat="server"  AutoPostBack="true" OnCheckedChanged="chkFixPaymentFooter_CheckedChanged"/>
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="К-во отраб. дней" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("COUNT_DAYS") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCountDays" Text='<%# Eval("COUNT_DAYS") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCountDaysFooter" runat="server" />
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="К-во отраб. часов" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("COUNT_HOUR") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCountHours" Text='<%# Eval("COUNT_HOUR") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCountHoursFooter" runat="server" />
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ставка" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# String.Format("{0:N2}", Eval("RATE")).Replace(",", ".") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRate" Text='<%#  String.Format("{0:N2}", Eval("RATE")).Replace(",", ".") %>'  runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRateFooter" runat="server" />
                        </FooterTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">  
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ЗП за месяц" HeaderStyle-HorizontalAlign="Left">
                         <ItemTemplate>
                            <%# String.Format("{0:N2}", CalculateSalary( Convert.ToString(Eval("FIX_PAYMENT")),  Convert.ToInt32(Eval("COUNT_HOUR")), Convert.ToSingle(Eval("RATE")))).Replace(",", ".")%>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="К-во раб. дней" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                             <%# WorkDays(2018,5)%>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>


                    <asp:TemplateField  HeaderStyle-Width = "5%">
                        <EditItemTemplate>
                            <asp:ImageButton runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/save.png" ToolTip="Update" Width="20px" />
                            <asp:ImageButton runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/cancel.png" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton runat="server" CommandName="AddNew" Height="20px" ImageUrl="~/Images/addnew.png" ToolTip="AddNew" Width="20px" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/edit.png" ToolTip="Edit" Width="20px" />
                            <asp:ImageButton runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/delete.png" ToolTip="Delete" Width="20px" 
                                OnClientClick="return confirm('Вы действительно хотите удалить запись?');"/>
                        </ItemTemplate>

<HeaderStyle Width="5%"></HeaderStyle>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
