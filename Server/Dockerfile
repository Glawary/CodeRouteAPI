FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CodeRoute.csproj", "."]
RUN dotnet restore "./CodeRoute.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CodeRoute.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeRoute.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "CodeRoute.dll"]
