Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Public Class clsDUtil
    Private m_ConnectionString As String

    Private Enum enmComboID
        enmShiftTp = 1

    End Enum

    Public Sub New(ByVal strConnectionString As String)
        MyBase.New()
        m_ConnectionString = strConnectionString
    End Sub



    Public Function GetComboData(ByVal strConnection As String, ByVal intComboTp As Integer) As DataTable

        Dim strSql As String = ""
        Dim drRetVal As DataTable

        Select Case intComboTp
            Case enmComboID.enmShiftTp
                strSql = "SELECT REC_ID,DSC FROM SRV_MAN_ATTN_TP ORDER BY DSC"
            Case Else
        End Select

        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)
        Return drRetVal

    End Function


    Public Function GetParameters(ByVal strConnection As String, ByVal intParam As Integer, blnNumeric As Boolean) As String
        Dim lngRetVal As String
        Dim strSql As String
        Dim stConnection As String = strConnection
        Try
            If blnNumeric Then
                strSql = "SELECT intValue FROM SiteParam where Id=? "
            Else
                strSql = "SELECT stringValue FROM SiteParam where Id=? "
            End If

            strSql = DAL.InsertParameters(strSql, intParam)

            lngRetVal = DAL.Nz(DAL.GetFieldValEmptyTbl(stConnection, strSql, 0, False), 0)

        Catch ex As Exception
            lngRetVal = -1
        End Try

        Return lngRetVal
    End Function


    Public Function GetSiteParams(ByVal strConnection As String) As DataTable
        Dim strSql As String = ""
        Dim drRetVal As DataTable

        strSql = "SELECT * FROM SiteParams"
        drRetVal = DAL.GetSqlDataAsDataTabel(strConnection, strSql)

        Return drRetVal
        Exit Function


    End Function

End Class
