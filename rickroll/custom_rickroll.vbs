value = InputBox("Введите интервал в минутах:", "rickroll")
value = Replace(value, ".", ",")
value = CDbl(value) * 60000

WScript.Sleep value

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run "rickroll.vbs"
Set WshShell = Nothing