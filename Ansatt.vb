Public Class Ansatt
    Private Ansatt_ID
    Private Stilling

    Public Sub New()

    End Sub


    Sub New(ByVal enAnsatt_ID As String, ByVal enStilling As String)
        Ansatt_ID = enAnsatt_ID
        Stilling = enStilling
    End Sub

    Public Function hentStilling() As String
        Return Stilling
    End Function

    Public Function hentAnsattID() As String
        Return Ansatt_ID
    End Function


End Class
