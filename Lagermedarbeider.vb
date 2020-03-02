Imports StaySafe.Lagermedarbeiderfil
Public Class Lagermedarbeider

    'Her fjernes tekst i listboksen. Derretter henter den tekst fra tekstboks og kjører den igjennom funksjon lagersok
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ListBox1.Items.Clear()
        lagersok(TextBox1.Text)
        Button6.Visible = False
        Button9.Visible = False
    End Sub

    'Når lagermedarbeider lukkes åpnes form1(Innloggingsviduet)
    Private Sub Lagermedarbeider_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub

    'Setter layout for behandling og åpner vareskjema og skjuler lagermedarbeider skjema.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        vareSkjemaBenhandlingLayout()
        Vareskjema.Show()
        Button6.Visible = False
        Button9.Visible = False
    End Sub

    'Setter layout for sletting av vare og åpner vareskjema og skjuler lagermedarbeider skjema.
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        vareSkjemaSlettLayout()
        Vareskjema.Show()
        Button6.Visible = False
        Button9.Visible = False

    End Sub

    'Endrer layout på knapper og henter ut informasjon til listboks fra hentOrdreinfo
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button6.Visible = True
        Button9.Visible = False
        ListBox1.Items.Clear()
        Lagermedarbeiderfil.HentOrdreinfo(ComboBox1, ListBox1)
    End Sub

    'Fyller comboboks med ordre som er klare for pakking og endrer layout av skjemaet.
    Private Sub Lagermedarbeider_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fyllordreforpakk(ComboBox1)
        Button6.Visible = False
        Button9.Visible = False
        Label1.Visible = False
    End Sub

    'Sender orderen til pakking og endrer layout
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        OrdrePakket(ComboBox1)
        Button6.Visible = False
        Button9.Visible = False
        ListBox1.Items.Clear()
    End Sub

    'Fyller/oppdaterer comboboks med ordre klare for pakking. endrer layout
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        fyllordreforpakk(ComboBox1)
        Button6.Visible = False
        Button9.Visible = False
    End Sub

    'Lister ut informasjon om varer som trenger vedlikehold og som er til leie. Endrer layout
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button6.Visible = False
        Button9.Visible = False
        leievarervedlikehold(ListBox1)

    End Sub

    'Henter informasjon om kunden som ønsker å levere inn utstyr og lister dette ut.
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim leiekunde As Kunde = Selgerfil.hentKundeInfo()
        leieInnleveringinfo(ListBox1, leiekunde)
        Button9.Visible = True
    End Sub

    'Denne registrer i databasen at varene er levert tilbake.
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        leverInnLeie()
        Button9.Visible = False
        ListBox1.Items.Clear()
    End Sub
End Class