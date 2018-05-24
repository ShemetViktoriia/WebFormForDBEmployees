<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="WebDbEmployees.Employees" %>

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
            <br />
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                ShowHeader="true"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" 
                OnRowCommand="gvEmployees_RowCommand" OnRowEditing="gvEmployees_RowEditing" OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
                OnRowUpdating="gvEmployees_RowUpdating" OnRowDeleting="gvEmployees_RowDeleting" 
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" ShowFooter="true">
                
                <%--Настройка GridView--%>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>
                    <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("L_NAME") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLName" Text='<%# Eval("L_NAME") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLNameFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("F_NAME") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFName" Text='<%# Eval("F_NAME") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFNameFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Фикс. ставка" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("FIX_PAYMENT") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFixPayment" Text='<%# Eval("FIX_PAYMENT") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFixPaymentFooter" runat="server" />
                        </FooterTemplate>
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
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ставка" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("RATE") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRate" Text='<%# Eval("RATE") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRateFooter" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="AddNew" Width="20px" Height="20px" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green" />
            <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
