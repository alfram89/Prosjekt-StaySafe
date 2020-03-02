Public Class Bestilling

    'Knapp legg til, her legges en vare til.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Selgerfil.leggTilVareiBestilling()
        Dim valgtVare As Varer = Lagermedarbeiderfil.hentVareInfo(ComboBox1)
        Label5.Text = valgtVare.hentVareAntall()
        Selgerfil.ordrelogg()

    End Sub

    'Når bestillings form lastes fylles combobox, et ordreNr blir laget og hentes.
    Private Sub Bestilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Selgerfil.fyllbestillingscomboSalg(ComboBox1)
        Dim ordrenr As String = Selgerfil.LagOrdreNr
        Query("INSERT INTO Ordre (OrdreNr) VALUES (" & ordrenr & ")")
        Label8.Text = ordrenr
        DateTimePicker1.MinDate = Date.Now
        RadioButton1.Checked = True
        Selger.Hide()
    End Sub

    'Hver gang comboboxen endrer verdi henter den vareinformasjon for valgt vare og skriver ut antall på lager.
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim valgtVare As Varer = Lagermedarbeiderfil.hentVareInfo(ComboBox1)
        Label5.Text = valgtVare.hentVareAntall()

    End Sub

    'Knapp Fjern. Her fjernes en vare fra orderen, se sub Selgerfil.fjernFraOrdre
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Selgerfil.fjernFraOrdre()
        Dim valgtVare As Varer = Lagermedarbeiderfil.hentVareInfo(ComboBox1)
        Selgerfil.ordrelogg()
        Label5.Text = valgtVare.hentVareAntall()
    End Sub

    'Knapp Annuller. Se Selgerfil.annulerOrdre.
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Selgerfil.annulerOrdre()
        Selger.Show()
        Me.Close()
    End Sub

    'Knapp Godkjenn. Se sendOrderen
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Selgerfil.sendOrderen()
        Selgerfil.holdavordrenr(Label8.Text)
        Selger.Show()
        Me.Close()
    End Sub

    'Radioknapp Salg. Her fylles comboboksen med varer som er til salgs hvis valgt.
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Selgerfil.fyllbestillingscomboSalg(ComboBox1)
    End Sub

    'Radioknapp Leie. Her fylles comboboksen med varer som er til Leie hvis valgt.
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Selgerfil.fyllbestillingscomboLeie(ComboBox1)
    End Sub

    'Når bestilling lukkes åpnes selger form.
    Private Sub Bestilling_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Selger.Show()
    End Sub

    'Fyller comboboks med forskjellige kurs som er tidgjengelig.
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Selgerfil.fyllbestillingscomboKurs(ComboBox1)
    End Sub
End Class