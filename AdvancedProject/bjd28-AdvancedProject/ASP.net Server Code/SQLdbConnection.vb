Public Class SQLdbConnection
    'This is the connection string for my database
    Private SQLConnectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=BathCSAdvancedProject;Integrated Security=True"
    Function getSQLConnectionString() As String
        Return SQLConnectionString
    End Function
End Class
