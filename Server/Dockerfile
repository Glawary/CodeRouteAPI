<<<<<<< HEAD
# укажем, на основе какого образа будем 
# делать наш контейнер. Для сборки приложения используем dotnet-sdk 
# и назовём этот образ builder
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder 
# укажем директорию для нашего приложения внутри контейнера
WORKDIR /Application

# Скопируем все файлы из проекта в файловую систему контейнера
COPY . ./
# Запустим restore для загрузки зависимостей
RUN dotnet restore
# Опубликуем собранный dll в папку "output"
RUN dotnet publish -c Release -o output

# Теперь соберём образ, в котором наше приложение 
# будет запущено. Для запуска приложения достаточно
# среды выполнения aspnet, без sdk
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /Application
# Скопируем файлы приложения из предыдущего образа 
COPY --from=builder /Application/output .
# укажем команду, которая будет запускать приложение
<<<<<<< HEAD
EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "CodeRoute.dll"]
=======
ENTRYPOINT ["dotnet", "run"]
=======
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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
ENTRYPOINT ["dotnet", "CodeRoute.dll"]
>>>>>>> 972a5f04d34e25a551b9fd95054a6f81752db562
>>>>>>> AlexSmirno-main
