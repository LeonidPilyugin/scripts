value = InputBox("Введите интервал в минутах:", "altf4")
value = Replace(value, ".", ",")
value = CDbl(value) * 60000

WScript.Sleep value

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run "altf4.vbs"
Set WshShell = Nothing