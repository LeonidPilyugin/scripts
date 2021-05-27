Option Explicit

Dim WshShell

Set WshShell = WScript.CreateObject("WScript.Shell")
WshShell.Run "http://youtube.com", 1, False
Set WshShell = Nothing