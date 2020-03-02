Imports StaySafe.My.Resources
Imports StaySafe.Subrutiner
Imports MySql.Data.MySqlClient
Module Funksjoner
    Public Ansattsominnlogget As Ansatt

    'Åpner tilkoblingen til egen database for innlogging.
    Public Function logginnString()
        Dim tilkobling As New MySqlConnection("Server=mysql.stud.iie.ntnu.no;Database=xxx;Uid=xxx;Pwd=xxx")
        Return tilkobling
    End Function

    ' Åpner tilkoblingen til StaySafe databasen.
    Public Function hentTilkobling()
        Dim tilkobling As New MySqlConnection("Server=mysql.stud.iie.ntnu.no;Database=g_oops_t14;Uid=g_oops_t14;Pwd=2udhdPNp")
        Return tilkobling
    End Function

    ' Funksjon for å verifisere innloggings informasjon returnerer en Ansatt hvis godkjent.
    Public Function LogginnFunksjon(ByVal brukernavn As String, ByVal passord As String) As Ansatt
        Dim sql As New MySqlCommand("SELECT * FROM StaySafeLogIn WHERE Brukernavn='" & brukernavn & "' and Passord='" & passord & "'", logginnString())

        Dim da As New MySqlDataAdapter
        Dim interTabell As New DataTable
        da.SelectCommand = sql
        da.Fill(interTabell)


        If interTabell.Rows.Count = 0 Then
            MessageBox.Show("Feil brukernavn eller passord")
        Else
            For Each rad As DataRow In interTabell.Rows
                Ansattsominnlogget = New Ansatt(rad("ID"), rad("Stilling"))
                Return Ansattsominnlogget
            Next
        End If

        Return Nothing
    End Function

    ' Funksjon for å kommunisere med databasen og returnerer en datatable.
    Public Function Query(ByVal sql As String) As DataTable
        Dim tilkobling As New MySqlConnection
        Dim data As New DataTable

        tilkobling = hentTilkobling()
        Try
            tilkobling.Open()
            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = New MySqlCommand(sql, tilkobling)
            adapter.Fill(data)
            tilkobling.Close()
        Catch queryFeil As Exception
            MessageBox.Show("Det oppsto en feil: " & queryFeil.Message)
        Finally
            tilkobling.Dispose()
        End Try
        Return data
    End Function
End Module
