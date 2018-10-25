<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Shop_Sidebar.ascx.cs" Inherits="Game.Web.Themes.Standard.Shop_Sidebar" %>
<ul class="ui-match-submenu">
    <asp:repeater id="rptTopType" runat="server">
        <ItemTemplate>
            <li class="ui-submenu-<%#Container.ItemIndex+1 %>"><a <%# Convert.ToInt32(Eval("TypeID"))==typeID?"class='ui-submenu-active'":""%> href='/Shop/Index.aspx?param=<%# Eval("TypeID")%>' title="<%# Eval("TypeName") %>"><%# Eval("TypeName") %></a></li>
        </ItemTemplate>
    </asp:repeater>    
    <li class="ui-submenu-1"><a <%= ShopPageID==1?" class=\"ui-submenu-active\"":"" %> href="/Shop/Order.aspx">我的订单</a></li>    
</ul>