Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.Generic
Imports System.Data
Public Class SrvManCallAction

    Public CallNo As Long
    Public ExecNo As Long
    Public X As Double
    Public Y As Double
    Public srvManNo As Long
    Public fleetCarNo As Long
    Public isSupplier As Integer
    Public ActionTp As Integer
    Public NextStep As Integer
    Public Success As Boolean
    Public ErrDesc As String

End Class
Public Class clsBCall
    Public Sub New()
        MyBase.New()
    End Sub

    Public Function SetCallAction(ByVal strConnection As String, clsCallAction As SrvManCallAction) As Boolean
        Dim objSbc As New ShagApi.clsDCall(strConnection)
        Dim blnResult As Boolean
        Dim lngCallNo As Long
        Dim lngExecNo As Long
        Dim intActionTp As Integer
        Dim X As Double
        Dim Y As Double
        Dim lngSrvManNo As Long
        Dim lngFleetCarNo As Long
        Dim intIsSupplier As Integer



        lngCallNo = clsCallAction.CallNo
        lngExecNo = clsCallAction.ExecNo
        intActionTp = clsCallAction.ActionTp
        X = clsCallAction.X
        Y = clsCallAction.Y
        lngSrvManNo = clsCallAction.srvManNo
        lngFleetCarNo = clsCallAction.fleetCarNo
        intIsSupplier = clsCallAction.isSupplier


        blnResult = objSbc.SetCallAction(strConnection, lngCallNo, lngExecNo, intActionTp, X, Y, lngSrvManNo, lngFleetCarNo, intIsSupplier)
        Return blnResult

    End Function
End Class
