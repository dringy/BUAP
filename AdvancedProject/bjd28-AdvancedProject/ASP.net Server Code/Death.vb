Public Class Death
    Public DeathLog As String
    Public DeathDate As Date
    Sub New(ByRef DeathLog As String, ByRef DeathDate As Date)
        Me.DeathLog = DeathLog 'String that contains the death message displayed by the game
        Me.DeathDate = DeathDate 'Date the death occured
    End Sub
End Class
