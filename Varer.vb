Public Class Varer
    Private vareid As String
    Private navn As String
    Private type As String
    Private status As String
    Private antall As String
    Private kommentar As String
    Private pris As String
    Private varekostnad As String


    Public Sub New()

    End Sub

    'klasse konstruktør
    Public Sub New(ByVal enVareid As String, ByVal enNavn As String, ByVal enType As String, ByVal enStatus As String, ByVal enAntall As String, ByVal enKommentar As String,
                   ByVal enPris As String, ByVal enVarekostnad As String)

        vareid = enVareid
        navn = enNavn
        type = enType
        status = enStatus
        antall = enAntall
        kommentar = enKommentar
        pris = enPris
        varekostnad = enVarekostnad
    End Sub

    'Funksjon for å hente ut vare ID
    Public Function hentVareID() As String
        Return vareid
    End Function

    'Funksjon for å hente ut vare navn.
    Public Function hentVareNavn() As String
        Return navn
    End Function

    'Funksjon for å hente ut vare typen
    Public Function hentVareType() As String
        Return type
    End Function

    'Funksjon for å hente ut vare status
    Public Function hentVareStatus() As String
        Return status
    End Function

    'Funksjon for å hente ut vare antall
    Public Function hentVareAntall() As String
        Return antall
    End Function

    'funksjon for å hente ut vare kommentar
    Public Function hentVareKommentar() As String
        Return kommentar
    End Function

    'funksjon for å hente ut vare prisen.
    Public Function hentVarePris() As String
        Return pris
    End Function

    'funksjon for å hente ut vare kostnad
    Public Function hentVarekostnad() As String
        Return varekostnad
    End Function
End Class
