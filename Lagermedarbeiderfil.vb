Imports StaySafe.My.Resources
Imports StaySafe.Vareskjema
Imports StaySafe.Varer
Public Class Lagermedarbeiderfil

    'Funksjon for sok. Du sender med en søkestring, spørringen blir gjort og den lister ut resultatet i listboksen til lagermedarbeiderne.
    Public Shared Sub lagersok(ByVal brukersok As String)

        Dim sok As DataTable = Query("SELECT * FROM Lager WHERE Navn LIKE '%" & brukersok & "%';")
        Dim vareid, navn, type, status, antall, kommentar, pris As String

        For Each rad As DataRow In sok.Rows
            vareid = rad("Vare_ID")
            navn = rad("Navn")
            type = rad("Type")
            status = rad("Status")
            antall = rad("Antall")
            kommentar = rad("Kommentar")
            pris = rad("Pris")
            Lagermedarbeider.ListBox1.Items.Add("Vare ID er: " & vareid)
            Lagermedarbeider.ListBox1.Items.Add("Varenavn: " & navn)
            Lagermedarbeider.ListBox1.Items.Add("Varetype: " & type)
            Lagermedarbeider.ListBox1.Items.Add("Status: " & status)
            Lagermedarbeider.ListBox1.Items.Add("Antall: " & antall)
            Lagermedarbeider.ListBox1.Items.Add("Kommentar: " & kommentar)
            Lagermedarbeider.ListBox1.Items.Add("Pris: " & pris)
            Lagermedarbeider.ListBox1.Items.Add("----")
        Next
    End Sub

    ' Henter vareinformasjon etter hva du valgte i comboboxen. Deretter legger informasjon inn i et objekt som blir returnert.
    Public Shared Function hentVareInfo(ByVal combo As ComboBox) As Varer
        Try
            Dim comboboxvalg As String = combo.SelectedItem.ToString
            Dim valgtvare As DataTable = Query("SELECT * FROM Lager WHERE Navn='" & comboboxvalg & "' AND Status NOT LIKE 'Slettet' ORDER BY 'Name';")

            Dim vareinfo As Varer
            Dim vareid, navn, type, status, antall, kommentar, pris, varekost As String
            For Each rad As DataRow In valgtvare.Rows
                vareid = rad("Vare_ID")
                navn = rad("Navn")
                type = rad("Type")
                status = rad("Status")
                antall = rad("Antall")
                kommentar = rad("Kommentar")
                pris = rad("Pris")
                varekost = rad("Varekostnad")

                If Vareskjema Is ActiveForm Then
                    If status = "Salg" Then
                        My.Forms.Vareskjema.RadioButton1.Checked = True
                    ElseIf status = "Leie" Then
                        My.Forms.Vareskjema.RadioButton2.Checked = True
                    End If
                End If

                vareinfo = New Varer(vareid, navn, type, status, antall, kommentar, pris, varekost)
                Return vareinfo
            Next
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Du må velge en vare fra comboboxen.")
        End Try
        Return Nothing
    End Function

    'Fyller comboboksen med varenavnene fra databasen.
    Public Shared Sub FyllCombobox(ByVal combo As ComboBox)
        Dim varene As DataTable = Query("SELECT * From Lager Where Status NOT LIKE 'Slettet'")

        Dim varenavn As String
        For Each rad As DataRow In varene.Rows
            varenavn = rad("Navn")
            combo.Items.Add(varenavn)
        Next
    End Sub

    'En subrutine som kjører en vare igjennom og sender informasjon til databasen.
    Public Shared Sub lagreVareInfo(ByVal Vare As Varer)
        Dim vareid, navn, type, status, antall, kommentar, pris, varekostnad As String
        vareid = Vare.hentVareID
        navn = Vare.hentVareNavn
        type = Vare.hentVareType
        status = Vare.hentVareStatus
        antall = Vare.hentVareAntall
        kommentar = Vare.hentVareKommentar
        pris = Vare.hentVarePris
        varekostnad = Vare.hentVarekostnad

        If vareid = "" Then

            Dim vareIdData As DataTable
            Query("INSERT INTO Lager (Vare_ID) Select 1 + COALESCE((SELECT MAX(Vare_ID) FROM Lager),0)")
            vareIdData = Query("SELECT MAX(Vare_ID) FROM Lager")

            For Each rad As DataRow In vareIdData.Rows
                vareid = rad("MAX(Vare_ID)")
            Next
            My.Forms.Vareskjema.TextBox1.Text = vareid
        End If
        Try
            Query("UPDATE `Lager` SET `navn`='" & navn & "',`Type`='" & type &
                              "',`Status`='" & status & "',`Antall`='" & antall & "',`Kommentar`='" & kommentar &
                              "',`Pris`='" & pris & "', `Varekostnad`='" & varekostnad & "' WHERE `Vare_ID`='" & vareid & "';")
        Catch ex As Exception
        Finally
            MessageBox.Show("Endring er lagret")
        End Try
        My.Forms.Vareskjema.ComboBox1.Items.Clear()
        FyllCombobox(My.Forms.Vareskjema.ComboBox1)
    End Sub

    'En enkel funksjon for å skjekke hvilken radiobutton som er huket av i vare skjemaet og returnere teksten.
    Public Shared Function vareSkjemaRadiobuttonCheck() As String
        Dim teksten As String = ""
        If My.Forms.Vareskjema.RadioButton1.Checked = True Then
            teksten = "Salg"
        ElseIf My.Forms.Vareskjema.RadioButton2.Checked = True Then
            teksten = "Leie"
        End If
        Return teksten
    End Function

    'En sub som "sletter" varer fra lageret med å endre status på varen til Slettet.
    Public Shared Sub slettVareFraLager()
        Dim vareslett As String = My.Forms.Vareskjema.TextBox1.Text
        Try
            Query("UPDATE `Lager` SET `Status`='Slettet' WHERE Vare_ID='" & vareslett & "';")
            MessageBox.Show("Varen er slettet")
            FyllCombobox(My.Forms.Vareskjema.ComboBox1)

        Catch feil As Exception
            MessageBox.Show("Det har oppstått en feil " & feil.Message)
        End Try

    End Sub

    'En sub som endrer layout på vare skjemaet til å passe til når varer skal slettes.
    Public Shared Sub vareSkjemaSlettLayout()
        My.Forms.Vareskjema.TextBox1.ReadOnly = True
        My.Forms.Vareskjema.TextBox2.ReadOnly = True
        My.Forms.Vareskjema.TextBox3.ReadOnly = True
        My.Forms.Vareskjema.RadioButton1.Enabled = False
        My.Forms.Vareskjema.RadioButton2.Enabled = False
        My.Forms.Vareskjema.TextBox5.ReadOnly = True
        My.Forms.Vareskjema.TextBox6.ReadOnly = True
        My.Forms.Vareskjema.TextBox7.ReadOnly = True
        My.Forms.Vareskjema.TextBox8.ReadOnly = True
        My.Forms.Vareskjema.Button1.Visible = False
        My.Forms.Vareskjema.Button2.Visible = True
        My.Forms.Vareskjema.Button3.Visible = True
        My.Forms.Vareskjema.Button4.Visible = False
    End Sub

    'En sub som endrer layut på vare skjemaet til å passe til når varer skal behandles(Legges til/Redigeres)
    Public Shared Sub vareSkjemaBenhandlingLayout()
        My.Forms.Vareskjema.TextBox1.ReadOnly = True
        My.Forms.Vareskjema.TextBox2.ReadOnly = False
        My.Forms.Vareskjema.TextBox3.ReadOnly = False
        My.Forms.Vareskjema.RadioButton1.Enabled = True
        My.Forms.Vareskjema.RadioButton2.Enabled = True
        My.Forms.Vareskjema.TextBox5.ReadOnly = False
        My.Forms.Vareskjema.TextBox6.ReadOnly = False
        My.Forms.Vareskjema.TextBox7.ReadOnly = False
        My.Forms.Vareskjema.TextBox8.ReadOnly = True
        My.Forms.Vareskjema.Button1.Visible = True
        My.Forms.Vareskjema.Button2.Visible = True
        My.Forms.Vareskjema.Button3.Visible = False
        My.Forms.Vareskjema.Button4.Visible = True
    End Sub

    'En sub som fjerner all tekst fra tekstboksene i vare skjemaet.
    Public Shared Sub vareSkjemaClear()
        My.Forms.Vareskjema.TextBox1.Clear()
        My.Forms.Vareskjema.TextBox2.Clear()
        My.Forms.Vareskjema.TextBox3.Clear()
        My.Forms.Vareskjema.TextBox5.Clear()
        My.Forms.Vareskjema.TextBox6.Clear()
        My.Forms.Vareskjema.TextBox7.Clear()
        My.Forms.Vareskjema.TextBox8.Clear()
        My.Forms.Vareskjema.ComboBox1.Items.Clear()
        Lagermedarbeiderfil.FyllCombobox(My.Forms.Vareskjema.ComboBox1)



    End Sub

    'Sub som henter ut ordrenr for ordre som er klare for pakk og legger dem i comboboks for lagermedarbeiderne.
    Public Shared Sub fyllordreforpakk(ByVal combo As ComboBox)
        Try
            Dim dato As Date = Date.Now.ToString("dd/MM/yyyy")
            Dim dagenOrdre As DataTable = Query("SELECT DISTINCT OrdreNr FROM Salg WHERE Dato='" & dato & "' AND Status='Aktiv';")
            Dim ordrenr As String

            combo.Items.Clear()
            For Each rad As DataRow In dagenOrdre.Rows
                ordrenr = rad("OrdreNr")
                combo.Items.Add(ordrenr)
            Next
            combo.SelectedItem = combo.Items(0)
        Catch feil As Exception
            Console.WriteLine("Ingen varer å pakke på dette tidspunkt")
        End try
    End Sub

    'Sub som henter ut data for valgte ordreid og viser i en listboks hva som skal pakkes.
    Public Shared Sub HentOrdreinfo(ByVal combo As ComboBox, ByVal listbox As ListBox)
        Try
            Dim logg As DataTable = Query("SELECT * FROM Salg WHERE OrdreNr='" & combo.SelectedItem.ToString & "';")

            listbox.Items.Add("OrdreNummer: " & combo.SelectedItem.ToString)
            Dim vare, antall As String
            For Each rad As DataRow In logg.Rows
                vare = rad("Varer")
                antall = rad("Antall")
                listbox.Items.Add("----")
                listbox.Items.Add("Varenavn: " & vare)
                listbox.Items.Add("Antall: " & antall)
            Next
        Catch feil As Exception
            MessageBox.Show("Du har ikke valgt et ordrenummer å pakke")
        End Try

    End Sub

    'Sub som kjøres når orderen er pakket. Og oppdaterer databasen.
    Public Shared Sub OrdrePakket(combo As ComboBox)
        Dim tabell As DataTable = Query("SELECT Type FROM Salg WHERE OrdreNr='" & combo.SelectedItem.ToString & "';")
        Dim status As String = ""
        Dim type As String

        For Each rad As DataRow In tabell.Rows
            status = rad("Type")
        Next
        If status = "Leie" Then
            type = "Utleid"
        Else
            type = "Innaktiv"
        End If

        Query("UPDATE `Salg` SET `Status`='" & type & "' WHERE OrdreNr='" & combo.SelectedItem.ToString & "';")
        fyllordreforpakk(combo)
    End Sub

    'Sub for å hente ut informasjon om varer som er utleid utifra kundeID
    Public Shared Sub leieInnleveringinfo(ByVal lagerlist As ListBox, ByVal leiekunde As Kunde)
        Try
            Dim leiekundeinfo As DataTable = Query("SELECT * FROM Salg WHERE Kunde_ID=" & leiekunde.hentKundeid & " AND Status='Utleid';")

            Dim vare, dato, antall, ordrenr As String
            For Each rad As DataRow In leiekundeinfo.Rows
                vare = rad("Varer")
                dato = rad("Dato")
                antall = rad("Antall")
                ordrenr = rad("OrdreNr")
                lagerlist.Items.Add("Ordrenummer: " & ordrenr)
                lagerlist.Items.Add("Vare: " & vare)
                lagerlist.Items.Add("Antall: " & antall)
                lagerlist.Items.Add("Dato utleid: " & dato)
                lagerlist.Items.Add("----")
                My.Forms.Lagermedarbeider.Label1.Text = ordrenr

            Next
        Catch er As Exception
            Console.WriteLine("Feil")
        End Try
    End Sub

    'Sub for å registrere innleverte varer og oppdatere lageret
    Public Shared Sub leverInnLeie()
        Dim leiekundeinfo As DataTable = Query("SELECT * FROM Salg WHERE OrdreNr=" & My.Forms.Lagermedarbeider.Label1.Text & " AND Status='Utleid';")
        Query("UPDATE  `Salg` SET  `Status` =  'Innaktiv' WHERE  `OrdreNr` = '" & My.Forms.Lagermedarbeider.Label1.Text & "' ;")

        Dim vare, dato, antall As String
        For Each rad As DataRow In leiekundeinfo.Rows
            vare = rad("Varer")
            dato = rad("Dato")
            antall = rad("Antall")
            Query("UPDATE Lager SET Antall=Antall+" & antall & " WHERE Navn='" & vare & "' AND Status='Leie';")
        Next
    End Sub

    'Sub for å hente ut 3 varer til utleie med kommentar og skrive ut i listbox
    Public Shared Sub leievarervedlikehold(ByVal lagerlist As ListBox)
        Dim leievarene As DataTable = Query("SELECT * FROM Lager WHERE Status='Leie' AND Kommentar NOT LIKE '' LIMIT 3;")

        lagerlist.Items.Clear()
        Dim vareid, navn, type, kommentar As String
        For Each rad As DataRow In leievarene.Rows
            vareid = rad("Vare_ID")
            navn = rad("Navn")
            type = rad("Type")
            kommentar = rad("Kommentar")
            lagerlist.Items.Add("Vareid: " & vareid)
            lagerlist.Items.Add("Navn: " & navn)
            lagerlist.Items.Add("Type: " & type)
            lagerlist.Items.Add("Kommentar: " & kommentar)
            lagerlist.Items.Add("---")
        Next

    End Sub

End Class
