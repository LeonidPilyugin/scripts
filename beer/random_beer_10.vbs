Randomize
x = rnd(1)

if x > 0.9 then
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "beer.vbs"
	Set WshShell = Nothing
End if