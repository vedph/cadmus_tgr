@echo off
echo PRESS ANY KEY TO INSTALL Cadmus Libraries TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Tgr.Parts\bin\Debug\Cadmus.Tgr.Parts.7.0.2.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Tgr.Parts\bin\Debug\Cadmus.Seed.Tgr.Parts.7.0.2.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Tgr.Services\bin\Debug\Cadmus.Tgr.Services.7.0.2.nupkg -source C:\Projects\_NuGet
pause
