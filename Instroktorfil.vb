Public Class Instroktorfil

    'Funksjon for tar med valgt dato for når du er opptatt og legger dette inn i databasen, sammen med Ansatt_ID
    'som er hentet fra brukernavn og passordet du har logget inn med.
    Public Shared Function registrerOpptattinstroktor(ByVal dato As String)
        Dim ansattid As String = Ansattsominnlogget.hentAnsattID

        Try
            Query("INSERT INTO `Introktor_tilgj`(`Dato`, `Tilgjengelighet`, `Ansatt_ID`) VALUES ('" & dato & "','false','" & ansattid & "')")
        Catch ex As Exception
            MessageBox.Show("Det har oppstått en feil. Kontakt administrator")
        End Try
        Return dato
    End Function

    'Henter ut informasjon om neste kurs med hensyn til person som er innlogget.
    Public Shared Function nesteKurs() As DataTable
        Dim ansattid As String = Ansattsominnlogget.hentAnsattID
        Dim kurstekst As DataTable = Query("SELECT * FROM Kurs WHERE Dato > CURDATE() AND Ansatt_ID=" &
                      ansattid & " ORDER BY Dato LIMIT 1")
        Return kurstekst
    End Function

    'Henter ut informasjon om de tre neste kursene med hensyn til person som er innlogget.
    Public Shared Function treNesteKurs() As DataTable
        Dim ansattid As String = Ansattsominnlogget.hentAnsattID
        Dim kurstekst As DataTable = Query("SELECT * FROM Kurs WHERE Dato > CURDATE() AND Ansatt_ID=" &
                      ansattid & " ORDER BY Dato LIMIT 3")
        Return kurstekst
    End Function

    'Henter ut informasjon om alle kurs med hensyn til person som er innlogget.
    Public Shared Function alleNesteKurs() As DataTable
        Dim ansattid As String = Ansattsominnlogget.hentAnsattID
        Dim kurstekst As DataTable = Query("SELECT * FROM Kurs WHERE Dato > CURDATE() AND Ansatt_ID=" &
                      ansattid & " ORDER BY Dato")
        Return kurstekst
    End Function

    'Funksjon for å skrive ut kurs informasjon til listboxene fra funksjonene "nesteKurs".
    Public Shared Sub skrivListbox(type As DataTable)
        Dim kursid, kundeid, dato, sted As String

        Instruktor.ListBox1.Items.Clear()

        For Each rad As DataRow In type.Rows
            kursid = rad("Kurs_ID")
            kundeid = rad("Kunde_ID")
            dato = rad("Dato")
            sted = rad("Sted")
            Instruktor.ListBox1.Items.Add("Ditt neste kurs er: " & dato)
            Instruktor.ListBox1.Items.Add("Sted: " & sted)
            Instruktor.ListBox1.Items.Add("For kunde: " & kundeid)
            Instruktor.ListBox1.Items.Add("Kurs ID: " & kursid)
            Instruktor.ListBox1.Items.Add("----")
        Next
    End Sub

End Class
