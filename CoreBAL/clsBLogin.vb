Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.Generic
Imports System.Data

Public Class clsRetUser
    Public UserId As Integer
    Public UserName As String
    Public Password As String

End Class
Public Class clsSmsCode
    Public SrvManNo As Integer
    Public SessionId As String
    Public SmsCode As String
    Public IsAuthenticated As Boolean


End Class


Public Class clsSrvManLastShiftInfo
    Public SrvManNo As Integer
    Public FleetCarNo As Long
    Public SessionId As String
    Public IsStartBreak As Boolean
    Public IsShiftStarted As Boolean
    Public IsSupplier As Boolean
    Public StartShift As Date
    Public ShiftBreak As Date
    Public ErrID As Boolean
    Public ErrDesc As String
End Class


Public Class clsShif
    Public Success As Boolean
    Public ErrMsg As String
    Public NextStep As Integer
End Class


Public Class clsRetSrvMan
    Public srvManNo As Integer
    Public srvManTp As Integer
    Public isSupplier As Boolean
    Public userName As String
    Public firstName As String
    Public lastName As String
    Public phoneNo As String
    Public srvManExists As Integer
    Public fleetCarNo As String

End Class


Public Class LoginLogs
    Public ErrID As Boolean
    Public ErrDesc As String
    Public loginId As Long
    Public creationDate As Date
    Public sessionId As String
    Public status As Integer
    Public ip As String
    Public SrvManName As String
    Public SrvManNo As Long
    Public phoneNo As String
    Public email As String
    Public smsCode As String
    Public smsCodeCounter As Integer
    Public lastUpdateDate As Date
    Public sbcDetailsId As Long
    Public smsCodeRetriesCounter As Integer
    Public smsCreationDate As Date
    Public isSupplier As Boolean
End Class


Public Class clsBLogin

    Public Sub New()
        MyBase.New()
    End Sub


    Public Function CheckIfUserExists(ByVal strConnection As String, LoginUser As clsRetUser) As clsRetUser
        'Retrun list of SbcVld as DataTable
        Dim lngRetVal As Long
        Dim objLogin As New ShagApi.clsDLogin(strConnection)
        lngRetVal = objLogin.CheckIfUserExists(strConnection, LoginUser.UserName, LoginUser.Password)
        LoginUser.UserId = lngRetVal
        Return LoginUser


    End Function
    Public Function CheckIfSrvManExists(ByVal strConnection As String, LoginUser As clsRetSrvMan) As clsRetSrvMan
        'Retrun list of SbcVld as DataTable
        Dim objRetSrvMan As New clsRetSrvMan
        Dim objLogin As New ShagApi.clsDLogin(strConnection)
        Dim rsTable As DataTable

        rsTable = New DataTable

        rsTable = objLogin.CheckIfSrvManExists(strConnection, LoginUser.SrvManNo)

        Dim dr As DataRow
        'FIRST_NM,LAST_NM,SRV_MAN_TP,PHONE_NO
        If rsTable.Rows.Count = 0 Then
            objRetSrvMan.SrvManExists = 0
        Else
            For Each dr In rsTable.Rows
                objRetSrvMan.SrvManNo = LoginUser.SrvManNo
                objRetSrvMan.SrvManTp = Nz(dr("SRV_MAN_TP"), 0)
                objRetSrvMan.isSupplier = False
                If (objRetSrvMan.SrvManTp = 3) Then
                    objRetSrvMan.isSupplier = True
                End If
                objRetSrvMan.FirstName = dr("FIRST_NM") & ""
                objRetSrvMan.LastName = dr("LAST_NM") & ""
                objRetSrvMan.PhoneNo = dr("PHONE_NO") & ""
                objRetSrvMan.UserName = objRetSrvMan.FirstName & " " & objRetSrvMan.LastName
                objRetSrvMan.SrvManExists = 1
                objRetSrvMan.FleetCarNo = ""
            Next
        End If

        Return objRetSrvMan


    End Function


    Public Function GetUserClaims(ByVal strConnection As String, ByVal lngUserId As Long) As DataTable
        'Retrun list of SbcVld as DataTable
        Dim dtRetVal As DataTable
        Dim objLogin As New ShagApi.clsDLogin(strConnection)
        dtRetVal = objLogin.GetUserClaims(strConnection, lngUserId)
        Return dtRetVal

    End Function
    Public Function GetSrvManClaims(ByVal strConnection As String, ByVal lngSrvManClaimId As Long) As DataTable
        'Retrun list of SbcVld as DataTable
        Dim dtRetVal As DataTable
        Dim objLogin As New ShagApi.clsDLogin(strConnection)
        dtRetVal = objLogin.GetUserClaims(strConnection, lngSrvManClaimId)
        Return dtRetVal

    End Function
    Public Function GetLoginRecord(ByVal strConnection As String, sessionId As String) As LoginLogs

        Dim rsTable As New DataTable
        Dim dr As DataRow
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim objLoginLog As New LoginLogs


        rsTable = objSbc.GetLoginRecord(strConnection, sessionId)
        If Not rsTable Is Nothing Then
            rsTable.TableName = "LOGIN_LOG"
        End If

        If rsTable.Rows.Count = 0 Then
            rsTable = Nothing
            objLoginLog.ErrID = 1
            objLoginLog.ErrDesc = "לא נמצא חיבור קודם"
        Else
            For Each dr In rsTable.Rows
                objLoginLog.loginId = Integer.Parse(dr("Id").ToString)
                objLoginLog.creationDate = Date.Parse(dr("creationDate").ToString)
                objLoginLog.sessionId = dr("sessionGuid").ToString
                objLoginLog.status = dr("status").ToString
                objLoginLog.ip = dr("ip").ToString
                objLoginLog.SrvManName = dr("customerName").ToString
                objLoginLog.SrvManNo = Integer.Parse(dr("SrvManNo").ToString)
                objLoginLog.phoneNo = dr("phoneNum").ToString
                objLoginLog.email = dr("email").ToString
                objLoginLog.smsCode = dr("smsCode").ToString
                objLoginLog.smsCodeRetriesCounter = Integer.Parse(dr("smsCodeRetriesCounter").ToString)
                If (Not dr.IsNull("smsCreationDate")) Then
                    objLoginLog.smsCreationDate = Date.Parse(dr("smsCreationDate").ToString)
                End If
                objLoginLog.smsCodeCounter = Integer.Parse(dr("smsCodeCounter").ToString)
                objLoginLog.lastUpdateDate = Date.Parse(dr("lastUpdateDate").ToString)
                If (Not dr.IsNull("sbcDetailsId")) Then
                    objLoginLog.sbcDetailsId = Integer.Parse(dr("sbcDetailsId").ToString)
                End If

            Next
        End If

        Return objLoginLog

        Exit Function




    End Function
    Public Function GetSrvManLastShiftInfo(ByVal strConnection As String, ByVal lngSrvManNo As Long) As clsSrvManLastShiftInfo
        Dim lngResult As New clsSrvManLastShiftInfo
        Dim rsTable As New DataTable
        Dim dr As DataRow
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        rsTable = objSbc.GetSrvManLastShiftInfo(strConnection, lngSrvManNo)
        If Not rsTable Is Nothing Then
            rsTable.TableName = "SrvManShiftInfo"
        End If
        lngResult.ErrID = 0
        If rsTable.Rows.Count = 0 Then
            rsTable = Nothing
            lngResult.ErrID = 1
            lngResult.ErrDesc = "לא נמצאה משמרת פתוחה"
        Else
            For Each dr In rsTable.Rows
                lngResult.SrvManNo = Integer.Parse(dr("EmpNo").ToString)
                lngResult.FleetCarNo = Long.Parse(dr("FleetCarNo").ToString)
                lngResult.StartShift = Date.Parse(dr("StartDt").ToString)
                lngResult.IsShiftStarted = True
                lngResult.IsSupplier = False
                lngResult.IsStartBreak = Integer.Parse(dr("SrvManTakeBreak").ToString)
                'If (Not dr.IsNull("smsCreationDate")) Then
                '    objLoginLog.smsCreationDate = Date.Parse(dr("smsCreationDate").ToString)
                'End If


            Next
        End If

        Return lngResult







        Return lngResult

    End Function

    Public Function GetSrvManLoginRecord(ByVal strConnection As String, sessionId As String, ByVal dtLimit As Date, intStatus As Integer) As LoginLogs

        Dim rsTable As New DataTable
        Dim dr As DataRow
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim objLoginLog As New LoginLogs


        rsTable = objSbc.GetSrvManLoginRecord(strConnection, sessionId, dtLimit, intStatus)
        If Not rsTable Is Nothing Then
            rsTable.TableName = "LOGIN_LOG"
        End If

        If rsTable.Rows.Count = 0 Then
            rsTable = Nothing
            objLoginLog.ErrID = 1
            objLoginLog.ErrDesc = "לא נמצא חיבור קודם"
        Else
            For Each dr In rsTable.Rows
                objLoginLog.ErrID = False
                objLoginLog.loginId = Integer.Parse(dr("Id").ToString)
                objLoginLog.creationDate = Date.Parse(dr("creationDate").ToString)
                objLoginLog.sessionId = dr("sessionGuid").ToString
                objLoginLog.status = dr("status").ToString
                objLoginLog.ip = dr("ip").ToString
                objLoginLog.SrvManName = dr("customerName").ToString
                objLoginLog.SrvManNo = Integer.Parse(dr("SrvManNo").ToString)
                objLoginLog.phoneNo = dr("phoneNum").ToString
                objLoginLog.email = dr("email").ToString
                objLoginLog.smsCode = dr("smsCode").ToString
                objLoginLog.smsCodeRetriesCounter = Integer.Parse(dr("smsCodeRetriesCounter").ToString)
                If (Not dr.IsNull("smsCreationDate")) Then
                    objLoginLog.smsCreationDate = Date.Parse(dr("smsCreationDate").ToString)
                End If
                objLoginLog.smsCodeCounter = Integer.Parse(dr("smsCodeCounter").ToString)
                objLoginLog.lastUpdateDate = Date.Parse(dr("lastUpdateDate").ToString)
                If (Not dr.IsNull("sbcDetailsId")) Then
                    objLoginLog.sbcDetailsId = Integer.Parse(dr("sbcDetailsId").ToString)
                End If
                objLoginLog.isSupplier = Integer.Parse(dr("isSupplier").ToString)
            Next
        End If

        Return objLoginLog

        Exit Function




    End Function



    Public Function GetCurrentLoginRecord(ByVal strConnection As String, ByVal lngEmpNo As Long, ByVal dtLimit As Date, intStatus As Integer) As LoginLogs

        Dim rsTable As New DataTable
        Dim dr As DataRow
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim objLoginLog As New LoginLogs


        rsTable = objSbc.GetCurrentLoginRecord(strConnection, lngEmpNo, dtLimit, intStatus)
        If Not rsTable Is Nothing Then
            rsTable.TableName = "LOGIN_LOG"
        End If

        If rsTable.Rows.Count = 0 Then
            rsTable = Nothing
            objLoginLog.ErrID = 1
            objLoginLog.ErrDesc = "לא נמצא חיבור קודם"
        Else
            For Each dr In rsTable.Rows
                objLoginLog.loginId = Integer.Parse(dr("Id").ToString) 'loginId
                objLoginLog.creationDate = Date.Parse(dr("creationDate").ToString)
                objLoginLog.sessionId = dr("sessionGuid").ToString
                objLoginLog.status = dr("status").ToString
                objLoginLog.ip = dr("ip").ToString
                objLoginLog.SrvManName = dr("customerName").ToString
                objLoginLog.SrvManNo = Integer.Parse(dr("SrvManNo").ToString)
                objLoginLog.phoneNo = dr("phoneNum").ToString
                objLoginLog.email = dr("email").ToString
                objLoginLog.smsCode = dr("smsCode").ToString
                objLoginLog.isSupplier = Integer.Parse(dr("isSupplier").ToString)
                objLoginLog.smsCodeRetriesCounter = Integer.Parse(dr("smsCodeRetriesCounter").ToString)
                If (Not dr.IsNull("smsCreationDate")) Then
                    objLoginLog.smsCreationDate = Date.Parse(dr("smsCreationDate").ToString)
                End If
                objLoginLog.smsCodeCounter = Integer.Parse(dr("smsCodeCounter").ToString)
                objLoginLog.lastUpdateDate = Date.Parse(dr("lastUpdateDate").ToString)
                If (Not dr.IsNull("sbcDetailsId")) Then
                    objLoginLog.sbcDetailsId = Integer.Parse(dr("sbcDetailsId").ToString)
                End If

            Next
        End If

        Return objLoginLog

        Exit Function




    End Function


    Public Function GetIpLoginCount(ByVal strConnection As String, strIP As String, limitTime As Date) As Long

        Dim lngResult As Long
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.GetIpLoginCount(strConnection, strIP, limitTime)

        Return lngResult


    End Function

    Public Function GetProccessId(ByVal strConnection As String, ByVal lngLoginId As Long) As Long

        Dim lngResult As Long
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.GetProccessId(strConnection, lngLoginId)

        Return lngResult


    End Function

    Public Function GetProccessCurrentStep(ByVal strConnection As String, ByVal lngLoginId As Long) As Integer

        Dim lngResult As Long
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.GetProccessCurrentStep(strConnection, lngLoginId)

        Return lngResult


    End Function



    Public Function checkIfSrvManLocked(ByVal strConnection As String, lngSrvManNo As Integer, limitTime As Date) As Boolean

        Dim lngResult As Long
        Dim blnRetVal As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.checkIfSrvManLocked(strConnection, lngSrvManNo, limitTime)
        If lngResult > 0 Then
            blnRetVal = True
        Else
            blnRetVal = False
        End If
        Return blnRetVal


    End Function
    Public Function CheckSmsCode(ByVal strConnection As String, ByVal SessionId As String, ByVal SmsCode As String) As Boolean

        Dim lngResult As Long
        Dim blnRetVal As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.CheckSmsCode(strConnection, SessionId, SmsCode)
        If lngResult > 0 Then
            blnRetVal = True
        Else
            blnRetVal = False
        End If
        Return blnRetVal


    End Function
    Public Function checkIfSrvManIpLocked(ByVal strConnection As String, strIP As String, limitTime As Date) As Boolean
        Dim blnRetVal As Boolean
        Dim lngResult As Long
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        lngResult = objSbc.checkIfSrvManIpLocked(strConnection, strIP, limitTime)
        If lngResult > 0 Then
            blnRetVal = True
        Else
            blnRetVal = False
        End If
        Return blnRetVal


    End Function

    Public Function SetBreak(ByVal strConnection As String, iSrvmanNo As Integer, iIsSupplier As Boolean, iBreakTp As Integer, intFleetCar As Integer, CarKm As Integer) As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim blnResult As Boolean

        blnResult = objSbc.SetBreak(strConnection, iSrvmanNo, iIsSupplier, iBreakTp, intFleetCar, CarKm)
        Return blnResult

    End Function

    Public Function SetSrvManShift(ByVal strConnection As String, iSrvmanNo As Integer, isSupplier As Boolean, iShiftTp As Integer, intFleetCar As Integer, CarKm As Integer) As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim blnResult As Boolean

        blnResult = objSbc.SetSrvManShift(strConnection, iSrvmanNo, isSupplier, iShiftTp, intFleetCar, CarKm)
        Return blnResult

    End Function
    Public Function CreateLoginInfo(ByVal strConnection As String, clsLogin As LoginLogs) As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim blnResult As Boolean

        blnResult = objSbc.CreateLoginInfo(strConnection, clsLogin.creationDate, clsLogin.sessionId, clsLogin.status, clsLogin.ip, clsLogin.SrvManName, clsLogin.SrvManNo, clsLogin.phoneNo, clsLogin.email, clsLogin.smsCode, clsLogin.smsCodeCounter, clsLogin.lastUpdateDate, clsLogin.smsCodeRetriesCounter, clsLogin.isSupplier)
        Return blnResult

    End Function


    Public Function CreateProccessId(ByVal strConnection As String, clsLogin As LoginLogs, intStepId As Integer) As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim blnResult As Boolean

        blnResult = objSbc.CreateProccessId(strConnection, clsLogin.loginId, intStepId, 1, clsLogin.sessionId, clsLogin.SrvManNo, 0)
        Return blnResult

    End Function

    Public Function LockSrvMan(ByVal strConnection As String, lngSrvManNo As Long) As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        Dim blnResult As Boolean

        blnResult = objSbc.LockSrvMan(strConnection, lngSrvManNo)
        Return blnResult

    End Function


    Public Function UpdateProccessStepId(ByVal strConnection As String, intProccessId As Long, intStepId As Integer, intSts As Integer) As Boolean
        Dim blnResult As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        blnResult = objSbc.UpdateProccessStepId(strConnection, intProccessId, intStepId, intSts)
        Return blnResult
    End Function

    Public Function UpdateLoginTbl(ByVal strConnection As String, ByVal intSts As Integer, strSessionId As String) As Boolean
        Dim blnResult As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        blnResult = objSbc.UpdateLoginTbl(strConnection, intSts, strSessionId)
        Return blnResult
    End Function
    Public Function UpdateLoginTblSmsTry(ByVal strConnection As String, ByVal intCounter As Integer, strSessionId As String) As Boolean
        Dim blnResult As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        blnResult = objSbc.UpdateLoginTblSmsTry(strConnection, intCounter, strSessionId)
        Return blnResult
    End Function

    Public Function UpdateLoginTblWithSmsInfo(ByVal strConnection As String, clsLogin As LoginLogs) As Boolean
        Dim blnResult As Boolean
        Dim objSbc As New ShagApi.clsDLogin(strConnection)
        blnResult = objSbc.UpdateLoginTblWithSmsInfo(strConnection, clsLogin.smsCode, clsLogin.smsCodeCounter, clsLogin.smsCodeRetriesCounter, clsLogin.sessionId)
        Return blnResult
    End Function

    Public Function GetAllUsers() As clsRetUser
        'Retrun list of SbcVld as DataTable

        Dim clsAnswer As New clsRetUser
        clsAnswer.UserId = 1
        clsAnswer.UserName = "Dudi"
        clsAnswer.Password = "123456"
        Return clsAnswer


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
        Else
            Nz = Value
        End If

        Exit Function
    End Function




End Class
