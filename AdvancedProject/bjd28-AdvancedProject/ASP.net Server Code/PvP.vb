Public Class PvP
    Public PlayerName As String 'Display Player Name of killer/victim
    Public PvPDate As Date 'Display date of instance
    Sub New(ByRef PlayerName As String, ByRef PvPDate As Date)
        'Setter for all fields
        Me.PlayerName = PlayerName
        Me.PvPDate = PvPDate
    End Sub
End Class

