FROM mcr.microsoft.com/dotnet/core/sdk:5.0 as build
WORKDIR /home/work
COPY . .
RUN dotnet build
RUN dotnet publish -c Release -o out TylerMart.Client/TylerMart.Client.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
WORKDIR /home/work
COPY --from=build /home/work/out .
CMD ["dotnet" "TylerMart.Client.dll"]
