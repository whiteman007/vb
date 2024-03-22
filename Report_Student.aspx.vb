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
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('" & "يتوجب إدخال رمز الطالب" & "');", True)
            Exit Sub
        End If
        Dim Dic_Student = student.students("(lower(scode_stu)='" & LCase(sCode.Text.Trim) & "') AND (deleted_Stu=0) AND (id_yer=" & ID_Yer & ")")
        If Dic_Student.Count <= 0 Then
            ErrorBox.Visible = True
            ErrorBox.Text = "رقم الإستمارة غير صحيح"
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('" & "رمز الطالب غير صحيح" & "');", True)
            Exit Sub
        ElseIf Dic_Student.Count > 1 Then
            ErrorBox.Visible = True
            ErrorBox.Text = "يوجب اكثر من طالب بنفس رقم الإستمارة, يرجى مراجعة مركز الحاسبة الالكترونية - شعبة الانظمة والبرامجيات"
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('" & "يوجب اكثر من طالب بنفس الرمز, يرجى مراجعة الإدارة" & "');", True)
            Exit Sub
        End If
        Dim ID_Stu = Dic_Student.Values(0).ID_Key
        ErrorBox.Text = ""
        ErrorBox.Visible = False
        Session.Add("ActStudentID", ID_Stu)

        If degree.degrees("(id_stu=" & ID_Stu & ") AND (active_deg=1) AND (deleted_deg=0) AND (id_yer=" & ID_Yer & ")").Count = 0 Then
            Session("ActStudentDegrees") = "False"
        Else
            Session("ActStudentDegrees") = "True"
        End If

        Dim stu As New student(ID_Stu)
        Session.Add("StudentName", stu.sname_stu.Trim)

        DowloadFile()
    End Sub

    Private Sub DowloadFile()
        Dim ID_Yer = Val(Session("YearID"))
        If ID_Yer = 0 Then
            Response.Redirect("Index.aspx")
            Exit Sub
        End If

        Dim ID_Stu As Long = 0
        ID_Stu = Val(Session("ActStudentID"))

        If ID_Stu = 0 Then
            Response.Redirect("Add_Student.aspx")
        End If

        Dim MyReportViewer As New ReportViewer
        MyReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        MyReportViewer.LocalReport.ReportPath = Server.MapPath("~/StudentPaper.rdlc")

        Dim DSet_1 As DataSet_Report1 = Manager.GetDATA_DS_1("Select choices.id_cho,subjectno_stu,students.sname_stu,students.mothername_stu,students.birthdate_stu,iif(students.gender_stu=1,'ذكر',iif(students.gender_stu=2,'انثى','غير معرف')) as gender_stu,students.tel_stu,students.address_stu,students.citizenship_stu,students.religon_stu,students.school_stu,students.graduate_stu,branches.bname_bch,iif(students.stry_stu=1,'الأول',iif(students.stry_stu=2,'الثاني',iif(students.stry_stu=3,'الثالث',iif(students.stry_stu=4,'الرابع',iif(students.stry_stu=5,'الخامس',iif(students.stry_stu=6,'السادس',iif(students.stry_stu=7,'السابع',iif(students.stry_stu=8,'الثامن',iif(students.stry_stu=9,'التاسع','العاشر'))))))))) as stry_stu,students.sumdegree_stu,students.addeddegree_stu,students.totaldegree_stu,colleges.cname_clg,max(departments.dname_dep) as dname_dep,students.scode_stu,students.examno_stu,students.addeddegreename_stu from choices inner join students on students.id_stu = choices.id_stu inner join branches on branches.id_bch = students.id_bch inner join departments on departments.id_dep = choices.id_dep inner join colleges on colleges.id_clg = departments.id_clg where (choices.id_stu=" & ID_Stu & ") AND (deleted_bch =0) AND (deleted_cho =0) AND (deleted_clg =0) AND (deleted_dep =0) AND (deleted_stu =0) AND (active_bch =1) AND (active_cho =1) AND (active_clg =1) AND (active_dep =1) AND (active_stu =1) AND (colleges.id_yer=" & ID_Yer & ") AND (departments.id_yer=" & ID_Yer & ") AND (branches.id_yer=" & ID_Yer & ") AND(students.id_yer=" & ID_Yer & ") AND (choices.id_yer=" & ID_Yer & ") group by subjectno_stu,students.sname_stu,students.mothername_stu,students.birthdate_stu,students.gender_stu,students.tel_stu,students.address_stu,students.citizenship_stu,students.religon_stu,students.school_stu,students.graduate_stu,branches.bname_bch,students.stry_stu,students.sumdegree_stu,students.addeddegree_stu,students.totaldegree_stu,colleges.cname_clg,choices.id_cho,students.scode_stu,students.examno_stu,students.addeddegreename_stu order by choices.id_cho", "Choices_Table")
        Dim MaxID As Long = CLng(DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("id_cho"))
        For i As Integer = 1 To 10 - DSet_1.Tables("Choices_Table").Rows.Count
            Dim ArrayOfData() As Object = Nothing
            ReDim Preserve ArrayOfData(0) : ArrayOfData(0) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("sname_stu")
            ReDim Preserve ArrayOfData(1) : ArrayOfData(1) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("mothername_stu")
            ReDim Preserve ArrayOfData(2) : ArrayOfData(2) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("birthdate_stu")
            ReDim Preserve ArrayOfData(3) : ArrayOfData(3) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("gender_stu")
            ReDim Preserve ArrayOfData(4) : ArrayOfData(4) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("tel_stu")
            ReDim Preserve ArrayOfData(5) : ArrayOfData(5) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("address_stu")
            ReDim Preserve ArrayOfData(6) : ArrayOfData(6) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("citizenship_stu")
            ReDim Preserve ArrayOfData(7) : ArrayOfData(7) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("religon_stu")
            ReDim Preserve ArrayOfData(8) : ArrayOfData(8) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("school_stu")
            ReDim Preserve ArrayOfData(9) : ArrayOfData(9) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("graduate_stu")
            ReDim Preserve ArrayOfData(10) : ArrayOfData(10) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("bname_bch")
            ReDim Preserve ArrayOfData(11) : ArrayOfData(11) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("stry_stu")
            ReDim Preserve ArrayOfData(12) : ArrayOfData(12) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("sumdegree_stu")
            ReDim Preserve ArrayOfData(13) : ArrayOfData(13) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("addeddegree_stu")
            ReDim Preserve ArrayOfData(14) : ArrayOfData(14) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("totaldegree_stu")
            ReDim Preserve ArrayOfData(15) : ArrayOfData(15) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("cname_clg")
            ReDim Preserve ArrayOfData(16) : ArrayOfData(16) = "##" 'DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("dname_dep")
            ReDim Preserve ArrayOfData(17) : ArrayOfData(17) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("scode_stu")
            ReDim Preserve ArrayOfData(18) : ArrayOfData(18) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("subjectno_stu")
            ReDim Preserve ArrayOfData(19) : ArrayOfData(19) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("addeddegreename_stu")
            ReDim Preserve ArrayOfData(20) : ArrayOfData(20) = DSet_1.Tables("Choices_Table").Rows(DSet_1.Tables("Choices_Table").Rows.Count - 1).Item("examno_stu")
            MaxID += 1
            ReDim Preserve ArrayOfData(21) : ArrayOfData(21) = MaxID
            DSet_1.Tables("Choices_Table").Rows.Add(ArrayOfData)
        Next

        Dim DSet_2 As DataSet_Report2
        If Session("ActStudentDegrees") = "True" Then
            DSet_2 = Manager.GetDATA_DS_2(True, "Select 1 as WithDegree, subjects.id_sbj,subjects.sname_sbj,degrees.degree_deg,'" & New yeartitle(ID_Yer).yeartitle_yer.Trim & "' as YearTitle,students.addeddegree_stu as AddedDegree from choices inner join students on students.id_stu = choices.id_stu inner join branches on branches.id_bch = students.id_bch inner join subjects on subjects.id_bch = students.id_bch inner join degrees on ((degrees.id_stu = students.id_stu) AND (degrees.id_sbj  = subjects.id_sbj )) where (students.id_stu=" & ID_Stu & ") AND (deleted_bch =0) AND (deleted_cho =0) AND (deleted_stu =0) AND (deleted_sbj =0) AND (active_bch =1) AND (active_cho =1) AND (active_stu =1) AND (active_sbj =1) AND (subjects.id_yer=" & ID_Yer & ") AND (branches.id_yer=" & ID_Yer & ") AND(students.id_yer=" & ID_Yer & ") AND (choices.id_yer=" & ID_Yer & ") AND (degrees.active_deg =1) AND (degrees.deleted_deg=0) AND (degrees.id_yer=" & ID_Yer & ") group by subjects.id_sbj,subjects.sname_sbj,degrees.degree_deg,students.addeddegree_stu order by subjects.id_sbj", "Subjects_Table")
        Else
            DSet_2 = Manager.GetDATA_DS_2(False, "Select 0 as WithDegree, subjects.id_sbj,subjects.sname_sbj,'" & New yeartitle(ID_Yer).yeartitle_yer.Trim & "' as YearTitle from choices inner join students on students.id_stu = choices.id_stu inner join branches on branches.id_bch = students.id_bch inner join subjects on subjects.id_bch = students.id_bch where (students.id_stu=" & ID_Stu & ") AND (deleted_bch =0) AND (deleted_cho =0) AND (deleted_stu =0) AND (deleted_sbj =0) AND (active_bch =1) AND (active_cho =1) AND (active_stu =1) AND (active_sbj =1) AND (subjects.id_yer=" & ID_Yer & ") AND (branches.id_yer=" & ID_Yer & ") AND(students.id_yer=" & ID_Yer & ") AND (choices.id_yer=" & ID_Yer & ") group by subjects.id_sbj,subjects.sname_sbj order by subjects.id_sbj", "Subjects_Table")
        End If

        Dim DSource As ReportDataSource = New ReportDataSource("DataSet_Report1", DSet_1.Tables("Choices_Table"))
        Dim DSource_2 As ReportDataSource = New ReportDataSource("DataSet_Report2", DSet_2.Tables("Subjects_Table"))

        MyReportViewer.LocalReport.DataSources.Clear()
        MyReportViewer.LocalReport.DataSources.Add(DSource)
        MyReportViewer.LocalReport.DataSources.Add(DSource_2)

        Dim filename As String = CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & "_" & CStr(Now.Hour) & CStr(Now.Minute) & CStr(Now.Second) & "_" & ID_Stu & ".pdf"

        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim bytes As Byte() = MyReportViewer.LocalReport.Render("PDF", "", mimeType, encoding, extension, streamids, warnings)
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-disposition", "attachment; filename=" + filename)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Session("ActStudentDegrees") = ""
        Session.Remove("ActStudentDegrees")

        Session("CurrentStudentFileName") = ""
        Session.Remove("CurrentStudentFileName")

        Session("ActStudentDegrees") = ""
        Session.Remove("ActStudentDegrees")

        Session("StudentName") = ""
        Session.Remove("StudentName")
    End Sub
End Class