# 2025-04-23-1649-dotnet-devcontainer-setup.md

## Requirements
- Create a Dev Container configuration for the Bookstore project
- Configure the Dev Container to use .NET 9
- Ensure the API project can run within the Dev Container
- Follow VS Code Dev Container best practices

## Additional comments from user
- None yet

## Plan
### Phase 1: Setup Dev Container Configuration Files
- [x] Task 1.1: Create devcontainer.json file with .NET 9 configuration
- [x] Task 1.2: Create Dockerfile for the Dev Container
- [x] Task 1.3: Add development tools and extensions to the Dev Container

### Phase 2: Configure Dev Container for Running the API
- [x] Task 2.1: Set up port forwarding for the API
- [x] Task 2.2: Configure environment variables
- [x] Task 2.3: Set up dev container post-create commands

### Success Criteria
- Dev Container can be built and started
- .NET 9 SDK is available within the container
- API project can be run within the container
- Developer experience is smooth with necessary extensions

## Decisions
- Used the official Microsoft .NET SDK image as base (mcr.microsoft.com/dotnet/sdk:9.0)
- Included essential VS Code extensions for .NET development (ms-dotnettools.csharp, etc.)
- Set up port forwarding for ports 5000 and 5001 to access the API
- Added post-create command to restore NuGet packages automatically

## Implementation Details
### .devcontainer/devcontainer.json
- Created configuration file that specifies:
  - Base image variant (.NET 9.0)
  - VS Code extensions for .NET development
  - Port forwarding for the API (5000, 5001)
  - Post-create command to restore NuGet packages
  - Non-root user configuration

### .devcontainer/Dockerfile
- Created Dockerfile with:
  - Base image: mcr.microsoft.com/dotnet/sdk:9.0
  - Installation of necessary OS packages
  - Setup of non-root user (vscode)
  - Additional tools for .NET development

### Supporting Files
- Added common-debian.sh script to setup the container environment

## Changes Made
- Created .devcontainer directory to store Dev Container configuration
- Added devcontainer.json with proper .NET 9 configuration
- Added Dockerfile with proper base image and dependencies
- Added library-scripts/common-debian.sh for container user setup
- Configured port forwarding for the API (5000/5001)
- Set up automatic NuGet package restoration

## Before/After Comparison
Before:
- No Dev Container configuration
- Development environment setup manually by each developer
- Inconsistent development environment across team

After:
- Reproducible development environment via Dev Container
- .NET 9 SDK available within the container
- API can be run directly within the container
- Consistent development environment with all necessary extensions pre-installed
- Automatic setup of ports, dependencies, and other configurations

## References
- VS Code Dev Container documentation: https://code.visualstudio.com/docs/devcontainers/create-dev-container
- VS Code Dev Container tutorial: https://code.visualstudio.com/docs/devcontainers/tutorial

