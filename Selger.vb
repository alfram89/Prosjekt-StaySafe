Imports StaySafe.My.Resources
Imports StaySafe.Subrutiner
Imports StaySafe.Funksjoner
Imports StaySafe.Selgerfil
Public Class Selger

    'Åpner kundebehandlingsskjema når du trykker på button4
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        KundeSkjema.Show()

    End Sub

    'Åpner form1(innloggings formen) når du lukker selger vinduet.
    Private Sub Selger_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub

    'Kjører bestillingskundeinfo som også åpnet bestillingsskjemaet.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Selgerfil.bestillingkundeinfo()
    End Sub
End Class