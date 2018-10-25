<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Card.aspx.cs" Inherits="Game.Web.UserService.JFT.Card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery.min.js"></script>
    <script src="/js/Check.js" type="text/javascript"></script>
    <script src="/js/utils.js" type="text/javascript"></script>    
    <style>
      body {width: 696px; height: 420px; padding: 5px; background: #251b14 url("/images/pay/pay_bg.png") no-repeat;}
      .ui-pay-menu {padding: 0 26px; font-size: 0;}
      .ui-pay-menu span, .ui-pay-menu a {display: inline-block; *zoom: 1; *display: inline; font-size: 0;}
      .ui-pay-menu a {
        width: 210px; height: 40px;
        background: url("/images/pay/menu.png") no-repeat;
      }
      .ui-pay-menu-2 {margin: 0 7px;}
      .ui-pay-menu-1 a {background-position: 0 0;}
      .ui-pay-menu-1 a.ui-pay-active {background-position: 0 -120px;}
      .ui-pay-menu-2 a {background-position: 0 -40px;}
      .ui-pay-menu-2 a.ui-pay-active {background-position: 0 -160px;}
      .ui-pay-menu-3 a {background-position: 0 -80px;}
      .ui-pay-menu-3 a.ui-pay-active {background-position: 0 -200px;}
      .ui-pay-content {padding: 20px;}
      .ui-pay-title {font-size: 15px; line-height: 22px; color: #fc9; text-align: center; margin-bottom: 10px;
        background: url("/images/popup/feedback_line.png") center bottom no-repeat;}
      .ui-pay-title span {font-size: 15px; line-height: 22px; color: #f60;}
      .ui-pay-info {text-align: center; padding-bottom: 20px;}
      .ui-pay-info .ui-position {margin-bottom: 10px; position: relative;}
      .ui-pay-info .ui-position span {position: absolute; left: 460px; font-size: 12px; color: #f00;}
      .ui-pay-info li>label { font-size: 14px; line-height: 20px; color: #FF9F4A;}
      .ui-pay-info li>p {font-size: 12px; color: #ccc;}
      .ui-text-1 {background: #2b190e; border: 0; color: #fc9; font-size: 12px; height: 21px; outline: none;}
      .ui-pay-btn-1,
      .ui-pay-btn-2 {font-size: 0; border: none; width: 92px; height: 34px; cursor: pointer;}
      .ui-pay-btn-1 {background: url("/images/pay/submit.png") no-repeat;}
      .ui-pay-btn-2 {background: url("/images/pay/reset.png") no-repeat;}
      .ui-pay-btn-go {background: url("/images/pay/go_on.png") no-repeat;font-size: 0; border: none; width: 92px; height: 34px; cursor: pointer;}
      .ui-text-1:focus {border: 1px solid #f60; outline: none; width: 168px; height: 20px; line-height: 20px; outline: none;}
      .ui-imitate-select {display: inline-block; *zoom: 1; *display: inline; width: 170px; text-align: left;
       position: relative; z-index: 999;}
      .ui-imitate-select p {color: #fc9; font-size: 12px; line-height: 21px; text-indent: 5px;
      background: #2b190e url("/images/pay_select.png") right no-repeat;}
      .ui-select-list {position: absolute; top: 21px; left: 0; width: 170px; background: #2b190e;}
      .ui-pay-info .ui-select-list li {font-size: 12px; color: #fc9; line-height: 16px; margin-bottom: 0;
       padding-left: 5px;}
      .ui-pay-info .ui-select-list li:hover {background: #00caed; color: #fff;}
    </style>
    <!--[if IE]> <style>
                    .ui-text-1:focus {height: 19px; line-height: 19px;}
                 </style> <![endif]-->
</head>
<body>
    <div class="ui-pay-menu">
      <span class="ui-pay-menu-1"><a href="/UserService/PayIndex.aspx">选择充值方式</a></span><!--
      --><span class="ui-pay-menu-2"><a href="javascript:;" class="ui-pay-active">填写充值信息</a></span><!--
      --><span class="ui-pay-menu-3"><a href="javascript:;">充值完成</a></span>
    </div>
    <div class="ui-pay-content">
      <div class="ui-pay-title">您选择了&nbsp;<span><%= cardName %></span>&nbsp;方式</div>
      <form name="fmStep1" runat="server" id="fmStep1">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#txtPayAccounts").blur(function () { checkAccounts(); });
                $("#txtPayReAccounts").blur(function () { checkReAccounts(); });
                //$("#txtPayAmount").blur(function () { checkAmount(); });
                $("#txtCardNumber").blur(function () { checkCardNumber(); });
                $("#txtCardPassword").blur(function () { checkCardPassword(); });

                $("#btnPay").click(function () {
                    return checkInput();
                });
            });

            function checkAccounts() {
                if ($.trim($("#txtPayAccounts").val()) == "") {
                    $("#txtPayAccountsTips").html("请输入您的游戏帐号");
                    return false;
                }
                $("#txtPayAccountsTips").html("");
                return true;
            }

            function checkReAccounts() {
                if ($.trim($("#txtPayReAccounts").val()) == "") {
                    $("#txtPayReAccountsTips").html("请再次输入游戏帐号");
                    return false;
                }
                if ($("#txtPayReAccounts").val() != $("#txtPayAccounts").val()) {
                    $("#txtPayReAccountsTips").html("两次输入的帐号不一致");
                    return false;
                }
                $("#txtPayReAccountsTips").html("");
                return true;
            }

            function checkCardNumber() {
                if ($.trim($("#txtCardNumber").val()) == "") {
                    $("#txtCardNumberTips").html("请输入充值卡号");
                    return false;
                }
                $("#txtCardNumberTips").html("");
                return true;
            }

            function checkCardPassword() {
                if ($.trim($("#txtCardPassword").val()) == "") {
                    $("#txtCardPasswordTips").html("请输入充值卡密码");
                    return false;
                }
                $("#txtCardPasswordTips").html("");
                return true;
            }

            function checkAmount()
            {
                if ($.trim($("#ddlAmount").val()) == "0") {
                    $("#ddlAmountTips").html("请选择充值卡面值");
                    return false;
                }
                $("#ddlAmountTips").html("");
                return true;
            }


            function checkInput() {
                if (!checkAccounts()) { $("#txtPayAccounts").focus(); return false; }
                if (!checkReAccounts()) { $("#txtPayReAccounts").focus(); return false; }
                if (!checkAmount()) { return false; }
                if (!checkCardNumber()) { $("#txtCardNumber").focus(); return false; }
                if (!checkCardPassword()) { $("#txtCardPassword").focus(); return false; }
            }
        </script>
        <ul class="ui-pay-info">
          <li class="ui-position">
            <label>游戏帐号：</label>
            <asp:TextBox ID="txtPayAccounts" runat="server" CssClass="ui-text-1"></asp:TextBox>
            <span id="txtPayAccountsTips" style=" color:Red"></span>
          </li>
          <li class="ui-position">
            <label>确认帐号：</label>
            <asp:TextBox ID="txtPayReAccounts" runat="server" CssClass="ui-text-1"></asp:TextBox>
            <span id="txtPayReAccountsTips" style="color:Red;"></span>
          </li>
          <li class="ui-position" style="z-index: 1;">
            <label>选择面值：</label>
            <asp:DropDownList ID="ddlAmount" runat="server" CssClass="ui-text-1">
                <asp:ListItem Value="0" Text="---请选择卡面值---"></asp:ListItem>
            </asp:DropDownList>
            <!--<div id="" class="ui-imitate-select">-->
              <!--<p>-&#45;&#45;请选择卡面值-&#45;&#45;</p>-->
              <!--<ul class="ui-select-list">-->
                <!--<li>5</li>-->
                <!--<li>6</li>-->
                <!--<li>10</li>-->
                <!--<li>15</li>-->
                <!--<li>20</li>-->
                <!--<li>30</li>-->
                <!--<li>50</li>-->
              <!--</ul>-->
            <!--</div>-->
            <span id="ddlAmountTips" style="color:Red;"></span>
          </li>
          <li class="ui-position">
            <label>充值卡号：</label>
            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="ui-text-1"></asp:TextBox>
            <span id="txtCardNumberTips" style="color:Red;"></span>
          </li>
          <li class="ui-position">
            <label>充值密码：</label>
            <asp:TextBox ID="txtCardPassword" TextMode="Password" runat="server" CssClass="ui-text-1"></asp:TextBox>
            <span id="txtCardPasswordTips" style="color:Red;"></span>
          </li>
          <li class="ui-position">
            <p id="lblPayInfo">10 元= <%= 10 * rateGameBean%>游戏豆</p>
            <span id="ePayInfo" style="color:Red;"></span>
            <input type="hidden" name="hdfSalePrice" id="hdfSalePrice" runat="server" value="10" />
          </li>
          <li class="ui-position">
            <asp:Button ID="btnPay" runat="server" CssClass="ui-pay-btn-1" Text="确定" onclick="btnPay_Click" />
            <input type="reset" value="重置" class="ui-pay-btn-2"/>
          </li>
        </ul>
      </form>

      <form id="fmStep2" runat="server" action="http://pay.jtpay.com/form/pay" method="post">
        <div class="ui-result">
        <p>
            <asp:Label ID="lblAlertIcon" runat="server"></asp:Label>
            <asp:Label ID="lblAlertInfo" runat="server" Text="操作提示"></asp:Label>
            <%= formData%>
        </p>
        <p id="pnlContinue" runat="server">
            <input id="btnReset1" type="button" value="继续充值" onclick="goURL('/UserService/PayIndex.aspx');" class="ui-pay-btn-go" />
        </p>
    </div>
    </form>
    <%= js %>
    </div>    
  </body>
</html>
