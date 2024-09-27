Imports System.Drawing.Printing

Public Class form1
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim Longpaper As Integer

    Private Sub PD_Beginprint(sender As Object, e As Printing.PrintEventArgs) Handles PD.BeginPrint
        Dim Pagesetup As New PageSettings
        Pagesetup.PaperSize = New PaperSize("Custom", 250, 500)
        PD.DefaultPageSettings = Pagesetup
    End Sub
    Private Sub PD_PrintPage(Sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        'font aligment
        Dim right As New StringFormat
        Dim center As New StringFormat

        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "--------------------------------------------------------------"
        e.Graphics.DrawString("Chine Town", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("New York Street 17 Avenue Garden", f10, Brushes.Black, centermargin, 25, center)
        e.Graphics.DrawString("Tel +256785924788 ", f8, Brushes.Black, centermargin, 40, center)

        e.Graphics.DrawString("Invoice ID", f8, Brushes.Black, 0, 60)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 60)
        e.Graphics.DrawString("KGFS1654", f8, Brushes.Black, 75, 60)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 75)
        e.Graphics.DrawString("JUSTUS", f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("08/19/2024", f8, Brushes.Black, 0, 90)

        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 100)

        Dim height As Integer 'DGV Position
        Dim i As Long
        DataGridView1.AllowUserToAddRows = False
        For row As Integer = 0 To DataGridView1.RowCount = 1
            height += 15
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PPD.Document = PD
        PPD.ShowDialog()

    End Sub
End Class


