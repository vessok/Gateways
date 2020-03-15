dotnet clean Src\Gateways.sln
dotnet restore Src\Gateways.sln
dotnet build --configuration Release Src\Gateways.sln
dotnet test --configuration Release Src\Gateways.sln