Imports StaySafe.My.Resources
Imports StaySafe.Ansatt
Imports StaySafe.AnsattInfo

Public Class LedelseFil

    'Funksjon som summerer alle salg etter dagens dato
    Public Shared Function finnDagensOmsettning()
        Dim krTabell As New DataTable
        Dim Today As Date = Date.Today()
        krTabell = Query("Select Sum(Totalpris) From Salg Where Dato='" & Today & "'")

        Dim totalsalg As String
        Dim ingenting As String = ""

        Try
            For Each row As DataRow In krTabell.Rows
                totalsalg = row("SUM(Totalpris)")
                Return totalsalg
            Next
        Catch ex As Exception
            MessageBox.Show("Det har ikke vært noen salg den " & Today)
        End Try
        Return ingenting


    End Function

    'Funksjon som teller opp alle dato collumns i databasen hvor datoen er den samme som i dag
    Public Shared Function dagensDato()
        Dim iDag As New DataTable

        Dim Today As Date = Date.Today()
        Dim antSalg As String
        Dim ingenting As String = ""
        iDag = Query("Select COUNT(Dato) FROM Salg Where Dato = '" & Today & "';")

        Try
            For Each row As DataRow In iDag.Rows
                antSalg = row("COUNT(Dato)")
                Return antSalg
            Next
        Catch ex As Exception
            MessageBox.Show("Det har ikke vært noen salg den " & Today)
        End Try
        Return Nothing
    End Function

    'Funksjon som teller opp alle dato collumns i databasen hvor datoen er den samme som det som blir valgt
    Public Shared Function antSalgForDato(ByVal dato As String)
        Dim spesDato As DataTable

        spesDato = Query("Select COUNT(Dato) FROM Salg Where Dato ='" & dato & "';")
        Try
            Dim antSalg As String
            For Each row As DataRow In spesDato.Rows
                antSalg = row("COUNT(Dato)")
                Return antSalg
            Next
        Catch ex As Exception
            MessageBox.Show("Det har ikke vært noen salg den " & dato)
        End Try
        Return Nothing
    End Function

    'Funksjon som summerer alle salg for datoen som er valgt
    Public Shared Function finnOmsettningPerDato(ByVal dato As String)
        Dim krTabell As New DataTable

        krTabell = Query("Select Sum(Totalpris) From Salg Where Dato='" & dato & "'")
        Try
            Dim totalsalg As String


            For Each row As DataRow In krTabell.Rows
                totalsalg = row("SUM(Totalpris)")
                Return totalsalg
            Next

        Catch ex As Exception
            MessageBox.Show("Det har ikke vært noen salg den " & dato)
        End Try
        Return Nothing
    End Function

    'Funksjon som finner antall salg en selger har gjort (velges med ID nummeret)
    Public Shared Function antallSalgAnsatt(ByVal valgtID As Integer)
        Dim interntabell As DataTable
        interntabell = Query("Select COUNT(Ansatt_ID) FROM Salg Where Ansatt_ID ='" & valgtID & "';")

        Dim antSalg As String
        Try
            For Each row As DataRow In interntabell.Rows
                antSalg = row("COUNT(Ansatt_ID)")
                Return antSalg
            Next
        Catch ex As Exception
            Console.WriteLine("Ingen salg")
        End Try
        Return Nothing
    End Function

    'Funksjon som summerer salgene en selger har gjort(velges med ID nummeret)
    Public Shared Function ansattSolgtFor(ByVal valgtID As Integer)
        Dim pristabell As DataTable
        pristabell = Query("Select SUM(Totalpris) From Salg Where Ansatt_ID='" & valgtID & "';")

        Dim totalsalg As String
        Try
            For Each row As DataRow In pristabell.Rows
                totalsalg = row("SUM(Totalpris)")
                Return totalsalg
            Next
        Catch ex As Exception
            Console.WriteLine("Ingen totalpris")
        End Try
        Return Nothing
    End Function

    'Funksjon for å hente ut ansattinfo fra databasen ved å søke på Ansatt_ID som man skriver inn i en inputbox
    Public Shared Function hentAnsattInfo() As AnsattInfo
        Dim IDsok As String = InputBox("Skriv inn IDen til den ansatte du vil se på / redigere")
        Dim sok As DataTable = Query("SELECT * FROM Ansatte WHERE Ansatt_ID='" & IDsok & "';")

        Dim ansattInfo As AnsattInfo
        Dim ansattID, fornavn, etternavn, adresse, tlf, epost, status, stilling, solgt_for, antall_salg As String
        For Each rad As DataRow In sok.Rows
            ansattID = rad("Ansatt_ID")
            fornavn = rad("Fornavn")
            etternavn = rad("Etternavn")
            adresse = rad("Adresse")
            tlf = rad("Tlf")
            epost = rad("Epost")
            status = rad("Status")
            stilling = rad("Stilling")
            solgt_for = rad("Solgt_for")
            antall_salg = rad("Antall_salg")
            ansattInfo = New AnsattInfo(ansattID, fornavn, etternavn, tlf, adresse, epost, status, stilling, solgt_for, antall_salg)
            Return ansattInfo
        Next
        Return Nothing
    End Function

    'Funksjon for å lagre ansatt informasjon i databasen
    Public Shared Sub lagreAnsattInfo(ByVal ansatt As AnsattInfo)
        Dim ansattID, fornavn, etternavn, adresse, tlf, epost, status, stilling, solgtFor, antallSalg As String

        ansattID = ansatt.hentAnsattID
        fornavn = ansatt.hentFornavn
        etternavn = ansatt.hentEtternavn
        tlf = ansatt.hentTlf
        adresse = ansatt.hentAdresse
        epost = ansatt.hentEpost
        status = ansatt.hentStatus
        stilling = ansatt.hentStilling
        solgtFor = ansatt.hentSolgt_for
        antallSalg = ansatt.hentAntall_salg

        If ansattID = "" Then

            Dim ansattIdData As DataTable
            solgtFor = "0"
            Query("INSERT INTO Ansatte (Ansatt_ID) Select 1 + COALESCE((SELECT MAX(Ansatt_ID) FROM Ansatte),0)")
            ansattIdData = Query("SELECT MAX(Ansatt_ID) FROM Ansatte")

            For Each rad As DataRow In ansattIdData.Rows
                ansattID = rad("MAX(Ansatt_ID)")
            Next

        End If
        Try
            Query("UPDATE `Ansatte` SET `Fornavn`='" & fornavn & "',`Etternavn`='" & etternavn &
                              "',`Adresse`='" & adresse & "',`Tlf`='" & tlf & "',`Epost`='" & epost &
                              "',`Status`='" & status & "',`Stilling`='" & stilling & "',`Solgt_for`='" & solgtFor & "',`Antall_salg`='" & antallSalg & "' WHERE `Ansatt_ID`='" & ansattID & "';")

        Catch ex As Exception
        Finally
            MessageBox.Show("Endring er lagret")
        End Try

    End Sub

    'funkjson for å finne den mest solgte varen som er markert for salg og hvor mange av den varen som er blitt solgt samt regner ut avansen og skriver ut i listbox
    Public Shared Sub mestSolgetISalg()
        Dim tabell As DataTable = Query("SELECT COUNT(*) FROM Lager WHERE Status =  'Salg' AND Antall_solgt = (Select MAX(Antall_solgt) From Lager)")

        Dim solgt As DataTable = Query("SELECT * FROM Lager WHERE Status= 'Salg' AND Antall_solgt = (Select MAX(Antall_solgt) From Lager);")


        Dim antall As String = ""
        For Each rad As DataRow In tabell.Rows
            antall = rad("COUNT(*)")
        Next

        If antall > 1 Then
            My.Forms.Ledelse.ListBox1.Items.Add("Varene som er solgt flest ganger er:")
        Else
            My.Forms.Ledelse.ListBox1.Items.Add("Den mest solgte varen ved salg er:")
        End If

        Dim vID, navn, type, pris, antallsolgt, varekostnad As String
        Dim avanse As Integer
        For Each rad As DataRow In solgt.Rows
            vID = rad("Vare_ID")
            navn = rad("Navn")
            type = rad("Type")
            pris = rad("Pris")
            antallsolgt = rad("Antall_solgt")
            varekostnad = rad("Varekostnad")

            avanse = (pris - varekostnad) * antallsolgt

            My.Forms.Ledelse.ListBox1.Items.Add("Vare ID: " & vID)
            My.Forms.Ledelse.ListBox1.Items.Add("Navn: " & navn)
            My.Forms.Ledelse.ListBox1.Items.Add("Type: " & type)
            My.Forms.Ledelse.ListBox1.Items.Add("Pris: " & pris)
            My.Forms.Ledelse.ListBox1.Items.Add("Det har blitt solgt: " & antallsolgt & " stykk")
            My.Forms.Ledelse.ListBox1.Items.Add("Varen har en avanse på: " & avanse)
            My.Forms.Ledelse.ListBox1.Items.Add("---")

        Next
    End Sub

    'funksjon for å finne varen som er utleid flest ganger og hvor mange ganger den er utleid og regner ut avansen samt skriver ut i listbox
    Public Shared Sub mestLeideVare()

        Dim tabell As DataTable = Query("SELECT COUNT(*) FROM Lager WHERE Status =  'Leie' AND Antall_solgt = (Select MAX(Antall_solgt) From Lager)")

        Dim solgt As DataTable = Query("SELECT * FROM Lager WHERE Status = 'Leie' AND Antall_solgt = ( 
                                                                                    SELECT MAX( Antall_solgt ) 
                                                                                    FROM Lager
                                                                                    WHERE STATUS =  'Leie' );")


        Dim antall As String = ""
        For Each rad As DataRow In tabell.Rows
            antall = rad("COUNT(*)")
        Next

        If antall > 1 Then
            My.Forms.Ledelse.ListBox1.Items.Add("Varene som er utleid mest er:")
        Else
            My.Forms.Ledelse.ListBox1.Items.Add("Den mest utleide varen er:")
        End If

        Dim vID, navn, type, pris, antallsolgt, varekostnad, avanse As String
        For Each rad As DataRow In solgt.Rows
            vID = rad("Vare_ID")
            navn = rad("Navn")
            type = rad("Type")
            pris = rad("Pris")
            antallsolgt = rad("Antall_solgt")
            varekostnad = rad("Varekostnad")

            avanse = (pris - varekostnad) * antallsolgt


            My.Forms.Ledelse.ListBox1.Items.Add("Vare ID: " & vID)
            My.Forms.Ledelse.ListBox1.Items.Add("Navn: " & navn)
            My.Forms.Ledelse.ListBox1.Items.Add("Type: " & type)
            My.Forms.Ledelse.ListBox1.Items.Add("Pris: " & pris)
            My.Forms.Ledelse.ListBox1.Items.Add("Det har blitt utleid: " & antallsolgt & " ganger")
            My.Forms.Ledelse.ListBox1.Items.Add("Avansen er på: " & avanse & " kr")
            My.Forms.Ledelse.ListBox1.Items.Add("---")

        Next
    End Sub



End Class
