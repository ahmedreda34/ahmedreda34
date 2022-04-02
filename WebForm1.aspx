<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style ="font-family :Arial">
            <asp:Button ID="Button1" runat="server" Text="get data from database" OnClick="Button1_Click" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                 DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit"  OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                    <asp:BoundField DataField="gender" HeaderText="gender" SortExpression="gender" />
                    <asp:BoundField DataField="totalmarks" HeaderText="totalmarks" SortExpression="totalmarks" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="Button2" runat="server" Text="update database table" OnClick="Button2_Click" />
            <asp:Label ID="Label1" runat="server" Text="Label"> </asp:Label>
            
        </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
