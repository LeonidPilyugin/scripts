value = InputBox("Введите интервал в минутах:", "beer")
value = Replace(value, ".", ",")
value = CDbl(value) * 60000

WScript.Sleep value

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run "..\basic_scripts\beer.vbs"
Set WshShell = Nothing