Randomize
x = rnd(1)

if x > 0.99 then
	WScript.Sleep 1800000
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "error.vbs"
	Set WshShell = Nothing
End if