net stop "EmailService"
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil /u %~dp0WindowsService.exe
pause