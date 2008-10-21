@echo off
cls

@echo E:\Programme\TortoiseSVN\bin\TortoiseProc.exe /command:add /path:"https://pop3pipe.googlecode.com/svn/trunk/POP3Pipe" /logmsg:"test log message" /notempfile /closeonend
E:\Programme\TortoiseSVN\bin\TortoiseProc.exe /command:commit /path:"C:\Dokumente und Einstellungen\Administrator\Eigene Dateien\Visual Studio 2008\Projects\POP3Pipe" /notempfile
pause