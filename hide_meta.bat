
@echo off
title Hide Unity Meta Files

cd /d "%~dp0"

echo Hiding Unity .meta files...
attrib +h /s /d "*.meta"

echo.
echo Done! All .meta files are now hidden.
pause