# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Publish the application (Release mode)
RUN dotnet publish -c Release -o /app/out

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Set environment variables for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80

# Copy the published output from the builder stage
COPY --from=builder /app/out ./

# Expose the application port (80 for Azure Container Apps)
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
