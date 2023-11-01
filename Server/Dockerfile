FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
 
WORKDIR /source

COPY ./*.csproj .
RUN dotnet restore 
 
# COPY from Source to Container, dot mean current directory for both source and container
COPY . .
RUN dotnet publish -c release -o /app --no-restore
 
FROM mcr.microsoft.com/dotnet/aspnet:7.0
 
RUN apt-get update
RUN apt-get install -y nginx
COPY nginx/default.conf /etc/nginx/sites-enabled/default
 
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:5001
# This will execute during docker run, start NGINX service and run .NET
CMD ["/bin/bash", "-c", "service nginx start && dotnet run"]
