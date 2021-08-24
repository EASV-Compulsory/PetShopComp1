FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PetShop.UI/PetShop.UI.csproj", "PetShop.UI/"]
RUN dotnet restore "PetShop.UI/PetShop.UI.csproj"
COPY . .
WORKDIR "/src/PetShop.UI"
RUN dotnet build "PetShop.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetShop.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetShop.UI.dll"]
