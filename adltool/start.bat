@echo off

FOR /f "tokens=2 delims==" %%G IN ('findstr /L startBatAdltoolArgs adltool.ini.flattened.ini') DO call set adltool_args=%%G

adltool.exe %adltool_args%