@echo off
echo BUILD Cadmus Tgr Packages
del .\Cadmus.Tgr.Parts\bin\Debug\*.nupkg
del .\Cadmus.Tgr.Parts\bin\Debug\*.snupkg
del .\Cadmus.Tgr.Services\bin\Debug\*.nupkg
del .\Cadmus.Tgr.Services\bin\Debug\*.snupkg
del .\Cadmus.Seed.Tgr.Parts\bin\Debug\*.nupkg
del .\Cadmus.Seed.Tgr.Parts\bin\Debug\*.snupkg

cd .\Cadmus.Tgr.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Tgr.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Tgr.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
