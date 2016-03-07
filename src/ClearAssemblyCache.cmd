del assembly.list /Q
dir *bin, *obj, *packages, *.orig /S /b > assembly.list
for /f "usebackq delims=" %%F in (assembly.list) DO (	
	rd /S /Q "%%F" 
)
del assembly.list /Q