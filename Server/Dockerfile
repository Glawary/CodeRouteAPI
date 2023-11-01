
FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app
 
RUN dotnet restore
RUN dotnet build
 
EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://localhost:5001/
ENV ASPNETCORE_ENVIRONMENT docker
 
ENTRYPOINT dotnet run
