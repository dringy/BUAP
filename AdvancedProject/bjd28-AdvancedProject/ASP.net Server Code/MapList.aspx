<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MapList.aspx.vb" Inherits="Bath_MinecraftASPWebsite.MapList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .activemap {
            color: rgb(51, 153, 102);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 24%;
            float: left;
            text-align: center;
            text-decoration: none;
        }

        .inactivemap {
            color: rgb(255, 128, 02);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 24%;
            float: left;
            text-align: center;
            text-decoration: none;
        }

        .mapheading {
            color: rgb(10, 121, 245);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 24%;
            float: left;
            text-align: center;
            font-weight: bold;
        }

        .nextbutton {
            float: right;
            text-align: right;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(255, 128, 02);
            text-decoration: none;
        }

        .backbutton {
            float: left;
            text-align: left;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(255, 128, 02);
            text-decoration: none;
        }

        .nextbutton:hover {
            color: rgb(10, 121, 245);
        }

        .backbutton:hover {
            color: rgb(10, 121, 245);
        }

        .activemap:hover {
            color: rgb(10, 121, 245);
        }

        .inactivemap:hover {
            color: rgb(10, 121, 245);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="clearaway">
        <a class ="backbutton" id="backbutton" runat="server"></a>
        <a class ="nextbutton" id="nextbutton" runat="server"></a>
    </div>

    <div class="clearaway">
        <div class="mapheading">#MapID</div>
        <div class="mapheading">Start Date</div>
        <div class="mapheading">#MapID</div>
        <div class="mapheading">StartDate</div>
    </div>

    <div class="clearaway" id="ASPmaplist" runat="server">

<%--    <div class="clearaway">
        <a class="inactivemap">#1</a>
        <a class="inactivemap">01/01/2000</a>
        <a class="inactivemap">#2</a>
        <a class="inactivemap">01/01/2000</a>
    </div>

    <div class="clearaway">
        <a class="activemap">#3</a>
        <a class="activemap">01/01/2000</a>
        <a class="inactivemap">#4</a>
        <a class="inactivemap">01/01/2000</a>
    </div>--%>

    </div>
</asp:Content>
