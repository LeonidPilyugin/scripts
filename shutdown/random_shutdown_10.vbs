Randomize
x = rnd(1)

if x > 0.9 then
	WScript.Sleep 1800000
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "..\basic scripts\shutdown.vbs"
	Set WshShell = Nothing
End if