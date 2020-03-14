
dotnet sonarscanner begin /k:"partner-stores" /d:sonar.cs.opencover.reportsPaths="$PSScriptRoot\..\src\UnitTests\TM.PartnerStores.UnitTests\coverage.opencover.xml" /d:sonar.host.url="http://192.168.99.100:9000"
dotnet build "$PSScriptRoot\..\"
dotnet test "$PSScriptRoot\..\src\UnitTests\TM.PartnerStores.UnitTests\TM.PartnerStores.UnitTests.csproj"  /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet sonarscanner end