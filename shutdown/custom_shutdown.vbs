value = InputBox("Введите интервал в минутах:", "shutdown")
value = Replace(value, ".", ",")
value = CDbl(value) * 60

WScript.Sleep value

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run "shutdown.vbs"
Set WshShell = Nothing