@echo off
echo PUSH PACKAGES TO NUGET
prompt
set nu=C:\Exe\nuget.exe
set src=-Source https://api.nuget.org/v3/index.json

%nu% push .\Cadmus.Tgr.Parts\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Tgr.Services\bin\Debug\*.nupkg %src% -SkipDuplicate
%nu% push .\Cadmus.Seed.Tgr.Parts\bin\Debug\*.nupkg %src% -SkipDuplicate
echo COMPLETED
echo on
