# Fix Dependency Injection for IBookstoreRepository

## Requirements
- Fix the API startup error related to dependency injection
- Properly register IBookstoreRepository in the DI container
- Update request processors to use the interface instead of concrete implementation

## Additional comments from user
- The API fails to start with dependency injection errors
- Recent changes to implement the Northwind approach for OrderItem with composite primary key require proper interface registration

## Plan
1. **Phase 1: Analyze the Issue**
   - Task 1.1: Review the error message to understand the dependency injection issue
   - Task 1.2: Check Program.cs to identify the incorrect registration

2. **Phase 2: Fix Dependency Injection Registration**
   - Task 2.1: Update Program.cs to properly register IBookstoreRepository
   - Task 2.2: Verify the registration works by running the API

3. **Phase 3: Update Request Processors**
   - Task 3.1: Identify all request processors that depend on concrete BookstoreRepository
   - Task 3.2: Update request processors to depend on IBookstoreRepository interface
   - Task 3.3: Verify the API starts successfully

## Decisions
- [x] Determine how to register the repository interface
  - Decision: Register IBookstoreRepository interface with BookstoreRepository implementation using AddTransient
- [x] Determine how to handle request processors that depend on concrete implementation
  - Decision: Update all request processors to depend on IBookstoreRepository interface instead of concrete BookstoreRepository

## Implementation Details
- [x] Program.cs needs to be updated to register IBookstoreRepository
  - The current code only registers BookstoreRepository but not the interface
  - Request processors are expecting IBookstoreRepository to be injected
  - Need to use builder.Services.AddTransient<IBookstoreRepository, BookstoreRepository>() instead of just registering BookstoreRepository
- [x] Request processors need to be updated to use IBookstoreRepository
  - Currently, they depend on concrete BookstoreRepository which creates another DI error
  - Need to change dependency from BookstoreRepository to IBookstoreRepository in each processor

## Changes Made
- [x] Update Program.cs to register IBookstoreRepository interface with BookstoreRepository implementation
  - Changed the repository registration from `builder.Services.AddTransient<BookstoreRepository>()` to `builder.Services.AddTransient<IBookstoreRepository, BookstoreRepository>()`
  - This properly connects the interface to its implementation in the dependency injection container
- [x] Update request processors to use IBookstoreRepository interface
  - Updated GetPublisherRequestProcessor to depend on IBookstoreRepository
  - Updated GetAllPublishersRequestProcessor to depend on IBookstoreRepository 
  - Updated GetCustomerRequestProcessor to depend on IBookstoreRepository
  - Updated GetAllCustomersRequestProcessor to depend on IBookstoreRepository 
  - Updated GetOrderRequestProcessor to depend on IBookstoreRepository
  - Updated GetAllOrdersRequestProcessor to depend on IBookstoreRepository

## Before/After Comparison
- **Before**: 
  - Only the concrete BookstoreRepository is registered, not connected to its interface
  - Request processors depend on concrete BookstoreRepository
  ```csharp
  // Register repository
  builder.Services.AddTransient<BookstoreRepository>();
  
  // Request processor
  private readonly BookstoreRepository _repository;
  public GetPublisherRequestProcessor(BookstoreRepository repository)
  ```
- **After**: 
  - The IBookstoreRepository interface is registered with the BookstoreRepository implementation
  - Request processors depend on IBookstoreRepository interface
  ```csharp
  // Register repository
  builder.Services.AddTransient<IBookstoreRepository, BookstoreRepository>();
  
  // Request processor
  private readonly IBookstoreRepository _repository;
  public GetPublisherRequestProcessor(IBookstoreRepository repository)
  ```

## References
- Current Program.cs file that has incorrect DI registration
- Request processor classes that need to be updated to use the interface
- IBookstoreRepository interface and BookstoreRepository implementation that were modified in previous task
- Previous breadcrumb: `.github/.copilot/breadcrumbs/2025-04-23-1515-northwind-orderitem-update.md` that implemented the OrderItem changes

## Implementation Checklist
- [x] **Phase 1: Analyze the Issue**
   - [x] Task 1.1: Review the error message to understand the dependency injection issue
   - [x] Task 1.2: Check Program.cs to identify the incorrect registration

- [x] **Phase 2: Fix Dependency Injection Registration**
   - [x] Task 2.1: Update Program.cs to properly register IBookstoreRepository
   - [x] Task 2.2: Verify the registration works by running the API

- [x] **Phase 3: Update Request Processors**
   - [x] Task 3.1: Identify all request processors that depend on concrete BookstoreRepository
   - [x] Task 3.2: Update request processors to depend on IBookstoreRepository interface
   - [x] Task 3.3: Verify the API starts successfully