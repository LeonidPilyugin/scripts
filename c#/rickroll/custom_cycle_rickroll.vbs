Set WshShell = CreateObject("WScript.Shell")

interval = InputBox("Введите интервал между включениями в минутах:", "error")
interval = Replace(interval, ".", ",")
interval = CDbl(interval) * 60000

pause = InputBox("Введите время, через которое запустится скрипт в минутах:", "error")
pause = Replace(pause, ".", ",")
pause = CDbl(pause) * 60000

WScript.Sleep pause

Do
	WshShell.Run "..\basic_scripts\rickroll.vbs"
	WScript.Sleep 220000
	WshShell.SendKeys("%{F4}")
	WScript.Sleep interval
Loop While True

Set WshShell = Nothing