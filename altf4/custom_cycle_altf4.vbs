Set WshShell = CreateObject("WScript.Shell")

interval = InputBox("Введите интервал между включениями в минутах:", "altf4")
interval = Replace(interval, ".", ",")
interval = CDbl(interval) * 60000

pause = InputBox("Введите время, через которое запустится скрипт в минутах:", "altf4")
pause = Replace(pause, ".", ",")
pause = CDbl(pause) * 60000

WScript.Sleep pause

Do
	WshShell.Run "altf4.vbs"
	WScript.Sleep interval
Loop While True

Set WshShell = Nothing