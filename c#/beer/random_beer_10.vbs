Randomize
x = rnd(1)

if x > 0.9 then
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "..\basic_scripts\beer.vbs"
	Set WshShell = Nothing
End if