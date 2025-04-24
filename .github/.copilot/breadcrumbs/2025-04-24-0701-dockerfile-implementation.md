# Dockerfile Implementation for Bookstore.Api

## Requirements
- Create a Dockerfile for the Bookstore.Api project
- Ensure the Dockerfile follows best practices for .NET applications
- Configure the container to run the API on the correct port (5223)
- Optimize for production use with appropriate multi-stage builds

## Additional comments from user
- The Dockerfile in `src\backend\Bookstore.Api` is currently empty
- The application is configured to run on port 5223 as per launchSettings.json

## Plan
1. Review the current Bookstore.Api project structure and requirements
2. Research best practices for containerizing .NET applications
3. Create a multi-stage Dockerfile for the Bookstore.Api project
   - Use SDK image for building
   - Use runtime image for the final container to reduce size
4. Configure the container to expose the correct port
5. Include appropriate health checks and configuration

### Task List
- [x] Review the Bookstore.Api project structure
- [x] Identify all dependencies required for the build
- [x] Create a multi-stage Dockerfile
- [x] Configure proper port exposure
- [x] Add appropriate labels and metadata
- [x] Document the Dockerfile implementation
- [ ] Test the Dockerfile to ensure it builds and runs correctly

## Decisions
- Will use a multi-stage build approach to minimize final image size
- Will use the official Microsoft .NET SDK and ASP.NET Runtime images
- Will expose port 5223 to match the application configuration
- Will use .NET 9.0 as specified in the project file
- Will ensure the build includes all project dependencies (Bookstore.Data and Bookstore.RequestProcessing)
- Set a non-root user (UID 1000) for security best practices
- Configure environment variables for production

## Implementation Details
The Dockerfile follows Microsoft's best practices for containerizing .NET applications:

1. **Build Stage**:
   - Use the .NET SDK 9.0 image for building
   - Copy and restore all project dependencies first for better layer caching:
     ```dockerfile
     COPY ["Bookstore.Api/Bookstore.Api.csproj", "Bookstore.Api/"]
     COPY ["Bookstore.Data/Bookstore.Data.csproj", "Bookstore.Data/"]
     COPY ["Bookstore.Dtos/Bookstore.Dtos.csproj", "Bookstore.Dtos/"]
     COPY ["Bookstore.RequestProcessing/Bookstore.RequestProcessing.csproj", "Bookstore.RequestProcessing/"]
     
     RUN dotnet restore "Bookstore.Api/Bookstore.Api.csproj"
     ```
   - Build the application with Release configuration
   - Publish the application to a directory

2. **Runtime Stage**:
   - Use the smaller ASP.NET Runtime 9.0 image for the final container
   - Copy only the published files from the build stage
   - Set appropriate environment variables for production:
     ```dockerfile
     ENV ASPNETCORE_ENVIRONMENT=Production
     ENV ASPNETCORE_URLS=http://+:5223
     ```
   - Run as non-root user (UID 1000) for security
   - Expose port 5223
   - Configure the entry point to run the application

## Changes Made
- Created a new Dockerfile in the src/backend/Bookstore.Api directory
- Implemented a multi-stage build process that optimizes for size and security
- Configured the container to use port 5223 to match the application settings
- Added security best practices like running as a non-root user

## Before/After Comparison
**Before:**
- Empty Dockerfile with no implementation

**After:**
- Fully implemented multi-stage Dockerfile optimized for production
- Proper handling of project dependencies
- Secure configuration with non-root user
- Correct port configuration (5223)
- Optimized for smaller image size by using multi-stage builds

## References
- [Microsoft's .NET Docker Best Practices](https://docs.microsoft.com/en-us/dotnet/core/docker/build-container)
- VS Code Dev Container configuration breadcrumb: Used to determine the correct port configuration (5223)
- Project Structure: Found that it targets .NET 9.0 and has dependencies on Bookstore.Data and Bookstore.RequestProcessing projects