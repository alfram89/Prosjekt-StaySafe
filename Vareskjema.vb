Imports StaySafe.My.Resources
Imports StaySafe.Varer

Public Class Vareskjema

    'Fyller comboboks og huker av radiobutton 1 som default.
    Private Sub Vareskjema_Load(sender As Object, e As EventArgs) Handles Me.Load
        Lagermedarbeiderfil.FyllCombobox(ComboBox1)
        RadioButton1.Checked = True
    End Sub

    'Henter vareinformasjon via hentvareinfo og fyller tekstbokser.
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim dinValgteVare As Varer = Lagermedarbeiderfil.hentVareInfo(ComboBox1)
        Try
            TextBox1.Text = dinValgteVare.hentVareID()
            TextBox2.Text = dinValgteVare.hentVareNavn()
            TextBox3.Text = dinValgteVare.hentVareType()
            TextBox5.Text = dinValgteVare.hentVareAntall()
            TextBox6.Text = dinValgteVare.hentVareKommentar()
            TextBox7.Text = dinValgteVare.hentVarePris()
            TextBox8.Text = dinValgteVare.hentVarekostnad()
        Catch ex As Exception
            Console.WriteLine("Noe gikk galt, kontakt admin")
        End Try
    End Sub

    'Skjekker hvem radioknapp som er huket av og lager dette som en vare sammen med tekstboksinformasjon derretter sendes varen til lagring.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim status = Lagermedarbeiderfil.vareSkjemaRadiobuttonCheck()
        Dim lagreVare As New Varer(TextBox1.Text, TextBox2.Text, TextBox3.Text, status, TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text)
        Lagermedarbeiderfil.lagreVareInfo(lagreVare)
    End Sub

    'klarerer comboboks og kjører slettvarefralager
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox1.Items.Clear()
        Lagermedarbeiderfil.slettVareFraLager()

    End Sub

    'kjører vareskjemaclear.
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Lagermedarbeiderfil.vareSkjemaClear()
    End Sub
End Class