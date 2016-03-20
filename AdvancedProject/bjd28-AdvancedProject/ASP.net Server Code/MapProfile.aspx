<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MapProfile.aspx.vb" Inherits="Bath_MinecraftASPWebsite.MapProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .InActiveRightText {
            float: right;
            text-align: right;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(255, 128, 02);
            text-decoration: none;
        }

        .ActiveRightText {
            float: right;
            text-align: right;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(51, 153, 102);
            text-decoration: none;
        }
        .onlineplayers {
            color:rgb(51, 153, 102);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 20%;
            float: left;
            text-align: left;
            text-decoration:none;
        }
        .onlineplayers:hover {
            color: rgb(10, 121, 245)
        }
        .offlineplayers{
            color: rgb(255, 128, 0);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 20%;
            float: left;
            text-align: left;
            text-decoration:none;
        }
        .offlineplayers:hover {
            color: rgb(10, 121, 245)
        }
        .emptyListMessage {
            color: rgb(95, 95, 95);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 20%;
            float: left;
            text-align: left;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearaway" id="MapInfo" runat="server">
<%--        <div class="Oheading2">
            <div class="leftside">
                Map#15
            </div>
            <div class="rightside">
                <div class="InActiveRightText" >
                    09/11/2013
                <br />
                    This Map Is Active
                </div>
            </div>
        </div>--%>
    </div>

    <div class="clearaway">
        <div class="heading2">
            Players
        </div>
    </div>

    <div class="clearaway">
        <hr />
    </div>
    <div id="PlayerList" runat="server">
        <div class="clearaway">
            <a class="onlineplayers">PlayerA</a>
            <a class="offlineplayers">PlayerB</a>
            <a class="offlineplayers">PlayerC</a>
            <a class="onlineplayers">PlayerD</a>
            <a class="offlineplayers">PlayerE</a>
        </div>
        <div class="clearaway">
            <a class="offlineplayers">PlayerF</a>
        </div>
    </div>
</asp:Content>
