﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Complaint-Setp-2.aspx.cs" Inherits="Game.Web.Member.Complaint_Setp_2" %>
<%@ Register TagPrefix="qp" TagName="Header" Src="~/Themes/Standard/Common_Header.ascx" %>
<%@ Register TagPrefix="qp" TagName="Footer" Src="~/Themes/Standard/Common_Footer.ascx" %>
<%@ Register TagPrefix="qp" TagName="UserSidebar" Src="~/Themes/Standard/User_Sidebar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/member/member-complaint.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <!--头部开始-->
    <qp:Header ID="sHeader" runat="server" PageID="4"/>
    <!--头部结束-->
    <div class="ui-banner">
      <div class="ui-banner-bg-1"></div>
      <div class="ui-banner-bg-2"></div>
      <div class="ui-carousel-right">
        <div class="ui-carousel-left">
          <div class="ui-banner-img">
            <a href="javascript:;"><img src="/images/banner_2.png" /></a>
          </div>
        </div>
      </div>
    </div>

    <div class="ui-content">
      <div class="ui-main">
        <div class="ui-page-title fn-clear">
          <a href="/index.aspx"><i class="ui-page-title-home"></i>首页</a>
          <i class="ui-page-title-current"></i>
          <a href="index.aspx">个人中心</a>
          <i class="ui-page-title-current"></i>
          <span>帐号申述</span>
          <div class="ui-page-title-right"><span>MEMBER&nbsp;CENTER</span><strong>个人中心</strong></div>
        </div>
        <div class="fn-clear">
          <!--左边开始-->
          <qp:UserSidebar ID="sUserSidebar" runat="server" MemberID="6" />
          <!--左边结束-->
          <div class="ui-main-details fn-right">
            <div class="ui-exchange-info">
                <h2 class="ui-title-solid">申述结果</h2>
                <div class="ui-result" style="padding-top:30px;">
                    <p>
                        <asp:Label ID="lblAlertIcon" runat="server"></asp:Label>
                        <asp:Label ID="lblAlertInfo" runat="server" Text="操作提示"></asp:Label>
                    </p>                    
                </div>
            </div>  
          </div>
        </div>
      </div>
    </div>

    <!--尾部开始-->
    <qp:Footer ID="sFooter" runat="server" />
    <!--尾部结束-->
</body>
</html>
