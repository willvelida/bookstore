# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Bookstore.Api/Bookstore.Api.csproj", "Bookstore.Api/"]
COPY ["Bookstore.Data/Bookstore.Data.csproj", "Bookstore.Data/"]
COPY ["Bookstore.Dtos/Bookstore.Dtos.csproj", "Bookstore.Dtos/"]
COPY ["Bookstore.RequestProcessing/Bookstore.RequestProcessing.csproj", "Bookstore.RequestProcessing/"]

RUN dotnet restore "Bookstore.Api/Bookstore.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR "/src/Bookstore.Api"
RUN dotnet build "Bookstore.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Bookstore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5223

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the user to non-root for security
USER 1000

# Expose the port
EXPOSE 5223

# Set the entrypoint
ENTRYPOINT ["dotnet", "Bookstore.Api.dll"]