Imports StaySafe.My.Resources
Imports StaySafe.Kunde
Public Class KundeSkjema

    'Fyller skjemaet med kundeinformasjon som er hentet med funksjon hentKundeInfo.
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim hentetKunde As Kunde
        Try
            hentetKunde = Selgerfil.hentKundeInfo()

            TextBox1.Text = hentetKunde.hentKundeid()
            TextBox2.Text = hentetKunde.hentKundeFornavn()
            TextBox3.Text = hentetKunde.hentKundeEtternavn()
            TextBox4.Text = hentetKunde.hentKundeTelefonummer()
            TextBox5.Text = hentetKunde.hentKundeAdresse()
            TextBox6.Text = hentetKunde.hentKundeEpost()
            TextBox7.Text = hentetKunde.hentKundeKjoptfor()
        Catch ex As Exception
            Console.WriteLine("Operatør avbrøt hentKundeInfo.")
        End Try
    End Sub

    'Lager et Kunde objekt fra informasjonen i tekstboksene. Og kjører objektet igjennom sub lagrekundeinfo.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sendKunde As New Kunde(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text)
        Selgerfil.lagreKundeInfo(sendKunde)
    End Sub
End Class