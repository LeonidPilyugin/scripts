Set WshShell = CreateObject("WScript.Shell")

interval = InputBox("Введите интервал между включениями в минутах:", "dj watermelon")
interval = Replace(interval, ".", ",")
interval = CDbl(interval) * 60000

pause = InputBox("Введите время, через которое запустится скрипт в минутах:", "dj watermelon")
pause = Replace(pause, ".", ",")
pause = CDbl(pause) * 60000

WScript.Sleep pause

Do
	WshShell.Run "..\basic_scripts\dj_watermelon.vbs"
	WScript.Sleep interval
Loop While True

Set WshShell = Nothing