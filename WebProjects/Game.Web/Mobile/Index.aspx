<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Mobile.Index" %>

<!DOCTYPE html>
<html>
    <head>
        <title><%=title %></title>
        <meta content="text/html; charset=utf-8" http-equiv="content-type"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
        <meta name="apple-mobile-web-app-capable" content="yes">
        <meta name="apple-touch-fullscreen" content="yes"/>
        <meta name="format-detection" content="telephone=no">
        <link href="/Mobile/Css/mobile/base.css?v=1.0" rel="stylesheet" type="text/css"/>
        <link href="/Mobile/Css/mobile/index.css?v=1.0" rel="stylesheet" type="text/css"/>
    </head>
    <body>
        <div class="ui-foot-menu">
            <ul>
                <li><a href="tel:<%= tel %>" class="ui-tel">电话咨询</a></li>
                <li><a href="http://wpa.qq.com/msgrd?v=3&uin=<%= qq %>&site=qq&menu=yes" class="ui-qq">在线咨询</a></li>
            </ul>
        </div>
        <div class="ui-head"><img src="<%= Game.Facade.Fetch.GetUploadFileUrl("/Site/MobileLogo.png")%>"/></div>
        <div class="ui-mobile-box">
            <div class="ui-banner" style="visibility: visible;">
                <div class='ui-banner-wrap'>
                    <div><a href="<%= targetURL %>" target="_blank"><img src="<%= resourceURL %>"/></a></div>
                </div>
                <ul class="ui-banner-nav"></ul>
            </div>
            <div class="ui-hot-recommend">
                <h1>游戏下载</h1>
                <ul class="ui-game-list">
                <!--大厅下载-->
                <asp:Panel ID="plPlatform" runat="server" Visible="false">
                    <li class="fn-clear">
                        <a href="/Mobile/Platform.aspx" class="ui-applist-wrap fn-clear">
                            <div class="ui-game-ico">
                                <img src="/Mobile/Img/mobile/PlatformIcon.png" /></div>
                            <div class="ui-game-info">
                                <b>手机大厅</b>
                                <span class="ui-game-score ui-score1">
                                    <img src="/Mobile/Img/mobile/score_ico_1.png">
                                </span>
                            </div>
                        </a>
                        <a href="<%= platformDownloadUrl %>" class="ui-game-download" title="下载">下载 </a>
                    </li>
                </asp:Panel>
                <asp:Repeater ID="rptMoblieGame" runat="server">
                    <ItemTemplate>
                        <li class="fn-clear">
                            <a href="/Mobile/Info.aspx?id=<%# Eval("KindID")%>" class="ui-applist-wrap fn-clear">
                                <div class="ui-game-ico"><img src="<%# Game.Facade.Fetch.GetUploadFileUrl(Eval("ThumbnailUrl").ToString())%>"/></div>
                                <div class="ui-game-info">
                                    <b><%# Eval("KindName")%></b>
                                    <span class="ui-game-score ui-score1"><img src="/Mobile/Img/mobile/score_ico_1.png"> </span>
                                </div>
                            </a>
                            <a href="<%# terminalType==2? Eval("IosDownloadUrl"):Eval("AndroidDownloadUrl") %>" class="ui-game-download" title="下载">下载 </a>
                        </li>
                     </ItemTemplate>
                </asp:Repeater>
                </ul>
            </div>
        </div>
        <div id="weixin-tip" class="ui-weixin-tip fn-hide">
          <p>
            <img src="Img/mobile/live_weixin.png" alt="微信打开" />
            <span title="关闭" class="close">×</span>
          </p>
        </div>
        <script type="text/javascript" src="/Mobile/Js/mobile/zepto/1.1.6/zepto.min.js"></script>
        <script type="text/javascript" src="/Mobile/Js/mobile/swipe/2.0.0/swipe.js"></script>
        <script type="text/javascript" src="/Mobile/Js/mobile/index.js"></script>
    </body>
</html>