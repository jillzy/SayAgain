:start
@echo off

:make_exe
robocopy .\Test\bin\debug ..\SayAgainGame\content\bin\debug /E
robocopy .\Test\Art ..\SayAgainGame\content\Art /E
robocopy .\Test\Fonts ..\SayAgainGame\content\Fonts /E
robocopy .\Test\Sounds ..\SayAgainGame\content\Sounds /E
robocopy .\Test\ ..\SayAgainGame\content *.json

REM call asdf.bat -linkfile sa.lnk -target SayAgainGame\content\bin\debug\Test.exe
exit