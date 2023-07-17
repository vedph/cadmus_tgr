# build
dotnet build -c Debug
# publish library
Remove-Item -Path .\Cadmus.Tgr.Services\bin\Debug\net7.0\publish -Recurse -Force
dotnet publish .\Cadmus.Tgr.Services\Cadmus.Tgr.Services.csproj -c Debug
# rename publish to Cadmus.Tgr.Services and zip
Rename-Item -Path .\Cadmus.Tgr.Services\bin\Debug\net7.0\publish -NewName Cadmus.Tgr.Services
compress-archive -path .\Cadmus.Tgr.Services\bin\Debug\net7.0\Cadmus.Tgr.Services\ -DestinationPath .\Cadmus.Tgr.Services\bin\Debug\net7.0\Cadmus.Tgr.Services.zip -Force
# rename back
Rename-Item -Path .\Cadmus.Tgr.Services\bin\Debug\net7.0\Cadmus.Tgr.Services -NewName publish
