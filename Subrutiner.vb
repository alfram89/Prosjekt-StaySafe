Imports MySql.Data.MySqlClient
Imports StaySafe.My.Resources
Imports StaySafe.Ansatt

Public Class Subrutiner

    'Leser ut stillingen til den som prøver å logge inn, deretter åpner det rette viduet for den ansatte.
    Public Shared Sub vinduValgForLogin()
        If Ansattsominnlogget.hentStilling = "Salg" Then
            Selger.Show()
            Form1.Hide()
        ElseIf Ansattsominnlogget.hentStilling = "Ledelse" Then
            Ledelse.Show()
            Form1.Hide()
        ElseIf Ansattsominnlogget.hentStilling = "Lager" Then
            Lagermedarbeider.Show()
            Form1.Hide()
        ElseIf Ansattsominnlogget.hentStilling = "Instruktor" Then
            Instruktor.Show()
            Form1.Hide()
        Else
            MessageBox.Show("Brukeren du har logget inn som har ingen stilling registrert")
        End If



    End Sub



End Class
