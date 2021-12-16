Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
'Imports System.Web.Configuration
Public Class clsDLogin
    Private m_ConnectionString As String
    Private Enum enmShiftTp
        enmStartShift = 1
        enmEndShift = 2
        enmStartBreak = 3
        enmEndBreak = 4
        enmStartShiftment = 91
        enmEndShiftment = 92
        enmStartBreakShiftment = 93
        enmEndBreakShiftment = 94
    End Enum
    Private Enum enmBreakTp
        enmStartBreak = 1
        enmEndBreak = 2

    End Enum
    Public Sub New(ByVal strConnectionString As String)
        MyBase.New()
        m_ConnectionString = strConnectionString
    End Sub

    Public Function CheckIfUserExists(ByVal strConnection As String, ByVal strUserNM As String, strPassword As String) As Long
        Dim lngRetVal As Long
        Dim strSql As String
        Dim stConnection As String = strConnection
        Try
            strSql = "SELECT UserId FROM [WebLet].[Security].[User]   where [UserNm]=? and Password=?"
            strSql = DAL.InsertParameters(strSql, strUserNM, strPassword)

            lngRetVal = DAL.Nz(DAL.GetFieldValEmptyTbl(stConnection, strSql, 0, True), 0)

        Catch ex As Exception
            lngRetVal = -1
        End Try

        Return lngRetVal
    End Function


    Public Function CheckIfSrvManExists(ByVal strConnection As String, ByVal intSrvManNo As Integer) As DataTable
        Dim drRetVal As DataTable
        Dim strSql As String
        Dim stConnection As String = strConnection

        strSql = "select FIRST_NM,LAST_NM,SRV_MAN_TP,PHONE_NO from SRV_MAN where SRV_MAN_NO=" & intSrvManNo

        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal

    End Function


    Public Function GetSrvManLastShiftInfo(ByVal strConnection As String, ByVal lngSrvMan As Long) As DataTable
        Dim drRetVal As DataTable
        Dim strSql As String
        Dim stConnection As String = strConnection

        strSql = "SELECT EmpNo,FleetCarNo,StartDt,EndDt,SrvManTakeBreak FROM empAttn where EndDt is null and EmpNo = " & lngSrvMan

        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
    End Function



    Public Function GetUserClaims(ByVal strConnection As String, ByVal lngUserId As Long) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable

        strSql = "SELECT  [ClaimId],[UserId],[ClaimType],[ClaimValue]FROM [Security].[UserClaim] where [UserId] = ?"
        strSql = DAL.InsertParameters(strSql, lngUserId)
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function




    End Function

    Public Function CheckSmsCode(ByVal strConnection As String, ByVal strSessionId As String, strSmsCode As String) As Long
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT count(1) FROM LoginLog where sessionGuid=?  and smsCode=?"
        strSql = DAL.InsertParameters(strSql, strSessionId, strSmsCode)
        lngRetVal = DAL.GetFieldValEmptyTbl(strConnection, strSql, 0, True)

        Return lngRetVal
    End Function
    Public Function checkIfSrvManLocked(ByVal strConnection As String, ByVal intSrvManNo As Integer, limitTime As Date) As Long
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT count(1) FROM LockedSrvMans where srvManNo=? and lockedDate> ?"
        strSql = DAL.InsertParameters(strSql, intSrvManNo, limitTime)
        lngRetVal = DAL.GetFieldValEmptyTbl(strConnection, strSql, 0, True)

        Return lngRetVal
    End Function


    Public Function checkIfSrvManIpLocked(ByVal strConnection As String, ByVal strIp As String, limitTime As Date) As Long
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT Count(1) FROM LockedIps where ip=? and lockedDate=?"
        strSql = DAL.InsertParameters(strSql, strIp, limitTime)
        ' intRecCount = DAL.Nz(DAL.GetFieldVal(strConnection, strSql), 0)
        lngRetVal = DAL.GetFieldValEmptyTbl(strConnection, strSql, 0, True)

        Return lngRetVal
    End Function


    Public Function GetProccessCurrentStep(ByVal strConnection As String, ByVal lngLoginId As Long) As Integer
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT step FROM Processes where lastLoginId = ?"
        strSql = DAL.InsertParameters(strSql, lngLoginId)
        ' intRecCount = DAL.Nz(DAL.GetFieldVal(strConnection, strSql), 0)
        lngRetVal = DAL.Nz(DAL.GetFieldValEmptyTbl(strConnection, strSql, -1, True), -1)

        Return lngRetVal
    End Function




    Public Function GetProccessId(ByVal strConnection As String, ByVal lngLoginId As Long) As Long
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT processId FROM Processes where lastLoginId = ?"
        strSql = DAL.InsertParameters(strSql, lngLoginId)
        ' intRecCount = DAL.Nz(DAL.GetFieldVal(strConnection, strSql), 0)
        lngRetVal = DAL.Nz(DAL.GetFieldValEmptyTbl(strConnection, strSql, 0, True), -1)

        Return lngRetVal
    End Function


    Public Function GetIpLoginCount(ByVal strConnection As String, ByVal strIp As String, limitTime As Date) As Long
        Dim strSql As String
        Dim lngRetVal As Long

        strSql = "SELECT count(1) FROM LoginLog where ip =? and creationDate>=?"
        strSql = DAL.InsertParameters(strSql, strIp, limitTime)
        ' intRecCount = DAL.Nz(DAL.GetFieldVal(strConnection, strSql), 0)
        lngRetVal = DAL.GetFieldValEmptyTbl(strConnection, strSql, 0, True)

        Return lngRetVal
    End Function
    Public Function GetSrvManLoginRecord(ByVal strConnection As String, sessionId As String) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable

        strSql = "SELECT Id,creationDate,sessionGuid,status,ip,customerName,SrvManNo,phoneNum,email,smsCode,smsCodeRetriesCounter,smsCreationDate,smsCodeCounter,lastUpdateDate,sbcDetailsId,isSupplier " &
                " From LoginLog" &
                " where sessionGuid=?"
        strSql = DAL.InsertParameters(strSql, sessionId)
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function
    End Function
    Public Function GetLoginRecord(ByVal strConnection As String, sessionId As String) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable

        strSql = "SELECT Id,creationDate,sessionGuid,status,ip,customerName,SrvManNo,phoneNum,email,smsCode,smsCodeRetriesCounter,smsCreationDate,smsCodeCounter,lastUpdateDate,sbcDetailsId,isSupplier " &
                " From LoginLog" &
                " where sessionGuid=?"
        strSql = DAL.InsertParameters(strSql, sessionId)
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function
    End Function
    Public Function GetSrvManLoginRecord(ByVal strConnection As String, sessionId As String, ByVal dtLimit As Date, intStatus As Integer) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable

        strSql = "SELECT Id,creationDate,sessionGuid,status,ip,customerName,SrvManNo,phoneNum,email,smsCode,smsCodeRetriesCounter,smsCreationDate,smsCodeCounter,lastUpdateDate,sbcDetailsId,isSupplier " &
                " From LoginLog" &
                " where sessionGuid=? and lastUpdateDate>=? and status=?"
        strSql = DAL.InsertParameters(strSql, sessionId, dtLimit, intStatus)
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function
    End Function
    Public Function GetCurrentLoginRecord(ByVal strConnection As String, ByVal lngEmpNo As Long, ByVal dtLimit As Date, intStatus As Integer) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable
        'replace empNo with SrvManNo data field name
        strSql = "SELECT Id,creationDate,sessionGuid,status,ip,customerName,SrvManNo,phoneNum,email,smsCode,smsCodeRetriesCounter,smsCreationDate,smsCodeCounter,lastUpdateDate,sbcDetailsId,isSupplier " &
                " From LoginLog" &
                " where SrvManNo=? and lastUpdateDate>=? and status=?"


        strSql = DAL.InsertParameters(strSql, lngEmpNo, dtLimit, intStatus)
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function
    End Function

    Public Function CreateProccessId(ByVal strConnection As String, ByVal lngLastLoginId As Long,
        ByVal intStep As Integer,
        ByVal intStatus As Integer,
        ByVal strSessionId As String,
        ByVal lngEmpNo As Long,
        ByVal intIsSupp As Integer) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean
        'Log Part
        Dim intLogRecId As Integer = 0
        Dim strParamList As String = ""
        Dim strAnsList As String = ""

        strSql = "INSERT INTO Processes (lastLoginId,creationDate,step,status,lastUpdateDate,sessionGuid,srvManNo,is_supp) " &
                 " VALUES (?,getdate(),?,?,getdate(),?,?,?) "

        strSql = DAL.InsertParameters(strSql, lngLastLoginId, intStep, intStatus, strSessionId, lngEmpNo, intIsSupp)

        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)

        If intAffRows = 0 Then
            blnRetVal = False
        Else
            blnRetVal = True
        End If




        Return blnRetVal
    End Function


    Public Function SetBreak(ByVal strConnection As String, iSrvmanNo As Integer, isSupplier As Boolean, iBreakTp As Integer, intFleetCar As Integer, CarKm As Integer) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean
        'Log Part
        Dim intLogRecId As Integer = 0
        Dim strParamList As String = ""
        Dim strAnsList As String = ""
        Dim iShiftTp As Integer = 0
        Dim intIsSupplier As Integer = 0

        If iBreakTp = enmBreakTp.enmStartBreak Then
            iShiftTp = enmShiftTp.enmStartBreakShiftment
        Else
            iShiftTp = enmShiftTp.enmEndBreakShiftment
        End If

        If isSupplier Then intIsSupplier = 1 Else intIsSupplier = 0

        'strSql = "INSERT INTO SRV_MAN_ATTN(SRV_MAN_NO,SHIFT_TP,SHIFT_DT,SHIFT_X,SHIFT_Y,SRV_CAR_NO,SRV_CAR_KM,REC_STS)  VALUES(?,?,getdate(),0,0,?,?,1)"
        'strSql = DAL.InsertParameters(strSql, iSrvmanNo, iShiftTp, intFleetCar, CarKm)
        strSql = "INSERT INTO WEBLET_SRV_MAN_ATTN(SRV_MAN_NO,SHIFT_TP,SHIFT_DT,SHIFT_X,SHIFT_Y,SRV_CAR_NO,SRV_CAR_KM,ATTN_TP,EXTERNAL_OPRR,REC_STS)  VALUES(?,?,getdate(),0,0,?,?,?,?,1)"
        strSql = DAL.InsertParameters(strSql, iSrvmanNo, iBreakTp, intFleetCar, CarKm, iShiftTp, intIsSupplier)


        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        blnRetVal = False
        'If intAffRows > 0 Then intIsSupplier = 1 Else intIsSupplier = 0
        'If isSupplier Then
        Select Case iShiftTp
            Case enmShiftTp.enmStartBreak 'Start Shift
                strSql = "insert into empAttnBreak (EmpNo,isSupplier,StartDt) values (?,?,getdate())"
                strSql = DAL.InsertParameters(strSql, iSrvmanNo, intIsSupplier)
                intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
                If Not isSupplier Then
                    strSql = "update empAttn set SrvManTakeBreak = 1 where EmpNo=? and EndDt is null"
                    strSql = DAL.InsertParameters(strSql, iSrvmanNo)
                    intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
                End If

            Case enmShiftTp.enmEndBreak 'End Shift
                strSql = "update empAttnBreak set EndDt = getdate() where EmpNo=? and EndDt is null"
                strSql = DAL.InsertParameters(strSql, iSrvmanNo)
                intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
                If Not isSupplier = True Then
                    strSql = "update empAttn set SrvManTakeBreak = 0 where EmpNo=? and EndDt is null"
                    strSql = DAL.InsertParameters(strSql, iSrvmanNo)
                    intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
                End If
        End Select
        If (intAffRows > 0) Then blnRetVal = True
        'End If




        Return blnRetVal
    End Function

    Public Function SetSrvManShift(ByVal strConnection As String, iSrvmanNo As Integer, isSupplier As Boolean, iShiftTp As Integer, intFleetCar As Integer, CarKm As Integer) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean
        'Log Part
        Dim intLogRecId As Integer = 0
        Dim strParamList As String = ""
        Dim strAnsList As String = ""
        Dim intIsSupplier As Integer = 0
        Dim intWebLetShift As Integer

        If iShiftTp = enmShiftTp.enmStartShiftment Then
            intWebLetShift = enmShiftTp.enmStartShift
        Else
            intWebLetShift = enmShiftTp.enmEndShift
        End If

        If isSupplier Then intIsSupplier = 1 Else intIsSupplier = 0
        ''on exit for now work Km =-1
        'strSql = "INSERT INTO SRV_MAN_ATTN(SRV_MAN_NO,SHIFT_TP,SHIFT_DT,SHIFT_X,SHIFT_Y,SRV_CAR_NO,SRV_CAR_KM,REC_STS)  VALUES(?,?,getdate(),0,0,?,?,1)"
        'strSql = DAL.InsertParameters(strSql, iSrvmanNo, iShiftTp, intFleetCar, CarKm)

        strSql = "INSERT INTO WEBLET_SRV_MAN_ATTN(SRV_MAN_NO,SHIFT_TP,SHIFT_DT,SHIFT_X,SHIFT_Y,SRV_CAR_NO,SRV_CAR_KM,ATTN_TP,EXTERNAL_OPRR,REC_STS)  VALUES(?,?,getdate(),0,0,?,?,?,?,1)"
        strSql = DAL.InsertParameters(strSql, iSrvmanNo, intWebLetShift, intFleetCar, CarKm, iShiftTp, intIsSupplier)


        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        blnRetVal = False
        If intAffRows > 0 Then
            If isSupplier = False Then
                Select Case iShiftTp
                    Case enmShiftTp.enmStartShiftment 'Start Shift
                        strSql = "insert into empAttn (EmpNo,FleetCarNo,SrvManTakeBreak,StartDt) values(?,?,0,getdate())"
                        strSql = DAL.InsertParameters(strSql, iSrvmanNo, intFleetCar, intFleetCar)
                        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
                    Case enmShiftTp.enmEndShiftment 'End Shift
                        strSql = "update empAttn set EndDt = getdate(),SrvManTakeBreak=0 where EmpNo=? and EndDt is null"
                        strSql = DAL.InsertParameters(strSql, iSrvmanNo)
                        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)

                        strSql = "update empAttnBreak set EndDt = getdate() where EmpNo=? and EndDt is null"
                        strSql = DAL.InsertParameters(strSql, iSrvmanNo)
                        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)

                End Select
            End If

        End If
        If (intAffRows > 0) Then blnRetVal = True



        Return blnRetVal
    End Function


    Public Function CreateLoginInfo(ByVal strConnection As String, ByVal dtCreationDate As Date,
        ByVal strSessionGuid As String,
        ByVal intStatus As Integer,
        ByVal strIp As String,
        ByVal strCustomerName As String,
        ByVal lngEmpNo As Long,
        ByVal strPhoneNum As String,
        ByVal strEmail As String,
        ByVal strSmsCode As String,
        ByVal intSmsCodeCounter As Integer,
        ByVal dtLastUpDateDate As Date,
        ByVal intSmsCodeRetriesCounter As Integer,
        ByVal blnIsSupplier As Integer
        ) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean
        'Log Part
        Dim intLogRecId As Integer = 0
        Dim strParamList As String = ""
        Dim strAnsList As String = ""
        'replace EmpNo with SrvManNo
        strSql = "INSERT INTO LoginLog (creationDate,sessionGuid,status,ip,customerName,SrvManNo,phoneNum,email,smsCode,smsCodeRetriesCounter,smsCodeCounter,lastUpdateDate,isSupplier) " &
                 " VALUES(? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,?  )"

        strSql = DAL.InsertParameters(strSql, dtCreationDate, strSessionGuid, intStatus, strIp, strCustomerName, lngEmpNo, strPhoneNum,
                                      strEmail, strSmsCode, intSmsCodeCounter, intSmsCodeRetriesCounter, dtLastUpDateDate, blnIsSupplier)

        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)

        If intAffRows = 0 Then
            blnRetVal = False
        Else
            blnRetVal = True
        End If




        Return blnRetVal
    End Function

    Public Function UpdateProccessStepId(ByVal strConnection As String, ByVal intProcessId As Long, ByVal intStepId As Integer, ByVal intSts As Integer) As Boolean
        'Return Log Rec_Id for update Answer
        Dim strSql As String

        Dim iAffRows As Integer
        Dim blnRetVal As Boolean
        blnRetVal = False

        strSql = "UPDATE Processes SET step = ?,status=?,lastUpdateDate=getdate() WHERE processId=?"
        strSql = DAL.InsertParameters(strSql, intStepId, intSts, intProcessId)
        iAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        If iAffRows > 0 Then blnRetVal = True
        Return blnRetVal

    End Function


    Public Function UpdateLoginTblWithSmsInfo(ByVal strConnection As String, ByVal strSmsCode As String, ByVal iSmsCodeCounter As Integer, ByVal iSmsCodeRetriesCounter As Integer, ByVal strSessionId As String) As Boolean
        'Return Log Rec_Id for update Answer
        Dim strSql As String

        Dim iAffRows As Integer
        Dim blnRetVal As Boolean
        blnRetVal = False

        strSql = "UPDATE LoginLog SET smsCode = ?,smsCodeCounter = ? ,smsCodeRetriesCounter = ?,smsCreationDate = getdate() WHERE sessionGuid = ?"
        strSql = DAL.InsertParameters(strSql, strSmsCode, iSmsCodeCounter, iSmsCodeRetriesCounter, strSessionId)
        iAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        If iAffRows > 0 Then blnRetVal = True
        Return blnRetVal

    End Function


    Public Function UpdateLoginTblSmsTry(ByVal strConnection As String, ByVal intCounter As Integer, ByVal strSessionId As String) As Boolean
        'Return Log Rec_Id for update Answer
        Dim strSql As String

        Dim iAffRows As Integer
        Dim blnRetVal As Boolean
        blnRetVal = False

        strSql = "UPDATE LoginLog SET smsCodeRetriesCounter = ? ,lastUpdateDate = getdate() WHERE sessionGuid = ?"
        strSql = DAL.InsertParameters(strSql, intCounter, strSessionId)
        iAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        If iAffRows > 0 Then blnRetVal = True
        Return blnRetVal

    End Function
    Public Function UpdateLoginTbl(ByVal strConnection As String, ByVal intSts As Integer, ByVal strSessionId As String) As Boolean
        'Return Log Rec_Id for update Answer
        Dim strSql As String

        Dim iAffRows As Integer
        Dim blnRetVal As Boolean
        blnRetVal = False

        strSql = "UPDATE LoginLog SET status = ? ,lastUpdateDate = getdate() WHERE sessionGuid = ?"
        strSql = DAL.InsertParameters(strSql, intSts, strSessionId)
        iAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        If iAffRows > 0 Then blnRetVal = True
        Return blnRetVal

    End Function

    Public Function LockSrvMan(ByVal strConnection As String, ByVal intSrvManNo As Long) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean
        'Log Part
        Dim intLogRecId As Integer = 0
        Dim strParamList As String = ""
        Dim strAnsList As String = ""

        strSql = "INSERT INTO LockedSrvMans(srvManNo,lockedDate) VALUES (?,getdate())"

        strSql = DAL.InsertParameters(strSql, intSrvManNo)

        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)

        If intAffRows = 0 Then
            blnRetVal = False
        Else
            blnRetVal = True
        End If




        Return blnRetVal
    End Function

End Class
