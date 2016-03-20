Public Class Player
    Public PlayerID As Integer 'Unique ID
    Public Name As String 'Player Name
    Public LastOnlineDate As Date 'Last date they were online
    Public IsOnline As Boolean 'Indicates if the user is online
    Sub New()

    End Sub

    Sub populate(ByRef PlayerID As Integer, ByRef Name As String, ByRef LastOnlineDate As Date, ByRef IsOnline As Boolean)
        'Populates all fields
        Me.PlayerID = PlayerID
        Me.Name = Name
        Me.LastOnlineDate = LastOnlineDate
        Me.IsOnline = IsOnline
    End Sub
    Sub pupulateLink(ByRef PlayerID As Integer, ByRef Name As String, ByRef IsOnline As Boolean)
        'Populates all fields except online date
        Me.PlayerID = PlayerID
        Me.Name = Name
        Me.IsOnline = IsOnline
    End Sub
End Class
