# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore as layers
COPY ./*.csproj ./
RUN dotnet restore

# Copy the rest of the files and publish the app
COPY . ./
RUN dotnet publish -c release -o bin/release/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/bin/release/publish .

# Set the entry point
EXPOSE 5045
EXPOSE 8080
EXPOSE 5000
ENTRYPOINT ["dotnet", "backend.dll"]