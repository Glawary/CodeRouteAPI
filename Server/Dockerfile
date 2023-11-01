FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
 
RUN dotnet build
 
EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://localhost:5001/
ENV ASPNETCORE_ENVIRONMENT docker
 
ENTRYPOINT dotnet run
