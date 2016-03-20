<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PlayerList.aspx.vb" Inherits="Bath_MinecraftASPWebsite.PlayerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
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
    <div class="Gheading2">
        Online Players
        <div class="clearaway">
            <hr />
        </div>
        <div runat="server" id="onlinelist">
            <!--
            <div class="clearaway">

                <a class="onlineplayers" href="/PlayerMapProfile?PlayerID=x&MapID=y"></a>


            </div>
            -->
        </div>
    </div>

    <div class="Oheading2">
        Offline Players
        <div class ="clearaway">
            <hr />
        </div>
        <div runat="server" id="offlinelist">
            
<%--            <div class="clearaway">

                <a class="offlineplayers" href="/PlayerMapProfile?PlayerID=x&MapID=y"</div>


            </div>
            --%>
        </div>
    </div>

</asp:Content>
