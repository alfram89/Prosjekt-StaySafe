Imports StaySafe.Lagermedarbeiderfil
Imports StaySafe.LedelseFil
Public Class Ledelse

    'åpner vareskjema
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Vareskjema.Show()

    End Sub

    'Legger ut informasjon i listboks om dagens statestikk.
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ListBox1.Items.Add("I dag har det vært " & dagensDato() & " salg.")
        ListBox1.Items.Add("I dag er det solgt for: " & finnDagensOmsettning() & " kr.")
        ListBox1.Items.Add("----")
    End Sub

    'Velger en dato fra dateTimepicker og sender til funksjonene
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim valgtdato As String = DateTimePicker1.Value.ToString("dd/MM/yyyy")

        ListBox1.Items.Add("På datoen " & valgtdato & " har det vært " & antSalgForDato(valgtdato) & " salg.")
        ListBox1.Items.Add("Da var det solgt for: " & finnOmsettningPerDato(valgtdato) & " kr.")
        ListBox1.Items.Add("..............................................................")
    End Sub

    'Lister ut statestikk om spesifikk selger
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim valgtID As Integer = Convert.ToInt32(TextBox1.Text)
            ListBox1.Items.Add("Ansatt ID: " & valgtID & " har gjort " & antallSalgAnsatt(valgtID) & " salg.")
            ListBox1.Items.Add("Selgeren har solgt for: " & ansattSolgtFor(valgtID) & " kr.")
            ListBox1.Items.Add("----")
        Catch ex As Exception
            MessageBox.Show("Skriv inn IDen med tall")
        End Try
    End Sub

    'Åpner kundeskjema
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        KundeSkjema.Show()
    End Sub

    'fjerner tekst i listboks
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
    End Sub

    'Viser ansattskjema
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AnsattSkjema.Show()
    End Sub

    'kjører subrutinen mestsolgtisalg
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LedelseFil.mestSolgetISalg()

    End Sub

    'Kjører subrutinen mestleidevare
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LedelseFil.mestLeideVare()
    End Sub

    'Viser skjema hvor man kan redigere og legge til kurs i databasen
    'for vidre implimentering
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        KursSkjema.Show()
    End Sub
End Class