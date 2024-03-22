Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports System.IO

Public Class Report_Student
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID_Yer As Long = 0
        Dim Dic_Yer = yeartitle.yeartitles("(deleted_yer=0) AND (active_yer=1)").Values
        If Dic_Yer.Count = 0 Then
            Response.Redirect("Index.aspx")
            Exit Sub
        Else
            ID_Yer = Dic_Yer(0).ID_Key
            Session.Add("YearID", ID_Yer)
        End If

        If (Not Me.IsPostBack) Then
            RenewForm()
        End If
    End Sub

    Private Sub RenewForm()
        ErrorBox.Text = ""
        ErrorBox.Visible = False
        sCode.Text = ""
    End Sub

    Protected Sub Cancel_Btn_Click(sender As Object, e As EventArgs)
        Response.Redirect("Index.aspx")
    End Sub

    Protected Sub Print_Btn_Click(sender As Object, e As EventArgs)
        Dim ID_Yer As Long = 0
        Dim Dic_Yer = yeartitle.yeartitles("(deleted_yer=0) AND (active_yer=1)").Values
        If Dic_Yer.Count = 0 Then
            Response.Redirect("Index.aspx")
            Exit Sub
        Else
            ID_Yer = Dic_Yer(0).ID_Key
            Session.Add("YearID", ID_Yer)
        End If

        ErrorBox.Text = ""
        ErrorBox.Visible = False
        If sCode.Text.Trim = "" Then
            ErrorBox.Visible = True
            ErrorBox.Text = "يتوجب إدخال رقم الإستمارة"
            Exit Sub
        End If

        Dim studentID As Long = GetStudentID(sCode.Text.Trim(), ID_Yer)

        If studentID = 0 Then
            ErrorBox.Visible = True
            ErrorBox.Text = "رقم الإستمارة غير صحيح"
            Exit Sub
        End If

        Dim hasDegrees As Boolean = CheckStudentDegrees(studentID, ID_Yer)
        Session("ActStudentDegrees") = If(hasDegrees, "True", "False")

        Dim stu As New student(studentID)
        Session.Add("StudentName", stu.sname_stu.Trim)

        DownloadFile()
    End Sub

    Private Function GetStudentID(ByVal code As String, ByVal yearID As Long) As Long
        Dim studentID As Long = 0
        Dim query As String = "SELECT TOP 1 ID_Key FROM students WHERE LOWER(scode_stu) = @Code AND deleted_Stu = 0 AND id_yer = @YearID"
        Using connection As New SqlConnection("connection_string_here")
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Code", code.ToLower())
                command.Parameters.AddWithValue("@YearID", yearID)
                connection.Open()
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing Then
                    studentID = Convert.ToInt64(result)
                End If
            End Using
        End Using
        Return studentID
    End Function

    Private Function CheckStudentDegrees(ByVal studentID As Long, ByVal yearID As Long) As Boolean
        Dim hasDegrees As Boolean = False
        Dim query As String = "SELECT COUNT(*) FROM degree WHERE id_stu = @StudentID AND active_deg = 1 AND deleted_deg = 0 AND id_yer = @YearID"
        Using connection As New SqlConnection("connection_string_here")
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@StudentID", studentID)
                command.Parameters.AddWithValue("@YearID", yearID)
                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                hasDegrees = (count > 0)
            End Using
        End Using
        Return hasDegrees
    End Function

    Private Sub DownloadFile()
        Dim ID_Yer = Val(Session("YearID"))
        If ID_Yer = 0 Then
            Response.Redirect("Index.aspx")
            Exit Sub
        End If

        Dim ID_Stu As Long = Val(Session("ActStudentID"))

        If ID_Stu = 0 Then
            Response.Redirect("Add_Student.aspx")
        End If

        ' Rest of the method remains unchanged
        ' ...
    End Sub
End Class
