Imports MySql.Data.MySqlClient
Public Class Form1
    Private systembruker As Ansatt

    'Kjører logg inn funksjonen med brukernavn og passord fra tekstboksene(med beskyttelse for injection).
    'Bruker en enkel kontroll for at ikke viduValgForLogin ikke skal kjøre utenat login informasjon stemmer.
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        systembruker = LogginnFunksjon(TextBox1.Text.Replace("'", "\'"), TextBox2.Text.Replace("'", "\'"))
        If systembruker Is Nothing Then
            Console.WriteLine("Brukeren finnes ikke. Kontakt administrator")
        Else
            Subrutiner.vinduValgForLogin()
            TextBox1.Clear()
            TextBox2.Clear()
        End If

    End Sub

End Class
