Imports StaySafe.My.Resources
Public Class Selgerfil
    ' Henter kundeinformasjon etter tlfonnummeret du velger. Deretter legger informasjon inn i et objekt som blir returnert.
    Public Shared Function hentKundeInfo() As Kunde
        Dim sokord As String = InputBox("Skriv inn telefonnummeret til kunden", "Hent kundeinformasjon", " ")
        Dim sok As DataTable = Query("SELECT * FROM Kunde WHERE Tlf='" & sokord & "';")

        If sokord = " " Then
            MessageBox.Show("Du må skrive inn et kunde telefonnummer")
        ElseIf sokord = "" Then
            Return Nothing
            Exit Function
        Else
            Dim kundeinfo As Kunde
            Dim kundeid, fornavn, etternavn, tlf, adresse, epost, kjoptfor As String
            For Each rad As DataRow In sok.Rows
                kundeid = rad("Kunde_ID")
                fornavn = rad("Fornavn")
                etternavn = rad("Etternavn")
                tlf = rad("Tlf")
                adresse = rad("Adresse")
                epost = rad("Epost")
                kjoptfor = rad("Kjopt_for")
                kundeinfo = New Kunde(kundeid, fornavn, etternavn, tlf, adresse, epost, kjoptfor)
                Return kundeinfo
            Next
        End If
        Return Nothing
    End Function

    'Mottar et kundeobjekt og leser ut informasjon fra dette. Hvis objektet ikke har en kunde_id lages dette først.
    'Derretter opplastes informasjonen fra kunden til DB med hensyn til kunde_id'en.
    Public Shared Sub lagreKundeInfo(ByVal kunde As Kunde)
        Dim kundeid, fornavn, etternavn, tlf, adresse, epost, kjoptfor As String
        kundeid = kunde.hentKundeid
        fornavn = kunde.hentKundeFornavn
        etternavn = kunde.hentKundeEtternavn
        tlf = kunde.hentKundeTelefonummer
        adresse = kunde.hentKundeAdresse
        epost = kunde.hentKundeEpost
        kjoptfor = kunde.hentKundeKjoptfor

        If kundeid = "" Then

            Dim KundeIdData As DataTable
            kjoptfor = "0"
            Query("INSERT INTO Kunde (Kunde_ID) Select 1 + COALESCE((SELECT MAX(Kunde_ID) FROM Kunde),0)")
            KundeIdData = Query("SELECT MAX(Kunde_ID) FROM Kunde")

            For Each rad As DataRow In KundeIdData.Rows
                kundeid = rad("MAX(Kunde_ID)")
            Next

        End If
        Try
            Query("UPDATE `Kunde` SET `Fornavn`='" & fornavn & "',`Etternavn`='" & etternavn &
                              "',`Adresse`='" & adresse & "',`Tlf`='" & tlf & "',`Epost`='" & epost &
                              "',`Kjopt_for`='" & kjoptfor & "' WHERE `Kunde_ID`='" & kundeid & "';")
        Catch ex As Exception
        Finally
            MessageBox.Show("Endring er lagret")
        End Try

    End Sub

    'Henter navn på kunden og skriver det inn i tekstboksen til bestillingsskjemaet.
    Public Shared Sub bestillingkundeinfo()
        Try
            Dim valgtKunde As Kunde = hentKundeInfo()
            Dim kundeid As String = valgtKunde.hentKundeid
            Bestilling.Show()
            My.Forms.Bestilling.TextBox1.Text = (valgtKunde.hentKundeEtternavn & ", " & valgtKunde.hentKundeFornavn())
            My.Forms.Bestilling.Label10.Text = kundeid
        Catch ex As Exception
            MessageBox.Show("Finner ikke tlf")
            KundeSkjema.Show()
            Bestilling.Close()
        End Try

    End Sub

    'Sub som legger til valgt vare inn i Ordredatabasen med unikt ordrenr. Den skjekker også lagerbeholdning slik at man ikke kan legge til mer enn det
    'som er på lager. I tillegg tar det så merker av for salg eller leie ettersom hva som er valgt i bestillingsskjemaet og hvis varen finnes på Ordrenummeret
    'Legges antall valgte varer til der. Antallet varer fjernes samtidig fra lager, slik at varene er holdt av til kunden og selgerne ikke kan selge mer enn de har.
    Public Shared Sub leggTilVareiBestilling()
        Try
            Dim valgtvare As Varer = Lagermedarbeiderfil.hentVareInfo(My.Forms.Bestilling.ComboBox1)
        Dim antallvalgt As String = My.Forms.Bestilling.TextBox3.Text
        Dim antallLager As Integer = valgtvare.hentVareAntall()
        Dim vareID As Integer = valgtvare.hentVareID
        Dim totalpris As Integer = valgtvare.hentVarePris * antallvalgt
        Dim dato As String = My.Forms.Bestilling.DateTimePicker1.Value.ToString("yyyy/MM/dd")
        Dim status As String = ""

            If antallvalgt <= antallLager Then
                Dim logg As DataTable = Query("SELECT * FROM Ordre WHERE OrdreNr='" & My.Forms.Bestilling.Label8.Text & "' AND Vare='" & valgtvare.hentVareNavn & "';")
                Dim vare As String = ""

                My.Forms.Bestilling.ListBox1.Items.Clear()
                For Each rad As DataRow In logg.Rows
                    vare = rad("Vare")
                Next

                If My.Forms.Bestilling.RadioButton1.Checked = True Then
                    status = "Salg"
                ElseIf My.Forms.Bestilling.RadioButton2.Checked = True Then
                    status = "Leie"
                Else
                    status = "Kurs"
                End If

                If vare = Nothing Then
                    Query("INSERT INTO Ordre (Kunde_ID, Ansatt_ID, Vare, Totalpris, OrdreNr, Antall, Dato_Levering, status) Values (" & My.Forms.Bestilling.Label10.Text &
                            ", " & Ansattsominnlogget.hentAnsattID & ", '" & My.Forms.Bestilling.ComboBox1.SelectedItem.ToString & "', " & totalpris &
                            ", " & My.Forms.Bestilling.Label8.Text & ", " & antallvalgt & ", '" & dato & "', '" & status & "');")
                Else
                    Query("UPDATE Ordre SET Antall=Antall+" & antallvalgt & ", Totalpris=Totalpris+" & totalpris & " WHERE Vare='" & valgtvare.hentVareNavn &
                          "' AND OrdreNr='" & My.Forms.Bestilling.Label8.Text & "';")
                End If

                Query("UPDATE `Lager` SET `Antall`= Antall -" & antallvalgt & " WHERE Vare_ID=" & vareID & ";")

                My.Forms.Bestilling.TextBox2.Text = My.Forms.Bestilling.TextBox2.Text + totalpris

            Else
                MessageBox.Show("Det er ikke nok varer på lageret.")
            End If

        Catch ex As Exception
            Console.WriteLine("Kontakt admin.")
        End Try

    End Sub

    'Funksjon som lager et ordrenummer. Denne brukes for at ikke to eller flere ordre skal få samme ordrenummer.
    Public Shared Function LagOrdreNr() As String
        Dim ordre As DataTable = Query("SELECT MAX(OrdreNr) +1 FROM Ordre")

        Dim ordreNr As String

        For Each rad As DataRow In ordre.Rows
            ordreNr = rad("max(OrdreNr) +1")
            Return ordreNr
        Next
        Return Nothing
    End Function

    'Subrutine for å skrive ut hva som er lagt til i orderen.
    Public Shared Sub ordrelogg()
        Dim logg As DataTable = Query("SELECT Vare, Totalpris, Antall, Status FROM Ordre WHERE OrdreNr='" & My.Forms.Bestilling.Label8.Text & "';")
        Dim vare, pris, antall, status As String


        My.Forms.Bestilling.ListBox1.Items.Clear()

        logg.Rows.RemoveAt(0) ' Sletter første rad av Datatable fordi den er uten verdi, og blir bare brukt til å holde av ordrenummeret.
        For Each rad As DataRow In logg.Rows
            vare = rad("Vare")
            pris = rad("Totalpris")
            antall = rad("Antall")
            status = rad("Status")
            My.Forms.Bestilling.ListBox1.Items.Add("Varenavn: " & vare)
            My.Forms.Bestilling.ListBox1.Items.Add("Pris: " & pris)
            My.Forms.Bestilling.ListBox1.Items.Add("Antall: " & antall)
            My.Forms.Bestilling.ListBox1.Items.Add("Varen har status: " & status)
            My.Forms.Bestilling.ListBox1.Items.Add("----")
        Next
    End Sub

    'Denne subrutinen fjerner valgt antall og vare fra Ordren. Hva som allerede er der sees i Ordre loggen.
    'Hvis du fjerner det samme antallet av en vare som er i orderen, slettes denne(Ordredatabasen er bare midlertidig så ingen feil kan skje av slettingen)
    'Ellers oppdateres antallet i orderen.
    Public Shared Sub fjernFraOrdre()
        Dim antall As Integer = My.Forms.Bestilling.TextBox3.Text
        Dim vare As Varer = Lagermedarbeiderfil.hentVareInfo(My.Forms.Bestilling.ComboBox1)
        Dim ordrenr As String = My.Forms.Bestilling.Label8.Text
        Dim ordren As DataTable = Query("SELECT * FROM Ordre WHERE OrdreNr='" & ordrenr & "' AND Vare='" & vare.hentVareNavn & "' AND Status='" &
                                        vare.hentVareStatus & "';")

        Dim antallvareiordre As String
        Try
            For Each rad As DataRow In ordren.Rows
                antallvareiordre = rad("Antall")

                If antallvareiordre > antall Then
                    Query("UPDATE Ordre SET Antall=Antall-" & antall & ", Totalpris=Totalpris-" & vare.hentVarePris * antall & " WHERE Vare='" & vare.hentVareNavn &
                              "' AND OrdreNr='" & My.Forms.Bestilling.Label8.Text & "';")
                ElseIf antallvareiordre = antall Then
                    Query("DELETE FROM Ordre WHERE Vare='" & vare.hentVareNavn & "' AND OrdreNr='" & ordrenr & "';")
                Else
                    MessageBox.Show("Varen finnes ikke i Orderen eller du har valgt et høyre antall av vare enn det er i orderen")
                End If
            Next
            Query("UPDATE `Lager` SET `Antall`= Antall +" & antall & " WHERE " & vare.hentVareID & ";")
        Catch ex As Exception
            MessageBox.Show("Varen du har valgt kan ikke fjernes. Skjekk logg, salg og leie status.")
        End Try
    End Sub

    'Denne subrutinen annulerer orderen. Hvis kunden ikke ønsker å bestille likevel, slettes orderen. På ordrenummeret fått i starten av bestillingen.
    Public Shared Sub annulerOrdre()
        Dim ordrenr As String = My.Forms.Bestilling.Label8.Text
        Query("DELETE FROM Ordre WHERE OrdreNr=" & ordrenr & ";")
    End Sub

    'Fyller comboboks etter om man har valgt leie i bestillingsskjemaet.
    Public Shared Sub fyllbestillingscomboLeie(ByVal combo As ComboBox)
        Dim varene As DataTable = Query("SELECT * From Lager Where Status= 'Leie'")


        Dim varenavn As String
        combo.Items.Clear()
        combo.Text = ""
        For Each rad As DataRow In varene.Rows
            varenavn = rad("Navn")
            combo.Items.Add(varenavn)
        Next
        combo.SelectedItem = combo.Items(0)
    End Sub

    'Fyller comboboks etter om man har valgt Salg i bestillingsskjemaet.
    Public Shared Sub fyllbestillingscomboSalg(ByVal combo As ComboBox)
        Dim varene As DataTable = Query("SELECT * From Lager Where Status= 'Salg'")


        Dim varenavn As String
        combo.Items.Clear()
        combo.Text = ""
        For Each rad As DataRow In varene.Rows
            varenavn = rad("Navn")
            combo.Items.Add(varenavn)
        Next
        combo.SelectedItem = combo.Items(0)
    End Sub

    'Fyller comboboks etter om man har valgt Kurs i bestillingsskjemaet.
    Public Shared Sub fyllbestillingscomboKurs(ByVal combo As ComboBox)
        Dim varene As DataTable = Query("SELECT * From Lager Where Status= 'Kurs'")


        Dim varenavn As String
        combo.Items.Clear()
        combo.Text = ""
        For Each rad As DataRow In varene.Rows
            varenavn = rad("Navn")
            combo.Items.Add(varenavn)
        Next
        combo.SelectedItem = combo.Items(0)
    End Sub

    'Sub som henter ut ordren og sender den videre til Salg databasen, etterpå annulerer den orderen fra Ordre databasen.
    Public Shared Sub sendOrderen()
        Dim ordreNr As String = My.Forms.Bestilling.Label8.Text
        Dim orderen As DataTable = Query("SELECT * FROM Ordre WHERE OrdreNr=" & ordreNr & ";")

        Dim kundeid, ansattid, vare, totalpris, antall, dato, status, salgsstatus As String
        orderen.Rows.RemoveAt(0)
        For Each rad As DataRow In orderen.Rows
            kundeid = rad("Kunde_ID")
            ansattid = rad("Ansatt_ID")
            vare = rad("Vare")
            totalpris = rad("Totalpris")
            antall = rad("Antall")
            dato = rad("Dato_Levering")
            status = rad("Status")

            If status = "Kurs" Then
                salgsstatus = "Innaktiv"
            Else
                salgsstatus = "Aktiv"
            End If
            Query("INSERT INTO `Salg`(`Kunde_ID`, `Ansatt_ID`, `Varer`, `Dato`, `Totalpris`, `OrdreNr`, `Antall`, `Type`, `Status`) VALUES " &
                 "(" & kundeid & "," & ansattid & ",'" & vare & "','" & dato & "'," & totalpris & "," & ordreNr & "," & antall & ",'" & status & "','" & salgsstatus & "')")
            Query("UPDATE Lager SET Antall_solgt=Antall_solgt+" & antall & " WHERE Navn='" & vare & "';")
        Next
        annulerOrdre()
    End Sub

    'Subrutine for å holde av ordrenr.
    Public Shared Sub holdavordrenr(ByVal ordrenr As String)
        Query("INSERT INTO Ordre (OrdreNr) VALUES (" & ordrenr & ")")
    End Sub

End Class
