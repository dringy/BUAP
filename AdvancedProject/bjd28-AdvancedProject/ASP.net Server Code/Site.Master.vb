Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim URL As String = Request.Url.ToString
        If URL.Contains("/PlayerList.aspx") Then
            'If we are on PlayerList.aspx then make the PlayerList hyperlink solid green
            masterRightSideText.InnerHtml = "<a class=""solidG"" href=""PlayerList.aspx"">Players</a><br /><a class=""Ghover"" href=""MapList.aspx"">Maps</a>"
        ElseIf URL.Contains("/MapList.aspx") Then
            'If we are on MapList.aspx then make the MapList hyperlink solid green
            masterRightSideText.InnerHtml = "<a class=""Ghover"" href=""PlayerList.aspx"">Players</a><br /><a class=""solidG"" href=""MapList.aspx"">Maps</a>"
        End If

    End Sub

End Class