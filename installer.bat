@echo off

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\SA.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%userprofile%\Desktop\SayAgainGame\content\bin\debug\SayAgain.exe" >> %SCRIPT%
echo oLink.IconLocation = "%userprofile%\Desktop\SayAgainGame\content\Art\exe_icon.ico" >> %SCRIPT%
echo oLink.WorkingDirectory = "%userprofile%\Desktop\SayAgainGame\content\bin\debug" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%
start /b "" cmd /c del "%~f0"&exit /b