Randomize
x = rnd(1)

if x > 0.99 then
	Set WshShell = CreateObject("WScript.Shell")
	WshShell.Run "beer.vbs"
	Set WshShell = Nothing
End if