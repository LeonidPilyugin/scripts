Randomize
x = rnd(1)

if x > 0.999 then
	WScript.Sleep 1800000
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "..\basic_scripts\altf4.vbs"
	Set WshShell = Nothing
End if