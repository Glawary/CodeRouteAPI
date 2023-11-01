FROM mcr.microsoft.com/dotnet/aspnet:7.0
 
RUN apt-get update
RUN apt-get install -y nginx
 
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:5001
# This will execute during docker run, start NGINX service and run .NET
CMD ["/bin/bash", "-c", "service nginx start && dotnet run"]
