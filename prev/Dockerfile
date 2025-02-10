# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /backend

# Copy the project file and restore dependencies
COPY backend/*.csproj ./
RUN dotnet restore --verbosity detailed

# Copy the remaining source code and build the application
COPY backend/. ./
RUN dotnet publish -c Release -o /out --verbosity detailed

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /backend
COPY --from=build /out .

# Expose the port the application runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "backend.dll"]
