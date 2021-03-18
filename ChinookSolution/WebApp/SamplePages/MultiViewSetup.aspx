<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiViewSetup.aspx.cs"
    Inherits="WebApp.SamplePages.MultiViewSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>MultiView Control Setup</h1>
    <div class="row">
        <div class="offset-1">
            <asp:Label ID="Label1" runat="server" Text="Common Data or Controls on the web page independent of the multiview control"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="offset-1">
            <asp:Menu ID="Menu1" runat="server"
                Font-Size="Large"
                Orientation="Horizontal"
                CssClass="tabs"
                StaticSelectedStyle-CssClass="selectedTab"
                StaticSelectedStyle-BackColor="LightBlue"
                StaticMenuItemStyle-HorizontalPadding="50px" OnMenuItemClick="Menu1_MenuItemClick">

                <Items>
                    <asp:MenuItem Selected="True" Text="Page 1" Value="0"></asp:MenuItem>
                    <asp:MenuItem Text="Page 2" Value="1"></asp:MenuItem>
                    <asp:MenuItem Text="Page 3" Value="2"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
    </div>

    <div class="row">
        <div class="offset-1">
            <asp:MultiView ID="MultiView1" runat="server"
                ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV1" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo2From1" runat="server" Text="Send To 2 From 1" OnClick="SendTo2From1_Click" />
                    <asp:Button ID="SendTo3From1" runat="server" Text="Send To 3 From 1" OnClick="SendTo3From1_Click" />
                </asp:View>

                <asp:View ID="View2" runat="server">
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV2" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo1From2" runat="server" Text="Send To 1 From 2" OnClick="SendTo1From2_Click" />
                    <asp:Button ID="SendTo3From2" runat="server" Text="Send To 3 From 2" OnClick="SendTo3From2_Click" />
                </asp:View>

                <asp:View ID="View3" runat="server">
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="IODataV3" runat="server"></asp:TextBox>
                    <asp:Button ID="SendTo1From3" runat="server" Text="Send To 1 From 3" OnClick="SendTo1From3_Click" />
                    <asp:Button ID="SendTo2From3" runat="server" Text="Send To 2 From 3" OnClick="SendTo2From3_Click" />
                </asp:View>

            </asp:MultiView>
        </div>
    </div>

</asp:Content>
