<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PlayerMapProfile.aspx.vb" Inherits="Bath_MinecraftASPWebsite.PlayerMapProfile" %>
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

        .lhalfrowHead{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(10, 121, 245);
            font-weight:bold;
            text-decoration:underline;
        }
        .rhalfrowHead{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(10, 121, 245);
            border-left: 1px solid rgb(10, 121, 245);
            font-weight:bold;
            text-decoration:underline;
        }
        .lhalfrowG{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(51, 153, 102);
        }
        .rhalfrowG{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(51, 153, 102);
            border-left: 1px solid rgb(10, 121, 245)
        }
        .lhalfrowO{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(255, 128, 02);
        }
        .rhalfrowO{
            float:left;
            width:49%;
            text-align:center;
            font-size: 15pt;
            font-family: "Berlin Sans FB Demi";
            color: rgb(255, 128, 02);
            border-left: 1px solid rgb(10, 121, 245)
        }
        .emptyListMessage {
            color: rgb(95, 95, 95);
            font-family: "Berlin Sans FB Demi";
            font-size: 15pt;
            width: 100%;
            float: left;
            text-align: left;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearaway" id="PlayerInfo" runat="server">
<%--        <div class="Oheading2">
            <div class="leftside">
                PlayerA
            </div>
            <div class="rightside">
                <div class="InActiveRightText">
                    Player Was Last Online 05/03/2002
                </div>
            </div>
        </div>--%>
    </div>

    <%--PvP Kills--%>
    <div class ="clearaway">
        <hr />
    </div>

    
    <div class ="clearaway">
        <div class="heading2">
            PvP Kills
        </div>
    </div>

    <div id="pvpkillsblock" runat="server">
        <div class="clearaway">
            <div class="lhalfrowHead">
                Date
            </div>
            <div class="rhalfrowHead">
                Victim
            </div>
        </div>
        <div id="pvpkills" runat="server">
        </div>
    </div>

        <%--PvP Deaths--%>
    <div class ="clearaway">
        <hr />
    </div>

    <div class ="clearaway">
        <div class="heading2">
            PvP Deaths
        </div>
    </div>

    <div id="pvpdeathsblock" runat="server">
        <div class="clearaway">
            <div class="lhalfrowHead">
                Date
            </div>
            <div class="rhalfrowHead">
                Killer
            </div>
        </div>
        <div id="pvpdeaths" runat="server">
        </div>
    </div>

    <%--Mob Kills--%>
    <div class ="clearaway">
        <hr />
    </div>

    <div class ="clearaway">
        <div class="heading2">
            Mob Kills
        </div>
    </div>

    <div class="clearaway">
        <div class="lhalfrowHead">
            Mob
        </div>
        <div class="rhalfrowHead">
            Number of Kills
        </div>        
    </div>
    
    <div class="clearaway">
        <div class="lhalfrowG">
            Zombie
        </div>
        <div class="rhalfrowG" id="zombie" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Skeleton
        </div>
        <div class="rhalfrowG" id="skeleton" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Creeper
        </div>
        <div class="rhalfrowG" id="Creeper" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Enderman
        </div>
        <div class="rhalfrowG" id="enderman" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Spider
        </div>
        <div class="rhalfrowG" id="Spider" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Cave Spider
        </div>
        <div class="rhalfrowG" id="caveSpider" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Silverfish
        </div>
        <div class="rhalfrowG" id="silverFish" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Witch
        </div>
        <div class="rhalfrowG" id="witch" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Wolf
        </div>
        <div class="rhalfrowG" id="wolf" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Slime
        </div>
        <div class="rhalfrowG" id="slime" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
           Zombie Pigman
        </div>
        <div class="rhalfrowG" id="zombiePigman" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Ghast
        </div>
        <div class="rhalfrowG" id="Ghast" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Magma Cube
        </div>
        <div class="rhalfrowG" id="MagmaCube" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Blaze
        </div>
        <div class="rhalfrowG" id="Blaze" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Wither Skeleton
        </div>
        <div class="rhalfrowG" id="WitherSkeleton" runat="server">
        </div>        
    </div>

    <div class="clearaway">
        <div class="lhalfrowG">
            Mob
        </div>
        <div class="rhalfrowG" id="Wither" runat="server">
        </div>        
    </div>
    <div class="clearaway">
        <div class="lhalfrowG">
            Ender Dragon
        </div>
        <div class="rhalfrowG" id="EnderDragon" runat="server">
        </div>        
    </div>


    <%--Deaths--%>
        <div class ="clearaway">
        <hr />
    </div>

    <div class ="clearaway">
        <div class="heading2">
            Non-PvP Deaths
        </div>
    </div>

    <div id="deathblock" runat="server">
        <div class="clearaway">
            <div class="lhalfrowHead">
                Date
            </div>
            <div class="rhalfrowHead">
                Death Log
            </div>
        </div>
        <div id="deathLists" runat="server">
        </div>
    </div>
</asp:Content>
