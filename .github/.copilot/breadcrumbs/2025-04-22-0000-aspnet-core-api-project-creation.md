# ASP.NET Core API Project Creation

## Requirements
- Create a new ASP.NET Core API project using .NET 9
- Create the project in a `src/backend` folder
- Name the solution `Bookstore` and the project `Bookstore.Api`
- Add Swagger/OpenAPI endpoint following the latest .NET 9 guidelines
- Return hardcoded responses from request processors (no database context yet)
- Follow the architecture described in the API specification

## Additional comments from user
- User requested to implement API based on the spec in 0002-create-api-project.md
- No database implementation is required at this stage

## Plan
### Phase 1: Project Setup
1. Create the necessary directory structure
2. Create a new .NET 9 solution named `Bookstore`
3. Add a new ASP.NET Core API project named `Bookstore.Api`

### Phase 2: Project Structure Implementation
1. Set up initial project structure following the specs:
   - Create necessary project folders for the architecture
   - Set up Program.cs with Minimal API configuration
   - Configure OpenAPI/Swagger according to .NET 9 guidance

### Phase 3: Implement API Endpoints
1. Create DTOs (Data Transfer Objects) for our API responses
2. Implement request processors with hardcoded data
3. Set up API endpoints for the book-related operations

### Phase 4: Testing
1. Verify the API runs correctly
2. Confirm Swagger/OpenAPI endpoint is working
3. Test the API endpoints return the expected hardcoded responses

## Decisions
- Using .NET 9 as specified
- Following Minimal API approach as outlined in the specification
- No database implementation at this stage, using hardcoded data instead
- Following the layered architecture from the spec with separation of concerns
- Using a simple repository pattern with hardcoded data rather than implementing a full database context
- Using Swashbuckle.AspNetCore package for Swagger/OpenAPI implementation

## Implementation Details
### Phase 1: Project Setup
- Created the `src/backend` directory structure
- Created a new .NET 9 solution named `Bookstore` using `dotnet new sln -n Bookstore`
- Created a new ASP.NET Core API project named `Bookstore.Api` using `dotnet new webapi -minimal`
- Added the API project to the solution using `dotnet sln add Bookstore.Api/Bookstore.Api.csproj`

### Phase 2: Project Structure Implementation
- Created additional projects to support the layered architecture:
  - `Bookstore.Data`: Contains domain entities and data access
  - `Bookstore.RequestProcessing`: Contains request processors and business logic
  - `Bookstore.Dtos`: Contains Data Transfer Objects for API responses
- Set up project references according to architecture:
  - `Bookstore.Api` references `Bookstore.Data` and `Bookstore.RequestProcessing`
  - `Bookstore.RequestProcessing` references `Bookstore.Data` and `Bookstore.Dtos`
- Configured Program.cs with minimal API endpoints for books and authors
- Set up OpenAPI/Swagger using Swashbuckle.AspNetCore package

### Phase 3: Implement API Endpoints
- Created domain entities in the Data project:
  - `Book.cs`: Book domain entity with properties like BookId, Title, ISBN, Price
  - `Author.cs`: Author domain entity with properties like AuthorId, Name, Biography
- Created DTOs in the Dtos project:
  - `BookDto.cs`: Data transfer object for Book entities
  - `AuthorDto.cs`: Data transfer object for Author entities
- Implemented a simple repository (`BookstoreRepository.cs`) with hardcoded data
- Created request processors for API operations:
  - `GetBookRequestProcessor.cs`: Handles requests to get a book by ID
  - `GetAuthorRequestProcessor.cs`: Handles requests to get an author by ID
- Set up API endpoints in Program.cs using minimal API syntax

### Phase 4: Testing
- Successfully built the solution with all required packages
- Verified that the API runs correctly at http://localhost:5223
- Confirmed Swagger UI is working:
  - `/swagger` redirects to `/swagger/index.html`
  - `/swagger/index.html` displays the OpenAPI documentation
- Tested API endpoints:
  - `/api/books/1` returns "To Kill a Mockingbird" with a 200 OK response
  - `/api/authors/2` returns George Orwell with his books (1984 and Animal Farm) with a 200 OK response

## Changes Made
- Created new directories and files:
  - `src/backend/Bookstore.sln`
  - `src/backend/Bookstore.Api/` with Program.cs and project files
  - `src/backend/Bookstore.Data/` with Book.cs, Author.cs, and BookstoreRepository.cs
  - `src/backend/Bookstore.Dtos/` with BookDto.cs and AuthorDto.cs
  - `src/backend/Bookstore.RequestProcessing/Features/` with GetBook and GetAuthor feature folders
- Updated Program.cs to use minimal API endpoints for books and authors
- Added Swagger/OpenAPI support via Swashbuckle.AspNetCore package
- Configured proper dependency injection for repository and request processors
- Removed auto-generated Class1.cs files from all projects

## Before/After Comparison
- Before: No .NET project structure in place
- After: Complete ASP.NET Core API solution with:
  - Layered architecture following best practices
  - Minimal API endpoints for book and author data
  - OpenAPI/Swagger configuration and UI for API documentation
  - Request processors with hardcoded data
  - Clean project structure with proper separation of concerns
  - Proper dependency injection

## References
- Domain Knowledge: N/A (no domain knowledge files available yet)
- Specifications: 
  - 0001-create-api-spec.md - Used to understand the architecture and API design
  - 0002-create-api-project.md - Used for specific requirements of this task
- URLs: 
  - https://devblogs.microsoft.com/dotnet/dotnet9-openapi/ - For OpenAPI implementation in .NET 9

## Checklist
- [x] Phase 1: Project Setup
  - [x] Create directory structure
  - [x] Create .NET 9 solution
  - [x] Add ASP.NET Core API project
- [x] Phase 2: Project Structure Implementation
  - [x] Set up folder structure
  - [x] Configure Program.cs
  - [x] Set up OpenAPI/Swagger
- [x] Phase 3: Implement API Endpoints
  - [x] Create DTOs
  - [x] Implement request processors
  - [x] Set up API endpoints
- [x] Phase 4: Testing
  - [x] Verify API runs
  - [x] Test Swagger/OpenAPI
  - [x] Test endpoints