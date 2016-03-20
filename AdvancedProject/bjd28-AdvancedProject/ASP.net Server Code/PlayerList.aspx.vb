Imports System.Data
Imports System.Data.SqlClient
Public Class PlayerList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Offline players are only displayed if they were online within two days
        'Date Boundary holds the current date minus 2 days, which has been converted to a string in a format that the SQL database will recognise
        Dim DateBoundary As String = (DateAdd(DateInterval.Day, -2, Date.Now())).ToString("yyyy-MM-dd HH:MM:ss")
        'New Map Object is created and populated with the data of the active map
        Dim ActiveMap As New Map
        ActiveMap.getActiveMap()
        'A list of both online and offline players are created
        Dim OnlinePlayerList As New List(Of Player)
        Dim OfflinePlayerList As New List(Of Player)

        Using sqlConn As SqlConnection = New SqlConnection(GetSQLConnectionString)
            sqlConn.Open()
            'Retireve the PlayerID, Name and IsOnline from the Player Database where the Player is either online or was online within two days.
            'Only players who have played on the currently active map are taken
            'The players are ordered by Last Online Date
            Dim Command As SqlCommand = New SqlCommand("Select P.PlayerID, P.Name, P.LastOnlineDate, P.IsOnline" &
                                                       " from Player P Join PlayerMapStats ps on p.PlayerID = ps.PlayerID where (P.LastOnlineDate > '" & DateBoundary &
                                                       "' or P.IsOnline = 'True') and Ps.MapID = " & ActiveMap.MapID & " order by p.LastOnlineDate", sqlConn)
            Dim dr As SqlDataReader = Command.ExecuteReader()

            While dr.Read()
                'Create new Player object and populates it with name, Last Online Date and the Boolean: IsOnline
                Dim NextPlayer As New Player
                NextPlayer.populate(dr.GetInt32(0), dr.GetString(1), dr.GetDateTime(2), dr.GetBoolean(3))

                If NextPlayer.IsOnline = True Then
                    'If the player is online add player to the online list
                    OnlinePlayerList.Add(NextPlayer)
                Else
                    'If the player is offline add player to the offline list
                    OfflinePlayerList.Add(NextPlayer)
                End If

            End While
            sqlConn.Close()
        End Using


        onlinelist.InnerHtml = "<div class=""clearaway"">"
        If OnlinePlayerList.Count > 0 Then 'Only run code if there is more than 1 online player

            Dim lineposition As Short = 1 'This represents which column the player element is in
            For i As Integer = 0 To OnlinePlayerList.Count - 1
                'For each online player
                If lineposition = 6 Then
                    'If we are on Column 6 of 5 we need to end this row and start a new one
                    onlinelist.InnerHtml += "</div><div class=""clearaway"">"
                    lineposition = 1
                End If
                'Display the player name in green
                'The name is a hyperlink to a player map stats page with the playerID and the active map ID given throught query string
                onlinelist.InnerHtml += (" <a class=""onlineplayers"" href=""/PlayerMapProfile.aspx?PlayerID=" & OnlinePlayerList(i).PlayerID &
                "&MapID=" & ActiveMap.MapID & """>" & OnlinePlayerList(i).Name & "</a>")
            Next
        Else
            'If there are no online players, a No Players message is shown
            onlinelist.InnerHtml += "<div class = ""emptyListMessage"">No Players are currently Online</div>"
        End If
        'Ends online player section
        onlinelist.InnerHtml += "</div>"

        offlinelist.InnerHtml = "<div class=""clearaway"">"
        If OfflinePlayerList.Count > 0 Then 'Only run code if there is more than 1 offline player
            'For each offline player
            Dim lineposition As Short = 1
            For i As Integer = 0 To OfflinePlayerList.Count - 1
                If lineposition = 6 Then
                    'If we are on Column 6 of 5 we need to end this row and start a new one
                    offlinelist.InnerHtml += "</div><div class=""clearaway"">"
                    lineposition = 1
                End If
                'Display the player name in orange
                'The name is a hyperlink to a player map stats page with the playerID and the active map ID given throught query string
                offlinelist.InnerHtml += (" <a class=""offlineplayers"" href=""/PlayerMapProfile.aspx?PlayerID=" & OfflinePlayerList(i).PlayerID &
                "&MapID=" & ActiveMap.MapID & """>" & OfflinePlayerList(i).Name & "</a>")
            Next
        Else
            'If there are no offline players in the date boundary then display an error message
            offlinelist.InnerHtml += "<div class = ""emptyListMessage"">No Recent Offline Players</div>"
        End If
        'close section
        offlinelist.InnerHtml += "</div>"



    End Sub

    Private Function GetSQLConnectionString() As String
        Dim SQLdbConn As SQLdbConnection = New SQLdbConnection()
        Return SQLdbConn.getSQLConnectionString()
    End Function

End Class