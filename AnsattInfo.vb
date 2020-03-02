Public Class AnsattInfo
    Private Ansatt_ID
    Private Fornavn
    Private Etternavn
    Private Adresse
    Private Tlf
    Private Epost
    Private Status
    Private Stilling
    Private Solgt_for
    Private Antall_salg

    Public Sub New()

    End Sub


    Public Sub New(ByVal enAnsatt_ID As String, ByVal enFornavn As String, ByVal enEtternavn As String, ByVal enAdresse As String, ByVal enTlf As String, ByVal enEpost As String, ByVal enStatus As String, ByVal enStilling As String, ByVal enSolgt_for As String, ByVal enAntall_salg As String)
        Ansatt_ID = enAnsatt_ID
        Fornavn = enFornavn
        Etternavn = enEtternavn
        Adresse = enAdresse
        Tlf = enTlf
        Epost = enEpost
        Status = enStatus
        Stilling = enStilling
        Solgt_for = enSolgt_for
        Antall_salg = enAntall_salg

    End Sub

    Public Function hentAnsattID() As String
        Return Ansatt_ID
    End Function

    Public Function hentFornavn() As String
        Return Fornavn
    End Function

    Public Function hentEtternavn() As String
        Return Etternavn
    End Function

    Public Function hentAdresse() As String
        Return Adresse
    End Function

    Public Function hentTlf() As String
        Return Tlf
    End Function

    Public Function hentEpost() As String
        Return Epost
    End Function

    Public Function hentStatus() As String
        Return Status
    End Function
    Public Function hentStilling() As String
        Return Stilling
    End Function

    Public Function hentSolgt_for() As String
        Return Solgt_for
    End Function

    Public Function hentAntall_salg() As String
        Return Antall_salg
    End Function


End Class
