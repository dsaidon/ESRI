Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.Generic
Imports System.Data
Public Class clsUtil
    Public Sub New()
        MyBase.New()
    End Sub


    Public Function GetComboData(ByVal strConnection As String, ByVal intComboTp As Integer) As DataTable
        Dim dtRetVal As DataTable
        Dim objLogin As New ShagApi.clsDUtil(strConnection)
        dtRetVal = objLogin.GetComboData(strConnection, intComboTp)
        Return dtRetVal

    End Function

    Public Function GetParameters(ByVal strConnection As String, ByVal intParam As Integer, blnNumeric As Boolean) As String
        Dim retVal As String = ""
        Dim objLogin As New ShagApi.clsDUtil(strConnection)
        retVal = objLogin.GetParameters(strConnection, intParam, blnNumeric)

        Return retVal
    End Function


    Public Function GetSiteParams(ByVal strConnection As String) As DataTable
        'Retrun list of SbcVld as DataTable
        Dim dtRetVal As DataTable
        Dim objLogin As New ShagApi.clsDUtil(strConnection)
        dtRetVal = objLogin.GetSiteParams(strConnection)
        Return dtRetVal

    End Function
End Class
