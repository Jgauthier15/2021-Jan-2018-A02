<%@ Page Title="ODS Repeater" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSRepeater.aspx.cs" Inherits="WebApp.SamplePages.ODSRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater with Nested Query</h1>
    <div class="row">
        <div class="offset-2">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="offset-2">
            <asp:Repeater ID="EmployeeCustomers" runat="server" 
                DataSourceID="EmployeeCustomersODS"
                 ItemType="ChinookSystem.ViewModels.EmployeeCustomerList">
                <HeaderTemplate>
                    <h3>Sales Support Employees</h3>
                </HeaderTemplate>
                <ItemTemplate>
                    <br />
                    <%# Item.EmployeeName %> (<%# Item.Title %>) has 
                      <%# Item.CustomerSupportCount %> customers <br /><br />
                   <%-- <asp:GridView ID="SupportedCustomersOfEmployee" runat="server"
                         DataSource="<%# Item.CustomerList %>">
                    </asp:GridView>--%>
                    <asp:Repeater ID="SupportedCustomersOfEmployee" runat="server"
                         DataSource="<%# Item.CustomerList %>"
                         ItemType="ChinookSystem.ViewModels.CustomerSupportItem">
                        <ItemTemplate>
                            Name: <%# Item.CustomerName %>&nbsp;&nbsp;
                            Phone: <%# Item.Phone %>&nbsp;&nbsp;
                            City: <%# Item.City %>&nbsp;&nbsp;
                            State: <%# Item.State == null ? "Unknown" : Item.State %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource ID="EmployeeCustomersODS" runat="server" 
                OldValuesParameterFormatString="original_{0}" 
                SelectMethod="Employee_EmployeeCustomerList" 
                 OnSelected="SelectCheckForException"
                TypeName="ChinookSystem.BLL.EmployeeController">

            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>