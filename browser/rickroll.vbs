Option Explicit

Dim WshShell

Set WshShell = WScript.CreateObject("WScript.Shell")
WshShell.Run "https://www.youtube.com/watch?v=dQw4w9WgXcQ", 1, False
WScript.Sleep(2000)
WshShell.SendKeys " "
WshShell.SendKeys "F"
Set WshShell = Nothing