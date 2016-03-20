Imports System.Data
Imports System.Data.SqlClient
Public Class Map
    Inherits SQLdbConnection
    Public MapID As Integer
    Public DateOfCreation As Date
    Public isActive As Boolean
    Public MapPlayers As New List(Of Player)

    Sub New()

    End Sub
    Sub getActiveMap()
        Using sqlConn As SqlConnection = New SqlConnection(GetSQLConnectionString)
            sqlConn.Open()
            'Map data for the active map is taken from the database 
            Dim Command As SqlCommand = New SqlCommand("Select * from Map where isActive = 1", sqlConn)
            Dim dr As SqlDataReader = Command.ExecuteReader()

            If dr.Read() Then
                MapID = dr.GetInt32(0) 'MapID is extracted
                DateOfCreation = dr.GetDateTime(1) 'DateOfCreation is extracted
                isActive = dr.GetBoolean(2) 'isActive is extracted
            End If
            sqlConn.Close()
        End Using
    End Sub
    Public Function getMapData(MapID)
        Using sqlConn As SqlConnection = New SqlConnection(GetSQLConnectionString)
            sqlConn.Open()
            'Map data for the active map is taken from the database
            Dim Command As SqlCommand = New SqlCommand("Select * From Map where MapID=" & MapID, sqlConn)
            Dim dr As SqlDataReader = Command.ExecuteReader()

            If dr.Read() Then
                Me.MapID = MapID 'MapID is extracted
                Me.DateOfCreation = dr.GetDateTime(1) 'DateOfCreation is extracted
                Me.isActive = dr.GetBoolean(2) 'isActive is extracted
            Else
                Return False
            End If
            dr.Close()
            'Data is taken from Player & PlayerMapStats
            Command = New SqlCommand("Select P.PlayerID, P.Name, P.IsOnline from PlayerMapStats ps Join Player p ON ps.PlayerID = p.PlayerID where ps.MapID=" &
                                     MapID & " order by P.Name", sqlConn)
            dr = Command.ExecuteReader()

            While dr.Read()
                'Create a Player Object and populate it with PlayerID, Name and Boolean: IsOnline
                Dim PlayerElement As New Player
                PlayerElement.pupulateLink(dr.GetInt32(0), dr.GetString(1), dr.GetBoolean(2))
                'Add object to list
                MapPlayers.Add(PlayerElement)
            End While
            Return True
            sqlConn.Close()
        End Using
    End Function
    Sub populateClass(ByVal MapId As Integer, ByVal DateOfCreation As Date, ByVal isActive As Boolean)
        'Setter for MapID, DateOfCreation & isActive
        Me.MapID = MapId
        Me.DateOfCreation = DateOfCreation
        Me.isActive = isActive
    End Sub
End Class
