Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
'Imports System.Web.Configuration
Public Class DAL
    Private Shared m_ConnectionString As String
    Private Const ecsUseTestDB As Boolean = False
    Private Const pcnsServerDateFormat = "yyyy-mm-dd hh:nn:ss"

    Public Sub New(ByVal strConnectionString As String)
        MyBase.New()
        m_ConnectionString = strConnectionString
        If InStr(m_ConnectionString, "@#$") > 0 Then m_ConnectionString = Replace(m_ConnectionString, "@#$", "dudi1$")

    End Sub
    Shared Function ExecuteSelectCommand(ByVal sqlCommand As String, ByVal strConString As String, ByVal strGUID As String) As DataTable
        If ecsUseTestDB Then strConString = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        If StrComp(strGUID, "") > 0 Then
            Dim conn As SqlConnection
            Dim comm As SqlCommand
            ' The DataTable to be returned
            Dim table As DataTable

            ' Execute the command, making sure the connection gets closed in the end
            conn = New SqlConnection(strConString)
            Try
                ' Open the data connection
                conn.Open()
                comm = New SqlCommand(sqlCommand, conn)
                ' Execute the command and save the results in a DataTable
                Dim reader As SqlDataReader
                reader = comm.ExecuteReader()
                table = New DataTable()
                table.Load(reader)
                ' Close the reader
                reader.Close()
            Catch ex As Exception
                '  Utilities.LogError(ex);
                Throw ex
            Finally
                ' Close the connection
                conn.Close()
            End Try
            Return table
        Else
            Return Nothing
        End If

    End Function

    Shared Function ExecuteSqlRetDataTable_Login(ByVal strConnection As String, ByVal sqlCommand As String) As DataTable
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Login_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        ''Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        If ecsUseTestDB Then strConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim stConnection As String = strConnection
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand
        Dim table As DataTable

        Try
            ' Open the data connection
            conn.Open()
            comm = New SqlCommand(sqlCommand, conn)
            ' Execute the command and save the results in a DataTable
            Dim reader As SqlDataReader
            reader = comm.ExecuteReader()
            table = New DataTable()
            table.Load(reader)
            ' Close the reader
            reader.Close()
        Catch ex As Exception
            '  Utilities.LogError(ex);
            Throw ex

        Finally

            ' Close the connection
            conn.Close()
        End Try
        Return table
    End Function

    Shared Function ExecuteNonQuery(ByVal strConnection As String, ByVal sqlCommand As String) As Integer
        Dim stConnection As String

        '  stConnection = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        ' If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        stConnection = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"

        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand


        ' The number of affected rows
        Dim affectedRows As Integer = -1
        Try
            ' Open the data connection
            conn.Open()
            comm = New SqlCommand(sqlCommand, conn)
            ' Execute the command and get the number of affected rows
            affectedRows = comm.ExecuteNonQuery()


        Catch ex As Exception

            ' Log eventual errors and rethrow them
            'Utilities.LogError(ex);
            Throw ex

        Finally
            ' Close the connection
            conn.Close()
        End Try
        ' return the number of affected rows
        Return affectedRows
    End Function

    Shared Function ExecuteNonQueryForGetId(ByVal strConnection As String, ByVal sqlCommand As String, ByRef _ID As Integer) As Integer
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand

        Dim query2 As String = "Select @@Identity"


        ' The number of affected rows
        Dim affectedRows As Integer = -1
        Try
            ' Open the data connection
            conn.Open()
            comm = New SqlCommand(sqlCommand, conn)
            ' Execute the command and get the number of affected rows
            affectedRows = comm.ExecuteNonQuery()

            comm.CommandText = query2

            _ID = comm.ExecuteScalar()



        Catch ex As Exception

            ' Log eventual errors and rethrow them
            'Utilities.LogError(ex);
            Throw ex

        Finally
            ' Close the connection
            conn.Close()
        End Try
        ' return the number of affected rows
        Return affectedRows
    End Function
    Shared Function FixDateString(ByVal stDate As String) As String
        'Convert Dates from 08051971 to 08/05/1971
        Return Left(stDate, 2) & "/" & Mid(stDate, 3, 2) & "/" & Right(stDate, 4)
    End Function


    Shared Function GetFieldValCount(ByVal strConnection As String, ByVal stSQL As String) As Integer
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand = Nothing
        Dim retValue As Integer = 0
        Try
            comm = New SqlCommand(stSQL, conn)
            conn.Open()
            'Return Null Table
            retValue = System.Convert.ToString(comm.ExecuteScalar())
            retValue = Val(retValue.ToString)
        Catch ex As Exception
            'LogWriter.WriteToLog(ex.ToString())
            Throw ex
        Finally
            If Not conn Is Nothing Then
                conn.Close()
            End If
        End Try

        Return retValue
    End Function

    Shared Function GetFieldValEmptyTbl(ByVal strConnection As String, ByVal stSQL As String, ByVal strDefVal As String, Optional ByVal bOnlyNumbers As Boolean = True) As String
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand = Nothing
        Dim retValue As String = ""
        Try
            comm = New SqlCommand(stSQL, conn)
            conn.Open()
            'Return Null Table
            retValue = System.Convert.ToString(comm.ExecuteScalar())
            ' If Not retObj Is Nothing Then
            'retValue = retObj.ToString
            If bOnlyNumbers Then
                If retValue = "" Then
                    retValue = Val(strDefVal)
                Else
                    retValue = Val(retValue.ToString)
                End If
            End If
            '  End If

        Catch ex As Exception
            'LogWriter.WriteToLog(ex.ToString())
            Throw ex
        Finally
            If Not conn Is Nothing Then
                conn.Close()
            End If
        End Try

        Return retValue
    End Function

    Shared Function GetFieldVal(ByVal strConnection As String, ByVal stSQL As String, Optional ByVal bOnlyNumbers As Boolean = True) As String
        'GetFieldVal
        'Dim stConnection = ConfigurationManager.ConnectionStrings("ToToConString").ToString
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"

        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand = Nothing
        Dim retObj As Object
        Dim retValue As String = ""
        Try
            comm = New SqlCommand(stSQL, conn)
            conn.Open()
            retObj = comm.ExecuteScalar().ToString
            If Not retObj Is Nothing Then
                retValue = retObj.ToString
                If bOnlyNumbers Then
                    retValue = Val(retValue.ToString)
                End If
            End If

        Catch ex As Exception
            'LogWriter.WriteToLog(ex.ToString())
            Throw ex
        Finally
            If Not conn Is Nothing Then
                conn.Close()
            End If
        End Try

        Return retValue
    End Function
    Shared Function FixSQLDate(ByVal stDate As String) As Object
        Dim oRet As Object
        If stDate.Length = 6 Then
            stDate = "01" + stDate
        End If
        If Val(stDate) = 0 Then
            oRet = DBNull.Value
        Else
            oRet = Mid(stDate, 1, 2) & "/" & Mid(stDate, 3, 2) & "/" & Mid(stDate, 5)
        End If
        Return oRet
    End Function
    Shared Function GetSqlDataAsDataSet(ByVal strConnection As String, ByVal strSql As String) As DataSet
        'Using sb sample
        'Dim sb As StringBuilder = New StringBuilder()
        '    sb.Append("SELECT Raiders.Fname + ' ' + Raiders.Pname AS Name, Raiders.IDnum, Raiders.CarLicenseNo, Raiders.PolicyNo, ")
        '    sb.Append("Packages.packageName, Raiders.RaiderPrice, CONVERT(varchar, Raiders.FromDate, 103) AS FromDate, ")
        'Dim strSql As String = sb.ToString()
        'Dim stConnection As String = WebConfigurationManager.ConnectionStrings("RaiderConnectionString").ConnectionString
        '-----------------------------------------

        'Dim stConnection = ConfigurationManager.ConnectionStrings("ToToConString").ToString
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim dsResult As DataSet = New DataSet()
        Dim da As SqlDataAdapter = Nothing
        Try
            da = New SqlDataAdapter(strSql, conn)
            da.SelectCommand.CommandType = CommandType.Text
            'reader.SelectCommand.CommandType = CommandType.Text
            da.Fill(dsResult, "reportTable")
        Catch ex As Exception
            'LogWriter.WriteToLog(ex.ToString())
            Throw ex
        Finally
            If Not conn Is Nothing Then
                conn.Close()
                conn = Nothing
            End If
            'If Not (IsNull  da) Then
            da.Dispose()
            'End If
        End Try

        Return dsResult
    End Function
    Shared Function GetSqlDataAsDataTabel(ByVal strConnection As String, ByVal strSql As String, Optional strSpNM As String = "") As DataTable
        'Dim stConnection = ConfigurationManager.ConnectionStrings("ToToConString").ToString
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim blnUseSP As Boolean = False
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim myDataTabel As New DataTable
        Dim da As SqlDataAdapter = Nothing
        If strSpNM.Length > 0 Then
            strSql = strSpNM
            blnUseSP = True
        End If
        Try
            da = New SqlDataAdapter(strSql, conn)
            If blnUseSP Then
                da.SelectCommand.CommandType = CommandType.StoredProcedure
            Else
                da.SelectCommand.CommandType = CommandType.Text
            End If

            da.Fill(myDataTabel)
            If Not myDataTabel Is Nothing Then
                myDataTabel.TableName = "ShagrirData"
            End If
        Catch ex As Exception
            'LogWriter.WriteToLog(ex.ToString())
            Throw ex
        Finally
            If Not conn Is Nothing Then
                conn.Close()
            End If
            'If Not (IsNull  da) Then
            da.Dispose()
            'End If
        End Try
        Return myDataTabel
    End Function
    Private Function BindGrid(ByVal strSql As String) As SqlDataReader
        'Dim stConnection = ConfigurationManager.ConnectionStrings("ToToConString").ToString
        'Dim stConnection As String = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = m_ConnectionString
        Dim objConn As SqlConnection = New SqlConnection(stConnection)
        Dim objCmd As SqlCommand = New SqlCommand(strSql, objConn)
        Dim myDataTabel As New DataTable
        Dim DR As SqlDataReader = Nothing
        Dim blnOk As Boolean = False
        objCmd.CommandType = CommandType.StoredProcedure
        Try
            objConn.Open()
            DR = objCmd.ExecuteReader()
            'gvEmployees.DataSource = DR
            'gvEmployees.DataBind()
            blnOk = True
        Catch ex As Exception

        Finally
            DR.Close()
            DR = Nothing
            objCmd.Dispose()
            objCmd = Nothing
            objConn.Close()
            objConn = Nothing
        End Try
        If blnOk Then
            Return DR
        Else
            Return Nothing
        End If
    End Function
    Shared Function ExecuteSqlRetDataTable(ByVal strConnection As String, ByVal sqlCommand As String) As DataTable
        'Dim stConnection As String
        'stConnection = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand
        Dim table As DataTable

        Try
            ' Open the data connection
            conn.Open()
            comm = New SqlCommand(sqlCommand, conn)
            ' Execute the command and save the results in a DataTable
            Dim reader As SqlDataReader
            reader = comm.ExecuteReader()
            table = New DataTable()
            table.Load(reader)
            ' Close the reader
            reader.Close()

            If Not table Is Nothing Then
                table.TableName = "ShagrirData"
            End If
        Catch ex As Exception
            '  Utilities.LogError(ex);
            Throw ex


        Finally

            ' Close the connection
            conn.Close()
        End Try
        Return table
    End Function
    ' execute a select command and return a single result as a string
    Shared Function ExecuteScalar(ByVal strConnection As String, ByVal strSql As String) As String
        'Dim stConnection = ConfigurationManager.ConnectionStrings("ToToConString").ToString
        'Dim stConnection As String

        'stConnection = System.Web.HttpContext.Current.Application("Database_ConnectionString")
        'If InStr(stConnection, "@#$") > 0 Then stConnection = Replace(stConnection, "@#$", "comtec1$")
        Dim stConnection As String = strConnection
        If ecsUseTestDB Then stConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim conn As SqlConnection = New SqlConnection(stConnection)
        Dim comm As SqlCommand = Nothing
        ' The value to be returned
        Dim retValue As String = ""

        ' Execute the command making sure the connection gets closed in the end
        Try

            ' Open the connection of the command
            comm = New SqlCommand(strSql, conn)
            conn.Open()
            ' Execute the command and get the number of affected rows
            retValue = comm.ExecuteScalar().ToString()

        Catch ex As Exception
        Finally

            ' Close the connection
            If Not conn Is Nothing Then
                conn.Close()
            End If
        End Try
        ' return the result
        Return retValue

    End Function
    Shared Function InsertParameters(ByVal Sql As String, ByVal ParamArray Params() As Object) As String
        Dim var As Object
        InsertParameters = Sql
        For Each var In Params
            InsertParameters = Replace(InsertParameters, "?", CSql(var), , 1, vbTextCompare)
        Next var
    End Function
    Shared Function CSql(ByVal Value As Object, Optional ByVal ToType As VariantType = vbEmpty, Optional ByVal DataBaseType As Integer = 0) As String
        '----------------------------------------------------------
        ' Purpose      : Convert a value to it's string format in SQL statment
        ' Accepts      : Any Value , VB type to convert to in SQL,DataBaseType -  0(Oracle), 1(Access)
        ' Returns      : A string value in SQL format

        '----------------------------------------------------------


        Dim theType As Integer

        CSql = ""

        If ToType = vbEmpty Then
            theType = VarType(Value)
        ElseIf IsNothing(Value) Then
            theType = vbNull
        ElseIf Value = vbNullString Then
            theType = vbString
        Else
            theType = ToType
        End If

        Select Case theType
            Case vbNull, vbEmpty
                CSql = "Null"
            Case vbString
                CSql = "'" & Replace(Value, "'", "''", 1, , vbTextCompare) & "'"
            Case vbDate
                If IsNothing(Value) Or (Not IsDate(Value)) Then
                    CSql = "NULL"
                Else
                    'If DataBaseType = 1 Then
                    '    CSql = "#" & Format(Value, "dd-MM-yyyy") & "#"
                    'ElseIf DataBaseType = 0 Then
                    '    'CSql = Format$(Value, pcnsAccessDateFormat)
                    '    CSql = "#" & Format(Value, "dd-MM-yyyy") & "#"
                    'End If
                    ' CSql = " { ts '" & Format$(Value, pcnsServerDateFormat) & "'}"
                    'CSql = " to_date('" & Value & "','dd/mm/yyyy hh24:mi:ss')"
                    CSql = " Convert(DateTime,'" & Value & "', 103)"
                End If
            Case vbLong
                CSql = CStr(Value)
            Case Else
                CSql = CStr(Value)
        End Select
        Return CSql
    End Function
    Shared Function Nz(ByRef Value As Object, Optional ByRef ValueIfNull As Object = Nothing) As Object
        If IsDBNull(Value) Or IsNothing(Value) Then
            If IsNothing(ValueIfNull) Then
                If VarType(Value) = VariantType.String Then
                    Nz = ""
                Else
                    Nz = 0
                End If
            Else
                Nz = ValueIfNull
            End If
        ElseIf Value.ToString = "" Then
            If IsNothing(ValueIfNull) Then
                If VarType(Value) = VariantType.String Then
                    Nz = ""
                Else
                    Nz = 0
                End If
            Else
                Nz = ValueIfNull
            End If
        Else

            Nz = Value
        End If

        Exit Function
    End Function
    Shared Function InsertNewRecordToTable(ByVal strConnection As String, FieldNextVal As String, strNameTable As String, strNameFields As String, strValues As String) As Long
        ' Purpose      : insert new record into table
        '                given the all fileds you be pleased to insert and values
        '                put then recid first in strNameFields
        '                if recid next val do not declaration val recid in strValues
        ' return       : new rec id

        Dim lngNewRecID As Long
        Dim blretval As Boolean
        Dim strSql As String
        Dim lngAffRows As Long
        Dim lngRetVal As Long
        If ecsUseTestDB Then strConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        'New Order Seq must be created
        lngNewRecID = GetSequenceNextVal(strConnection, FieldNextVal, strNameTable)
        blretval = IfExistingReturnRecord(strConnection, "REC_ID", "order_call", " rec_id = " & lngNewRecID)
        If Not blretval Then
            strValues = lngNewRecID & "," & strValues
            strSql = "INSERT INTO " & strNameTable & "  ( " & strNameFields & ")" & " VALUES  ( " & strValues & " )"
            lngAffRows = ExecuteNonQuery(strConnection, strSql)
        Else
            lngAffRows = -1 'Call exists
        End If

        If lngAffRows > 0 Then
            lngRetVal = lngNewRecID
        Else
            lngRetVal = 0
        End If


        Return lngRetVal

        Exit Function



    End Function
    Shared Function UpdateRecordToTable(ByVal strConnection As String, strNameTable As String, strSetFields As String, strcrit As String) As Boolean
        ' Purpose    : update record to same table
        '                given the all fileds you to insert and values
        '                put then recid first in strNameFields
        If ecsUseTestDB Then strConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Dim strSql As String
        Dim lngRecAff As Long
        Dim blnRetVal As Boolean

        strSql = " UPDATE " & strNameTable & " SET " & strSetFields & " WHERE " & strcrit
        lngRecAff = ExecuteNonQuery(strConnection, strSql)
        If lngRecAff > 0 Then
            blnRetVal = True
        Else
            blnRetVal = False

        End If
        Return blnRetVal
    End Function

    Shared Function GetSequenceNextVal(ByVal strConnection As String, FieldName As String, TableName As String, Optional intBranch As Integer = 1) As Long
        '----------------------------------------------------------
        ' Purpose      : Generate new number for fields that should be automatic incremeted.
        ' Accepts      : Field name, table name
        ' Returns      : The next squenctial number
        '----------------------------------------------------------
        Dim strSql As String
        Dim lngRecId As Long
        If ecsUseTestDB Then strConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"
        Select Case UCase(TableName)

            Case "ORDER_CALL"
                strSql = "SELECT ORDER_CALL_SEQ.NEXTVAL FROM DUAL"
            Case "ALLOTMENT_EXEC"
                strSql = "SELECT ALLOTMENT_EXEC_SEQ.NEXTVAL FROM DUAL"
            Case "SBC"
                strSql = "SELECT SBC_SEQ.NEXTVAL FROM DUAL"
            Case "SBC_VLD"
                strSql = "SELECT SBC_VLD_SEQ.NEXTVAL FROM DUAL"
            Case "PAY_SBC"
                strSql = "SELECT TZROR_SEQ.NEXTVAL FROM DUAL"
            Case "ORDER_CALL_WEB_LOG"
                strSql = "SELECT ORDER_CALL_WEB_LOG_SEQ.NEXTVAL FROM DUAL"
            Case "WEB_SEND_SMS"
                strSql = "SELECT WEB_SEND_SMS_SEQ.NEXTVAL FROM DUAL"
            Case Else
                strSql = "SELECT GENERAL_SEQ.NEXTVAL FROM DUAL"
        End Select

        lngRecId = GetFieldVal(strConnection, strSql)


        Return lngRecId

        Exit Function


    End Function

    Shared Function IfExistingReturnRecord(ByVal strConnection As String, strfileds As String, strTable As String, strcrit As String) As Boolean
        'return the Record in table   IfExistingReturnVal = true
        Dim strSql As String
        Dim strSqlRetVal As String
        Dim blnRetVal As Boolean
        If ecsUseTestDB Then strConnection = "Data Source=TEST;User ID=shagrir;Password=comtec1$;Unicode=True"

        strSql = "SELECT " & strfileds & " From " & strTable & " WHERE " & strcrit
        strSqlRetVal = ExecuteScalar(strConnection, strSql)
        If strSqlRetVal.Length = 0 Then
            blnRetVal = False
        Else
            blnRetVal = True

        End If

        Return blnRetVal

        Exit Function
    End Function
End Class
