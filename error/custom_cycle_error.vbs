interval = InputBox("Введите интервал между включениями в минутах:", "error")
interval = Replace(interval, ".", ",")
interval = CDbl(interval) * 60000

pause = InputBox("Введите время, через которое запустится скрипт в минутах:", "error")
pause = Replace(pause, ".", ",")
pause = CDbl(pause) * 60000

WScript.Sleep pause

Do
	x = MsgBox("Произошла неустранимая ошибка Windows. Требуется перезагрузка компьютера.", 16+0+4096+65536, "Сообщение об ошибке")
	WScript.Sleep interval
Loop While True