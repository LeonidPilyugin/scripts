Randomize
x = rnd(1)

if x > 0.99 then
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "..\basic_scripts\dj_watermelon.vbs"
	Set WshShell = Nothing
End if