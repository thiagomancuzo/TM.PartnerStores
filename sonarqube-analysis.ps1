dotnet sonarscanner begin /k:"partner-stores" /d:sonar.cs.opencover.reportsPaths=".\src\UnitTests\TM.PartnerStores.UnitTests\coverage.opencover.xml"
dotnet build
dotnet test .\src\UnitTests\TM.PartnerStores.UnitTests\TM.PartnerStores.UnitTests.csproj  /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet sonarscanner end