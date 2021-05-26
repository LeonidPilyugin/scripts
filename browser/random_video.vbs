Dim i
Dim str
Set WshShell = WScript.CreateObject("WScript.Shell")
Set FSO = CreateObject("Scripting.FileSystemObject")
Set f = FSO.OpenTextFile("data.txt", 1)
Set Dict = CreateObject("Scripting.Dictionary")

i = 0
Do While Not f.AtEndOfStream
  str = f.ReadLine
  Dict.Add i, str
  i = i + 1
Loop

f.Close

Randomize
str = Dict.Item(int(Rnd * i))

WshShell.Run str, 1, False
WScript.Sleep(2000)
WshShell.SendKeys " "
WshShell.SendKeys "F"