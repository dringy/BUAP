Public Class PlayerMapProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MapID As Integer
        Dim PlayerID As Integer
        'This Page requires a PlayerID and MapID from querystring
        If IsNumeric(Request.QueryString("PlayerID")) = True And IsNumeric(Request.QueryString("MapID")) = True Then
            'If the input query string is numeric store them in respective variables
            MapID = CInt(Request.QueryString("MapID"))
            PlayerID = CInt(Request.QueryString("PlayerID"))
        Else
            'If the querystrings are not numeric redirect to PlayerList.aspx
            Response.Redirect("/PlayerList.aspx")
        End If

        'Create a PlayerMapStats object and use the in built function to populate the fields from the database using Player ID and Map ID
        'If the function returns false then there is no matching MapPlayerStats with those ID's
        Dim PlayerStats As New PlayerMapStats
        If PlayerStats.GetStats(PlayerID, MapID) = False Then
            'In the event the Map ID , PlayerID pair is invalid redirect to PlayerList.aspx
            Response.Redirect("/PlayerList.aspx")
        End If


        If PlayerStats.Player.IsOnline = False Then
            'If the player is offline convert the Last Online Date to a string in the format dd/mm/yyyy
            Dim LastOnlineDisplayDate As String = PlayerStats.Player.LastOnlineDate.ToString("dd/MM/yyyy")
            'Display Name and Date in orange
            PlayerInfo.InnerHtml = "<div class=""Oheading2""><div class=""leftside"">" & PlayerStats.Player.Name &
                " - Map#" & MapID & "</div><div class=""rightside""><div class=""InActiveRightText"">Player Was Last Online " &
                LastOnlineDisplayDate & ".</div></div></div>"
        Else
            'If the player is online display the player name in green
            PlayerInfo.InnerHtml = "<div class=""Gheading2""><div class=""leftside"">" & PlayerStats.Player.Name &
                " - Map#" & MapID & "</div><div class=""rightside""><div class=""ActiveRightText"">Player is Online.</div></div></div>"
        End If

        If PlayerStats.PvPKills.Count > 0 Then
            For i As Integer = 0 To PlayerStats.PvPKills.Count - 1
                'Loop through each PvP kill log and display a row table containing the Name and the Date
                pvpkills.InnerHtml += "<div class=""clearaway""><div class=""lhalfrowG"">" & PlayerStats.PvPKills(i).PvPDate &
                    "</div><div class=""rhalfrowG"">" & PlayerStats.PvPKills(i).PlayerName & "</div></div>"
            Next
        Else
            'If there are no pvp kills display message
            pvpkillsblock.InnerHtml = "<div class =""emptyListMessage"">This Player has no PvP Kills in this map</div>"
        End If

        'Display the mob kill number into the table
        caveSpider.InnerHtml = PlayerStats.CaveSpiderKills
        enderman.InnerHtml = PlayerStats.EndermanKills
        Spider.InnerHtml = PlayerStats.SpiderKills
        wolf.InnerHtml = PlayerStats.WolfKills
        zombiePigman.InnerHtml = PlayerStats.ZombiePigmanKills
        Blaze.InnerHtml = PlayerStats.BlazeKills
        Creeper.InnerHtml = PlayerStats.CreeperKills
        Ghast.InnerHtml = PlayerStats.GhastKills
        MagmaCube.InnerHtml = PlayerStats.MagmaCubeKills
        silverFish.InnerHtml = PlayerStats.SilverfishKills
        skeleton.InnerHtml = PlayerStats.SkeletonKills
        slime.InnerHtml = PlayerStats.SlimeKills
        witch.InnerHtml = PlayerStats.WitchKills
        WitherSkeleton.InnerHtml = PlayerStats.WitherSkeletonKills
        zombie.InnerHtml = PlayerStats.ZombieKills
        Wither.InnerHtml = PlayerStats.WitherKills
        'Convert boolean: EnderDragonKilled into a number
        If PlayerStats.EnderDragonKilled = True Then
            EnderDragon.InnerHtml = "1"
        Else
            EnderDragon.InnerHtml = "0"
        End If

        If PlayerStats.PvPDeaths.Count > 0 Then
            For i As Integer = 0 To PlayerStats.PvPDeaths.Count - 1
                'Loop through each PvP death log and display a row table containing the Name and the Date
                pvpdeaths.InnerHtml += "<div class=""clearaway""><div class=""lhalfrowO"">" & PlayerStats.PvPDeaths(i).PvPDate &
                    "</div><div class=""rhalfrowO"">" & PlayerStats.PvPDeaths(i).PlayerName & "</div></div>"
            Next
        Else
            'if there are no pvp death logs display message
            pvpdeathsblock.InnerHtml = "<div class =""emptyListMessage"">This Player has no PvP Deaths in this map</div>"
        End If

        If PlayerStats.Deaths.Count > 0 Then
            For i As Integer = 0 To PlayerStats.Deaths.Count - 1
                'Loop through each death log and display a table containing the date and death message
                deathLists.InnerHtml += "<div class=""clearaway""><div class=""lhalfrowO"">" & PlayerStats.Deaths(i).DeathDate &
                    "</div><div class=""rhalfrowO"">" & PlayerStats.Deaths(i).DeathLog & "</div></div>"
            Next
        Else
            'if there are no death logs display message
            deathblock.InnerHtml = "<div class =""emptyListMessage"">This Player has not died on this map</div>"
        End If

    End Sub

End Class