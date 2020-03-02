Public Class Kunde
    Private kundeid As String
    Private fornavn As String
    Private etternavn As String
    Private tlf As String
    Private adresse As String
    Private epost As String
    Private kjoptfor As String


    Public Sub New()

    End Sub

    'Klasse konstruktør
    Public Sub New(ByVal nykundeid As String, ByVal nyfornavn As String, ByVal nyetternavn As String, ByVal nytlf As String,
                   ByVal nyadresse As String, ByVal nyepost As String, ByVal nykjoptfor As String)

        kundeid = nykundeid
        fornavn = nyfornavn
        etternavn = nyetternavn
        tlf = nytlf
        adresse = nyadresse
        epost = nyepost
        kjoptfor = nykjoptfor

    End Sub

    'Funksjon for å hente ut kunde_ID
    Public Function hentKundeid() As String
        Return kundeid
    End Function

    'Funksjon får å hente ut Kunde_Fornavn
    Public Function hentKundeFornavn() As String
        Return fornavn
    End Function

    'Funksjon for å hente ut Kunde_etternavn
    Public Function hentKundeEtternavn() As String
        Return etternavn
    End Function

    'Funksjon for å hente ut Kunde_telefonnummer
    Public Function hentKundeTelefonummer() As String
        Return tlf
    End Function

    'Funksjon for å hente ut Kunde_adresse
    Public Function hentKundeAdresse() As String
        Return adresse
    End Function

    'Funksjon for å hente ut Kunde_epost
    Public Function hentKundeEpost() As String
        Return epost
    End Function

    'Funksjon for å hente ut Kunde_kjoptfor
    Public Function hentKundeKjoptfor() As String
        Return kjoptfor
    End Function

End Class
