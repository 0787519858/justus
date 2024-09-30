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
        line = "--------------------------------------------------------------------------"
        e.Graphics.DrawString("China Town Store", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("Plot39 Shimoni Rd, Kampala", f10, Brushes.Black, centermargin, 25, center)
        e.Graphics.DrawString("Tel +256785924788 ", f8, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("wokonicholasanyoli@gmail.com ", f8, Brushes.Black, centermargin, 50, center)

        e.Graphics.DrawString("Invoice ID", f8, Brushes.Black, 0, 60)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 60)
        e.Graphics.DrawString("KGFS1654", f8, Brushes.Black, 75, 60)

        e.Graphics.DrawString("Transaction ID", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 85, 75)
        e.Graphics.DrawString(transactionID, f8, Brushes.Black, 100, 75) ' Display transaction ID

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 90)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 90)
        e.Graphics.DrawString("ICON GROUP", f8, Brushes.Black, 70, 90)

        e.Graphics.DrawString("Date" & Date.Now(), f8, Brushes.Black, 0, 100)

        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 110)

        Dim height As Integer 'DGV Position
        Dim i As Long
        DataGridView1.AllowUserToAddRows = False
        For row As Integer = 0 To DataGridView1.RowCount - 1
            height += 15
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(1).Value.ToString, f10, Brushes.Black, 0, 100 + height)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(0).Value.ToString, f10, Brushes.Black, 25, 100 + height)
            i = DataGridView1.Rows(row).Cells(2).Value
            DataGridView1.Rows(row).Cells(2).Value = Format(i, "##,##0")
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(2).Value.ToString, f10, Brushes.Black, rightmargin, 100 + height, right)
        Next
        Dim height2 As Integer
        height2 = 110 + height
        Sumprice() 'call the sumprice sub
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Subtotal: " & t_price.ToString("UGX##,##0"), f10b, Brushes.Black, rightmargin, 15 + height2, right)
        e.Graphics.DrawString("Discount: " & Discount.ToString("UGX##,##0"), f10b, Brushes.Black, rightmargin, 30 + height2, right)
        e.Graphics.DrawString("Tax: " & taxAmount.ToString("UGX##,##0"), f10b, Brushes.Black, rightmargin, 45 + height2, right)

        Dim finalTotal As Long = t_price - Discount + taxAmount
        e.Graphics.DrawString("Total Amount Due: " & finalTotal.ToString("UGX##,##0"), f10b, Brushes.Black, rightmargin, 60 + height2, right)
        ' Display payment method
        e.Graphics.DrawString("Payment Method: " & paymentMethod, f10, Brushes.Black, 0, 80 + height2)
        e.Graphics.DrawString("Served by: ICON GROUP", f10b, Brushes.Black, centermargin, 90 + height2, center)
        e.Graphics.DrawString("Thank you for Shopping", f10b, Brushes.Black, centermargin, 100 + height2, center)


    End Sub
    Dim t_price As Long
    Dim t_Qty As Long
    Dim Discount As Long
    Dim taxRate As Decimal = 0.07D ' 7% tax rate
    Dim taxAmount As Long
    Dim paymentMethod As String = "Cash" ' Default payment method
    Dim transactionID As String = "TXN-" & Guid.NewGuid().ToString().Substring(0, 8) ' Generate a unique transaction ID


    Sub Sumprice()
        Dim countprice As Long = 0
        For rowitem As Long = 0 To DataGridView1.RowCount - 1
            countprice = countprice + Val(DataGridView1.Rows(rowitem).Cells(2).Value * Val(DataGridView1.Rows(rowitem).Cells(1).Value))
        Next
        t_price = countprice

        Dim Countqty As Long = 0
        For rowitem As Long = 0 To DataGridView1.RowCount - 1
            countqty = countqty + Val(DataGridView1.Rows(rowitem).Cells(1).Value)
        Next
        t_Qty = Countqty

        ' Calculate Discount (e.g., 10% of the total price)
        If t_price > 100 Then
            Discount = (10 / 100) * t_price ' 10% discount for total price over 100
        ElseIf t_Qty > 5 Then
            Discount = (5 / 100) * t_price ' 5% discount for count over 5
        Else
            Discount = 0 ' No discount
        End If

        ' Calculate Tax
        taxAmount = Math.Round((t_price - Discount) * taxRate) ' Tax based on the subtotal after discount
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PPD.Document = PD
        PPD.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class

