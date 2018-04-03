@InstallUtil ../WindowsService.exe
@Net Start ServiceShaka
@sc config ServiceShaka start= auto
::@pause