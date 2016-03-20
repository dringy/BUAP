Imports System.Data
Imports System.Data.SqlClient
Public Class MapList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PageNumber As Integer
        'Page Number is extracted from querystring, if the querystring isn't specified or is invalid, pagenumber defaults to 1
        If IsNumeric(Request.QueryString("page")) Then
            PageNumber = CInt(Request.QueryString("page"))
        Else
            PageNumber = 1
        End If

        If PageNumber < 1 Then
            PageNumber = 1
        End If

        Using sqlConn As SqlConnection = New SqlConnection(GetSQLConnectionString)
            sqlConn.Open()
            'The top x rows of data are taken from map when ordered by Date of cration
            'X in this case is ((PageNumber * 10) + 1), the PageNumber * 10 is from the 10 elements per page idea, that way it will always get the right data
            'the +1 is so we can tell if there is more data after the page data
            Dim Command As SqlCommand = New SqlCommand("Select Top " & ((PageNumber * 10) + 1) & "* from Map order by DateOfCreation", sqlConn)
            Dim dr As SqlDataReader = Command.ExecuteReader()

            For i As Integer = 1 To (PageNumber - 1) * 10
                'This is to skip through row data on previous page
                'for example if we are on page 2, we read through 10 elements so we can start from row 11
                dr.Read()
            Next

            Dim MapList As New List(Of Map) 'List of map objects

            For i As Integer = 1 To 10
                If dr.Read() Then
                    'For up to 10 remaining elements create a map object and populate the object with MapID, DateOfCreation and isActive
                    Dim MapElement As Map = New Map
                    MapElement.populateClass(dr.GetInt32(0), dr.GetDateTime(1), dr.GetBoolean(2))
                    'Object is added to list
                    MapList.Add(MapElement)
                End If
            Next

            If MapList.Count = 0 And PageNumber > 1 Then
                'If there are no maps and we aren't on page 1, redirect to page 1
                Response.Redirect("/MapList.aspx?page=1")
            End If

            Dim anotherPage As Boolean = False 'Represents if there are more maps after this

            If dr.Read() Then
                'If there is at least another row we know there are more maps after the ones to be displayed
                anotherPage = True
            End If

            If PageNumber > 1 Then
                'If the page number is greater than 1 display a previous page hyperlink
                backbutton.InnerHtml = "Previous Page"
                'Give hyperlink URL of MapList with the pagenumber 1 less than this page
                backbutton.HRef = "/MapList.aspx?page=" & PageNumber - 1
            End If
            If anotherPage = True Then
                'If there is more map data display a next page hyperlink
                nextbutton.InnerHtml = "Next Page"
                'Give hyperlink URL of MapList with the pagenumber 1 more than this page
                nextbutton.HRef = "/MapList.aspx?page=" & PageNumber + 1
            End If

            ASPmaplist.InnerHtml = "" 'Set to blank
            Dim LeftSide As Boolean = True 'LeftSide represents if the element we are placing on the screen is on the left or not

            For i As Integer = 0 To MapList.Count - 1
                'Loop through each map
                If LeftSide = True Then
                    'If this element is to be on the left start a new row
                    ASPmaplist.InnerHtml += "<div class=""clearaway"">"
                End If
                'Fetch DateOfCreation from the map and convert it to string in the format dd/mm/yyyy
                Dim DisplayDate As String = MapList(i).DateOfCreation.ToString("dd/MM/yyyy")
                If MapList(i).isActive = True Then
                    'If the map is active, put the MapId and Display Date on screen in green (Using class activemap)
                    'make it a hyperlink that links to MapProfile, the mapId is passed to this site as a querystring
                    ASPmaplist.InnerHtml += "<a class=""activemap"" href=""MapProfile.aspx?MapID=" & MapList(i).MapID & """>" & MapList(i).MapID & "</a>"
                    ASPmaplist.InnerHtml += "<a class=""activemap"" href=""MapProfile.aspx?MapID=" & MapList(i).MapID & """>" & DisplayDate & "</a>"
                Else
                    'If the map is not active, put the MapId and Display Date on screen in orange (Using class inactivemap)
                    'make it a hyperlink that links to MapProfile, the mapId is passed to this site as a querystring
                    ASPmaplist.InnerHtml += "<a class=""inactivemap"" href=""MapProfile.aspx?MapID=" & MapList(i).MapID & """>" & MapList(i).MapID & "</a>"
                    ASPmaplist.InnerHtml += "<a class=""inactivemap"" href=""MapProfile.aspx?MapID=" & MapList(i).MapID & """>" & DisplayDate & "</a>"
                End If
                If LeftSide = False Then
                    'If we are on the right side, close the row and set the next element to be at the left side
                    ASPmaplist.InnerHtml += "</div>"
                    LeftSide = True
                Else
                    'If we are on the left side, set the next element to be at the right side (by saying it is not on the left)
                    LeftSide = False
                End If
            Next

            If LeftSide = True Then
                'If we ended on the left side, we still need to end the row
                ASPmaplist.InnerHtml += "</div>"
            End If


        End Using
    End Sub
    Private Function GetSQLConnectionString() As String
        Dim SQLdbConn As SQLdbConnection = New SQLdbConnection()
        Return SQLdbConn.getSQLConnectionString()
    End Function
End Class