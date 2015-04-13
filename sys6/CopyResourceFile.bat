@echo off
REM 进入当前批处理文件所在目录
cd %~dp0
REM 进入上级目录
cd ..
REM 复制资源/配置文件
REM set srcPath=6.RC
REM xcopy /E /Y ResourceFiles\%srcPath% %~dp0
REM xcopy /E /Y ResourceFiles\%srcPath% %~dp0\bin\Debug\
REM xcopy /E /Y ResourceFiles\%srcPath% %~dp0\bin\Release\
REM if errorlevel 0 (echo 复制资源/配置文件成功!)^
REM else (call :PrintError 复制资源/配置文件失败，请检查!)
REM call :PrintSeperator

REM 复制各个系统均需要的文件
xcopy /E /Y ResourceFiles\AllRequired %~dp0
xcopy /E /Y ResourceFiles\AllRequired %~dp0\bin\Debug\
xcopy /E /Y ResourceFiles\AllRequired %~dp0\bin\Release\

if errorlevel 0 (echo 复制通用资源/配置文件成功!)^
else (call :PrintError 复制通用资源/配置文件失败，请检查!)
call :PrintSeperator

exit

:PrintError
echo %1
pause

:PrintSeperator
echo ======