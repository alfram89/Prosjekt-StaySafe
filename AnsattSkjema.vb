Imports StaySafe.My.Resources
Imports StaySafe.AnsattInfo
Public Class AnsattSkjema
    'Lukker ansattskjema
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    'Fyller inn informasjon i tekstboksene fra hentansattinfo
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim hentetAnsattInfo As AnsattInfo

        hentetAnsattInfo = LedelseFil.hentAnsattInfo()
        TextBox1.Text = hentetAnsattInfo.hentAnsattID()
        TextBox2.Text = hentetAnsattInfo.hentFornavn()
        TextBox3.Text = hentetAnsattInfo.hentEtternavn()
        TextBox5.Text = hentetAnsattInfo.hentAdresse()
        TextBox4.Text = hentetAnsattInfo.hentTlf()
        TextBox6.Text = hentetAnsattInfo.hentEpost()
        TextBox7.Text = hentetAnsattInfo.hentStatus()
        TextBox8.Text = hentetAnsattInfo.hentStilling()
        TextBox9.Text = hentetAnsattInfo.hentSolgt_for()
        TextBox10.Text = hentetAnsattInfo.hentAntall_salg
    End Sub

    'Sender med informasjon fra tekstboksene til en kunde deretter sendes den nye/oppdaterte kunden igjennom lagreAnsattInfo
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sendAnsatt As New AnsattInfo(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text, TextBox9.Text, TextBox10.Text)
        LedelseFil.lagreAnsattInfo(sendAnsatt)
    End Sub

    'Endrer tekstboks teksten til Ansatt når radiobutton 4 er huket av.
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        TextBox7.Text = "Ansatt"
    End Sub

    'Endrer tekstboks teksten til Innaktiv når radiobutton 5 er huket av.
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        TextBox7.Text = "Innaktiv"
    End Sub

    'Endrer tekstboks teksten til Salg når radiobutton 1 er huket av.
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        TextBox8.Text = "Salg"
    End Sub

    'Endrer tekstboks teksten til Lager når radiobutton 2 er huket av.
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TextBox8.Text = "Lager"
    End Sub

    'Endrer tekstboks teksten til Ledelse når radiobutton 3 er huket av.
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        TextBox8.Text = "Ledelse"
    End Sub
End Class