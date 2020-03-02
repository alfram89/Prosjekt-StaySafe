Imports StaySafe.Instroktorfil
Public Class Instruktor

    'Gjør om datoen("yyyy/MM/dd") slik at databasen kan forstå den. Deretter skriver den ut i listbox
    'og kjører funksjonen registrerOpptattinstroktor med den valgte datoen.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valgtdato As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        ListBox1.Items.Add("Du har valgt at du er opptatt den: " & registrerOpptattinstroktor(valgtdato))

    End Sub

    'Kjører funksjon skrivListbox med datatable nestekurs
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        skrivListbox(nesteKurs)
    End Sub

    'Kjører funksjon skrivListbox med datatable treNesteKurs
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        skrivListbox(treNesteKurs)
    End Sub

    'Kjører funksjon skrivListbox med datatable alleNesteKurs
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        skrivListbox(alleNesteKurs)
    End Sub

    'Åpner form1 igjen når du lukker instroktor.
    Private Sub Instruktor_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Form1.Show()
    End Sub
End Class