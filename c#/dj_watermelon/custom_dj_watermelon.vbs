value = InputBox("Введите интервал в минутах:", "dj_watermelon")
value = Replace(value, ".", ",")
value = CDbl(value) * 60000

WScript.Sleep value

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run "..\basic_scripts\dj_watermelon.vbs"
Set WshShell = Nothing