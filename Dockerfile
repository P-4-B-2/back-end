# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution and project files
COPY backend.sln ./
COPY backend/*.csproj ./backend/

# Restore dependencies
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET Core runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Expose the port the application runs on
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "backend.dll"]



