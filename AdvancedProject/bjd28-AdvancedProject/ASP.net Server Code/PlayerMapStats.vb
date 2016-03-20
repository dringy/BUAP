Imports System.Data
Imports System.Data.SqlClient
Public Class PlayerMapStats
    Inherits SQLdbConnection
    Private PlayerMapID As Integer 'Unique ID
    Public Player As Player 'Player object
    'List of PvP stats
    Public PvPKills As List(Of PvP)
    Public PvPDeaths As List(Of PvP)
    'List of deaths
    Public Deaths As List(Of Death)
    'Mob Kill Count
    Public CaveSpiderKills As Integer
    Public EndermanKills As Integer
    Public SpiderKills As Integer
    Public WolfKills As Integer
    Public ZombiePigmanKills As Integer
    Public BlazeKills As Integer
    Public CreeperKills As Integer
    Public GhastKills As Integer
    Public MagmaCubeKills As Integer
    Public SilverfishKills As Integer
    Public SkeletonKills As Integer
    Public SlimeKills As Integer
    Public WitchKills As Integer
    Public WitherSkeletonKills As Integer
    Public ZombieKills As Integer
    Public WitherKills As Integer
    'As the Ender Dragon Mob can only be killed once a boolean variable is good enough to represent
    Public EnderDragonKilled As Boolean
    Sub New()
        'Create new instances
        PvPKills = New List(Of PvP)
        PvPDeaths = New List(Of PvP)
        Deaths = New List(Of Death)
    End Sub
    Public Function GetStats(ByRef PlayerID As Integer, ByRef MapID As Integer)
        Using sqlconn As SqlConnection = New SqlConnection(GetSQLConnectionString)
            sqlconn.Open()
            'Fetch Player Map Stats using PlayerID and MapID
            Dim Command As SqlCommand = New SqlCommand("Select * from PlayerMapStats where MapID = " & MapID & " and PlayerID = " & PlayerID, sqlconn)
            Dim dr As SqlDataReader = Command.ExecuteReader()
            If dr.Read() Then
                'Extract PlayerMapID
                PlayerMapID = dr.GetInt32(2)
                'Extract Mob Kill Count
                CaveSpiderKills = dr.GetInt32(3)
                EndermanKills = dr.GetInt32(4)
                SpiderKills = dr.GetInt32(5)
                WolfKills = dr.GetInt32(6)
                ZombiePigmanKills = dr.GetInt32(7)
                BlazeKills = dr.GetInt32(8)
                CreeperKills = dr.GetInt32(9)
                GhastKills = dr.GetInt32(10)
                MagmaCubeKills = dr.GetInt32(11)
                SilverfishKills = dr.GetInt32(12)
                SkeletonKills = dr.GetInt32(13)
                SlimeKills = dr.GetInt32(14)
                WitchKills = dr.GetInt32(15)
                WitherSkeletonKills = dr.GetInt32(16)
                ZombieKills = dr.GetInt32(17)
                WitherKills = dr.GetInt32(18)
                'Extract Ender Dragon Boolean
                EnderDragonKilled = dr.GetBoolean(19)
            Else
                Return False
            End If
            dr.Close()
            'Retrieve all victim player names from all PvP logs of this Player Map Stats
            Command = New SqlCommand("Select PvP.PvPDate, P.Name from PvP Join PlayerMapStats ps On PvP.VictimMapID=ps.PlayerMapID" &
                                     " Join Player p On ps.PlayerID=p.PlayerID where KillerMapID = " &
                                     PlayerMapID, sqlconn)
            dr = Command.ExecuteReader
            While dr.Read()
                'For each victim create a new instance of PvP using player name and date of kill and add it to a list
                Dim PvPKill As New PvP(dr.GetString(1), dr.GetDateTime(0))
                PvPKills.Add(PvPKill)
            End While
            dr.Close()
            'Retrieve all killer player names from all PvP logs of this Player Map St
            Command = New SqlCommand("Select PvP.PvPDate, P.Name from PvP Join PlayerMapStats ps On PvP.KillerMapID=ps.PlayerMapID" &
                                     " Join Player p On ps.PlayerID=p.PlayerID where VictimMapID = " &
                                     PlayerMapID, sqlconn)
            dr = Command.ExecuteReader
            While dr.Read()
                'For each killer create a new instance of PvP using player name and date of kill and add it to a list
                Dim PvPDeath As New PvP(dr.GetString(1), dr.GetDateTime(0))
                PvPDeaths.Add(PvPDeath)
            End While
            dr.Close()
            'Get all the data about the player from the database
            Command = New SqlCommand("Select * from Player where PlayerID = " & PlayerID, sqlconn)
            dr = Command.ExecuteReader
            If dr.Read() Then
                'Create a new player object with all the player information from the database
                Player = New Player
                Player.populate(dr.GetInt32(0), dr.GetString(1), dr.GetDateTime(2), dr.GetBoolean(3))
            Else
                Return False
            End If
            dr.Close()
            'Get all the death logs for this player map stats
            Command = New SqlCommand("Select DeathLog, DeathDate from Death where PlayerMapID = " & PlayerMapID, sqlconn)
            dr = Command.ExecuteReader
            While dr.Read()
                'Create a new death object using the Log and Date then add it to a list.
                Dim DeathStat As New Death(dr.GetString(0), dr.GetDateTime(1))
                Deaths.Add(DeathStat)
            End While
            sqlconn.Close()
            Return True
        End Using
    End Function
End Class
