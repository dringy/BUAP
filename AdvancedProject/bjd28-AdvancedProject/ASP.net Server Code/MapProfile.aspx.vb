Public Class MapProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MapID As Integer
        'This site required MapID through querystring.
        'If the querystring is a number than we store it in MapID
        If IsNumeric(Request.QueryString("MapID")) = True Then
            MapID = Int(Request.QueryString("MapID"))
        Else
            'If there isn't a valid query string, we can't continue so redirect the browser to MapList.aspx
            Response.Redirect("/MapList.aspx")
        End If

        Dim Map As New Map 'Create new map data
        'getMapData will populate Map from the database using the MapID given to it.
        ' if, however the MapID is not found then the function will return false
        If Map.getMapData(MapID) = False Then
            'If there is no matching Map then we can't continue so redirect the browser to MapList.aspx
            Response.Redirect("/MapList.aspx")
        End If

        'Convert extracted Date of creation to a string in the format dd/mm/yyyy
        Dim DisplayDate As String = Map.DateOfCreation.ToString("dd/MM/yyyy")
        If Map.isActive = True Then
            'If the map is active, display the title and date in green text
            MapInfo.InnerHtml = "<div class=""Gheading2""><div class=""leftside"">Map#" & MapID &
                "</div><div class=""rightside""><div class=""ActiveRightText"">" & DisplayDate &
                "<br />This Map is Active</div></div></div>"
        Else
            'If the map is not active, display the title and date in orange text
            MapInfo.InnerHtml = "<div class=""Oheading2""><div class=""leftside"">Map#" & MapID &
                "</div><div class=""rightside""><div class=""InActiveRightText"">" & DisplayDate &
                "<br />This Map is Not Currently Active</div></div></div>"
        End If

        Dim columnNo As Integer = 1 'Represents which column the player name will be put on, as 5 will be listed in a row
        PlayerList.InnerHtml = ""
        For i As Integer = 0 To Map.MapPlayers.Count - 1
            'Loops through for each player that has played on the map
            If columnNo = 1 Then
                'If we are on column 1 we need to start a new row
                PlayerList.InnerHtml += "<div class=""clearaway"">"
            End If

            If Map.MapPlayers(i).IsOnline = True Then
                'If the player is online display the name in green
                'A link to the PlayerMapProfile with the mapID and playerID given as query-strings
                PlayerList.InnerHtml += "<a class=""onlineplayers"" href=""PlayerMapProfile.aspx?PlayerID=" & Map.MapPlayers(i).PlayerID & "&MapID=" & MapID & """>" & Map.MapPlayers(i).Name & "</a>"
            Else
                'If the player is not online display the name in orange
                'A link to the PlayerMapProfile with the mapID and playerID given as query-strings
                PlayerList.InnerHtml += "<a class=""offlineplayers"" href=""PlayerMapProfile.aspx?PlayerID=" & Map.MapPlayers(i).PlayerID & "&MapID=" & MapID & """>" & Map.MapPlayers(i).Name & "</a>"
            End If

            If columnNo = 5 Then
                'If we are on column 5 we now need to end the row, and start again from 1
                PlayerList.InnerHtml += "</div>"
                columnNo = 1
            Else
                'otherwise move a column right
                columnNo += 1
            End If
        Next

    End Sub

End Class