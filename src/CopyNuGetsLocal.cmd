SET location=%1
del assembly.list /Q
dir Baasic.Client*.nupkg /S /b | findstr /v /i "packages"  | findstr /v /i "symbols" > assembly.list
for /f "usebackq delims=" %%F in (assembly.list) DO (	
    xcopy %%F %location%
)
del assembly.list /Q