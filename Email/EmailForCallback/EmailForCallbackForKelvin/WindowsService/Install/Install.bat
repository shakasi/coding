@InstallUtil ../WindowsService.exe
@Net Start EmailService
@sc config EmailService start= auto
::@pause