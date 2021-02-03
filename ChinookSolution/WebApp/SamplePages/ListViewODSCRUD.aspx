<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.WebForm1" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Single Record CRUD using ODS: ListView</h1>
    <div class="row">
        <div class="offset-2">
            <blockquote class="alert alert-info">
                This sample will use the asp:ListView control <br/>
                This sample will use ObjectDataSource for the ListView control <br/>
                This sample will use minimal code behind<br />
                This sample will use the course MessageUserControl for error handling<br />
                This sample will use validation on the ListView control
            </blockquote>
        </div>
    </div>
    <div class="row">
        <div class="offset-2">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" 
                 HeaderText="Following are concerns with your data while editing."
                 ValidationGroup="egroup"/>
            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" 
                HeaderText="Following are concerns with your data while inserting."
                 ValidationGroup="igroup"/>
        </div>
    </div>
    <div class="row">
        <div class="offset-2">

        <%-- REMINDER: Use the Attribute DataKeyNames to get the DELETE function of your ListView CRUD to work
            
            The DataKeyNAmes attribute is set to your pkey field--%>

        <asp:ListView ID="AlbumList" runat="server" 
            DataSourceID="AlbumListODS" 
            InsertItemPosition="FirstItem"
            DataKeyNames ="AlbumId">

            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #284775;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" 
                            Text="Delete" ID="DeleteButton"
                             OnClientClick="return confirm('Are you sure you wish to remove this album?')"/>
                        <asp:Button runat="server" CommandName="Edit" 
                            Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel"/></td>
                    <td>
                        <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                    <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayField" 
                            DataValueField="ValueField"
                            SelectedValue='<%# Eval("ArtistId") %>'
                            Enabled="false"
                            Width="300px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                </tr>
            </AlternatingItemTemplate>

            <EditItemTemplate>
                <%-- Validation controls will be placed inside the associated template
                     The ID of the validated control needs to be unique
                     The validation controls for a particular Template needs to be GROUPED *****
                    
                    The validation excutes ONLY on the use of the template where the group is tied to the Button.--%>
                <asp:RequiredFieldValidator ID="RequiredTitleE" runat="server" 
                    ErrorMessage="Album title is required when editing."
                    ControlToValidate="TitleTextBoxE" Display="None"
                    ValidationGroup="egroup">
                </asp:RequiredFieldValidator>
                <tr style="background-color: #999999;">
                    <td>
                        <%-- Tie the button to the appropriate validation group (ie:egroup)
                            FAILURE to tie the button to the appropriate group will causse ALL validation on the page to execute.--%>
                        <asp:Button runat="server" CommandName="Update" 
                            Text="Update" ID="UpdateButton" 
                            ValidationGroup ="egroup"/>

                        <asp:Button runat="server" CommandName="Cancel" 
                            Text="Cancel" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox"
                            Width="50px" Enabled="false" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                    <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayField" 
                            DataValueField="ValueField"
                            SelectedValue='<%# Bind("ArtistId") %>'
                            Enabled="true"
                            Width="300px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox"
                            Width="60px"/></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>

            <InsertItemTemplate>
                <asp:RequiredFieldValidator ID="RequiredTitleI" runat="server" 
                    ErrorMessage="Album title is required when inserting."
                    ControlToValidate="TitleTextBoxI" Display="None"
                    ValidationGroup="igroup">
                </asp:RequiredFieldValidator>
                <tr style="">
                    <td>
                        <asp:Button runat="server" CommandName="Insert" 
                            Text="Insert" ID="InsertButton"
                            ValidationGroup="igroup" Display="none"/>
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" 
                            Width="50px"/></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                    <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayField" 
                            DataValueField="ValueField"
                            SelectedValue='<%# Bind("ArtistId") %>'
                            Enabled="true"
                            Width="300px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox"
                            Width="60px"/></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                </tr>
            </InsertItemTemplate>


            <ItemTemplate>
                <tr style="background-color: #E0FFFF; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel"
                            Width="50px"/></td>
                    <td>
                        <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                    <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayField" 
                            DataValueField="ValueField"
                            SelectedValue='<%# Eval("ArtistId") %>'
                            Enabled="false"
                            Width="300px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" 
                            Width="60px"/></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                    <th runat="server"></th>
                                    <th runat="server">Id</th>
                                    <th runat="server">Title</th>
                                    <th runat="server">Artist</th>
                                    <th runat="server">Year</th>
                                    <th runat="server">Label</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #c0c0c0; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    <asp:NumericPagerField></asp:NumericPagerField>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                    <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayField" 
                            DataValueField="ValueField"
                            SelectedValue='<%# Eval("ArtistId") %>'
                            Enabled="false"
                            Width="300px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 

            TypeName="ChinookSystem.BLL.AlbumController" 
            DataObjectTypeName="ChinookSystem.ViewModels.AlbumItem" 
            DeleteMethod="Album_Delete"
             OnDeleted="DeleteCheckForException"
            InsertMethod="Album_Add"
             OnInserted="InsertCheckForException"
            SelectMethod="Albums_List" 
             OnSelected="SelectCheckForException"
            UpdateMethod="Album_Update"
             OnUpdated="UpdateCheckForException">
             
            

        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Artists_DDLList"
             OnSelected="SelectCheckForException"
            TypeName="ChinookSystem.BLL.ArtistController">
        </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>