'MsgBox "Произошла неустранимая ошибка Windows. Требуется перезагрузка компьютера.", 16+0+4096+65536

Set objExcel = CreateObject("Excel.Application")
With objExcel
.Visible = true
.Workbooks.Add
.Cells(1, 1).Value = "Тест работы VBScript c Excel"
.Cells(2, 1).Value = 1.111
.Cells(3, 1).Value = 2.222
.Cells(4, 1).Value = "=A2+A3"
End With