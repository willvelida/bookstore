# Update OrderItem Table to Use Composite Primary Key (Northwind Approach)

## Requirements
- Modify the OrderItem table to use a composite primary key (OrderID, BookID) instead of a surrogate key (OrderItemID)
- Align with the Northwind database approach for OrderDetails
- Update the ERD diagram in the domain knowledge file
- Implement the Order and OrderItem entity classes with the composite primary key approach

## Additional comments from user
- The Northwind database uses a composite primary key for OrderDetails (OrderId, ProductId)
- Current implementation has OrderItemDto but no entity class implementation yet
- Need to switch to the Northwind approach with composite primary keys

## Plan
1. **Phase 1: Update Domain Knowledge**
   - Task 1.1: Update the domain model ERD diagram in the `.github/.copilot/domain_knowledge/bookstore_data_model.md` file
   - Task 1.2: Update the entity relationship description to reflect the new approach

2. **Phase 2: Implement Entity Classes**
   - Task 2.1: Create Order entity class if it doesn't exist
   - Task 2.2: Create OrderItem entity class with composite primary key (OrderID, BookID)
   - Task 2.3: Add Discount field to match Northwind's OrderDetails
   - Task 2.4: Add required package references for Entity Framework Core

3. **Phase 3: Update DTOs**
   - Task 3.1: Update the OrderItemDto class to reflect the new primary key structure
   - Task 3.2: Ensure OrderDto correctly references the updated OrderItemDto

4. **Phase 4: Update Repository Implementation**
   - Task 4.1: Add repository methods for handling Order and OrderItem entities
   - Task 4.2: Implement composite key handling in repository methods

5. **Phase 5: Testing**
   - Task 5.1: Run tests to ensure everything works correctly
   - Task 5.2: Fix any issues that arise during testing

## Decisions
- [x] Determine if there are any additional fields needed (like Discount from Northwind)
  - Decision: We will add the Discount field to match the Northwind OrderDetails structure
- [x] Decide how to handle existing code that expects a single OrderItemID
  - Decision: Since we don't have actual entity classes implemented yet and the repository doesn't have methods for orders, we can implement the composite key approach from the start
- [x] Determine required dependencies for composite primary key implementation
  - Decision: Need to add Microsoft.EntityFrameworkCore package reference to Bookstore.Data project

## Implementation Details
- [x] Domain knowledge ERD diagram update
  - Updated the OrderItem entity in the ERD diagram to use a composite primary key (OrderID, BookID)
  - Added the Discount field to match Northwind's OrderDetails structure
  - Updated entity relationship descriptions to reflect the new approach
- [x] Order.cs and OrderItem.cs creation
  - Created Order entity class with standard primary key
  - Created OrderItem entity class with composite primary key using EF Core's [PrimaryKey] attribute
  - Added Discount field to OrderItem entity
- [x] OrderItemDto.cs updates
  - Updated to use OrderId and BookId as key fields instead of OrderItemId
  - Added Discount field and updated Subtotal calculation
- [x] Repository interface and implementation updates
  - Added methods for handling Order and OrderItem entities
  - Implemented composite key handling in repository methods
- [x] Add required NuGet package reference for Entity Framework Core
  - Added Microsoft.EntityFrameworkCore package to the Bookstore.Data project
- [x] Update request processors to use composite key 
  - Fixed GetOrderRequestProcessor and GetAllOrdersRequestProcessor to use OrderId+BookId instead of OrderItemId
  - Added Discount field to all OrderItemDto instances

## Changes Made
- [x] Update the ERD diagram in the domain knowledge file
  - Changed OrderItem to use composite primary key (OrderID, BookID) instead of surrogate key (OrderItemID)
  - Added Discount field to OrderItem
  - Updated entity relationship descriptions to reflect new structure
- [x] Create entity classes for Order and OrderItem
  - Created Order.cs with navigation to Customer and OrderItems
  - Created Customer.cs to support Order navigation
  - Created OrderItem.cs with composite primary key (OrderID, BookID)
- [x] Update DTOs as needed
  - Updated OrderItemDto to use OrderId and BookId instead of OrderItemId
  - Added Discount field to OrderItemDto
- [x] Update repository implementation
  - Added Order and OrderItem related methods to IBookstoreRepository
  - Implemented those methods in BookstoreRepository
- [x] Add required NuGet package reference for Entity Framework Core
  - Added Microsoft.EntityFrameworkCore package to enable the [PrimaryKey] attribute
- [x] Update request processors
  - Fixed GetOrderRequestProcessor and GetAllOrdersRequestProcessor to use the new composite key approach

## Before/After Comparison
- **Before**: 
  - No OrderItem entity class exists yet, only DTOs with OrderItemId as primary key
  - No Discount field in OrderItemDto
  - No support for composite primary keys in the data model

- **After**: 
  - OrderItem entity class implemented with composite primary key (OrderID, BookID)
  - Added Discount field to both OrderItem entity and OrderItemDto
  - Repository implementation supports composite key operations
  - Request processors updated to use the composite key approach
  - All tests pass successfully

## References
- [Northwind ERD diagram](https://documentation.red-gate.com/dms6/files/49646072/49646073/3/1559655630714/ERDiagramNorthwind.png)
- Current domain knowledge file: `.github/.copilot/domain_knowledge/bookstore_data_model.md`
- Current DTOs: `src/backend/Bookstore.Dtos/OrderDto.cs` and `src/backend/Bookstore.Dtos/OrderItemDto.cs`
- Repository interface: `src/backend/Bookstore.Data/IBookstoreRepository.cs`
- Entity classes: `src/backend/Bookstore.Data/OrderItem.cs` and `src/backend/Bookstore.Data/Order.cs`
- Request processors: `src/backend/Bookstore.RequestProcessing/Features/GetOrder/GetOrderRequestProcessor.cs` and `src/backend/Bookstore.RequestProcessing/Features/GetAllOrders/GetAllOrdersRequestProcessor.cs`

## Implementation Checklist
- [x] **Phase 1: Update Domain Knowledge**
   - [x] Task 1.1: Update the domain model ERD diagram
   - [x] Task 1.2: Update the entity relationship description

- [x] **Phase 2: Implement Entity Classes**
   - [x] Task 2.1: Create Order entity class if it doesn't exist
   - [x] Task 2.2: Create OrderItem entity class with composite primary key
   - [x] Task 2.3: Add Discount field to match Northwind's OrderDetails
   - [x] Task 2.4: Add required package references for Entity Framework Core

- [x] **Phase 3: Update DTOs**
   - [x] Task 3.1: Update the OrderItemDto class
   - [x] Task 3.2: Ensure OrderDto correctly references OrderItemDto

- [x] **Phase 4: Update Repository Implementation**
   - [x] Task 4.1: Add repository methods for Order and OrderItem
   - [x] Task 4.2: Implement composite key handling

- [x] **Phase 5: Testing**
   - [x] Task 5.1: Run tests to ensure everything works correctly
   - [x] Task 5.2: Fix any issues that arise during testing