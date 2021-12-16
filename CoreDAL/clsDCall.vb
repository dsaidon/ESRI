Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web


Public Class clsDCall
    Private m_ConnectionString As String

    Public Sub New(ByVal strConnectionString As String)
        MyBase.New()
        m_ConnectionString = strConnectionString
    End Sub


    Public Function SetCallAction(ByVal strConnection As String, ByVal lngCallNo As Long, ByVal lngExecNo As Long, ByVal intActionTp As Integer, ByVal X As Double, ByVal Y As Double, ByVal lngSrvManNo As Long, ByVal lngFleetCarNo As Long, ByVal intIsSupplier As Integer) As Boolean

        Dim strSql As String
        Dim intAffRows As Integer
        Dim blnRetVal As Boolean

        strSql = "INSERT INTO WEBLET_SRV_MAN_EXEC_STS (CALL_NO,EXEC_NO,WEB_LET_REC_ID,EXEC_STS,REP_TM,REP_X,REP_Y,SRV_MAN_NO,SRV_CAR_NO,REC_STS,EXTERNAL_OPRR)" &
                 " VALUES(?,?,-1,?,getdate(),?,?,?,?,0,?)"

        strSql = DAL.InsertParameters(strSql, lngCallNo, lngExecNo, intActionTp, X, Y, lngSrvManNo, lngFleetCarNo, intIsSupplier)

        intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
        blnRetVal = False
        If (intAffRows > 0) Then blnRetVal = True
        If intActionTp = 4 Then 'reject actionType
            strSql = "update WEBLET_FLEET_CAR_REQUEST set STS=1 where SRV_MAN_NO = ? and EXEC_NO = ?"
            strSql = DAL.InsertParameters(strSql, lngSrvManNo, lngExecNo)
            intAffRows = DAL.ExecuteNonQuery(strConnection, strSql)
            blnRetVal = False
            If (intAffRows > 0) Then blnRetVal = True
        End If

        Return blnRetVal
    End Function
End Class
