# ERD Diagram Update

## Requirements
- Review the current ERD diagram in the domain knowledge file
- Verify if there's a bug related to missing composite primary keys in many-to-many relationships
- Generate a corrected ERD diagram using Mermaid.js notation if issues are found
- Update the domain knowledge file with the corrected diagram

## Additional comments from user
- Concerned about the lack of composite primary keys in many-to-many relationships
- Wants to use the Northwind diagram as a reference for proper structure: https://documentation.red-gate.com/dms6/files/49646072/49646073/3/1559655630714/ERDiagramNorthwind.png

## Plan
1. Compare the current ERD diagram with the actual implementation in the codebase
2. Identify discrepancies, particularly related to many-to-many relationships
3. Create a correct ERD diagram that properly represents the relationships, including join tables with composite primary keys
4. Update the domain knowledge file with the new diagram

## Decisions
- [x] Determine which approach to take: modify the ERD to match the code or suggest code changes to match the ERD
  - Decision: Create a new ERD that correctly implements many-to-many relationships with proper join tables (composite keys), while keeping the current entity structure where possible
- [x] Decide on the proper structure for join tables in the many-to-many relationships
  - Decision: Add join tables with composite primary keys for Book-Author and Book-Category relationships, following best practices

## Implementation Details
- [x] Review current implementation of entity classes
  - Finding: Book.cs has a direct AuthorId foreign key (one-to-many) which contradicts the ERD's many-to-many description
  - Finding: No join tables for many-to-many relationships are implemented
- [x] Create new Mermaid.js ERD diagram
  - Created a proper ERD diagram with join tables (BookAuthor, BookCategory) that have composite primary keys
  - Used the Mermaid.js notation for the ERD diagram to make it visual and clear
- [x] Document the correct relationships between entities
  - Properly documented all entity relationships, especially the many-to-many relationships with join tables

## Changes Made
- [x] Update the `.github/.copilot/domain_knowledge/bookstore_data_model.md` file with the corrected ERD diagram
  - Added BookAuthor and BookCategory join tables with composite primary keys
  - Added a Mermaid.js diagram to visually represent the relationships
  - Updated the Entity Relationship Summary to accurately describe the relationships

## Before/After Comparison
- [x] Highlight the differences between the original and updated ERD diagrams
  - Original diagram didn't have proper join tables for many-to-many relationships
  - New diagram adds BookAuthor and BookCategory join tables with proper composite primary keys (BookID, AuthorID) and (BookID, CategoryID)
  - Updated the relationship descriptions to clearly indicate the join tables and their purpose
- [x] Explain how the updated diagram addresses the composite primary key issue
  - BookAuthor and BookCategory join tables now have composite primary keys consisting of both foreign keys
  - This resolves the data integrity issue in many-to-many relationships
  - Each relationship between entities is now properly modeled with appropriate cardinality

## References
- Current entity models in `src/backend/Bookstore.Data/`
- DTOs in `src/backend/Bookstore.Dtos/`
- `.github/.copilot/domain_knowledge/bookstore_data_model.md`
- Northwind ERD diagram for reference: https://documentation.red-gate.com/dms6/files/49646072/49646073/3/1559655630714/ERDiagramNorthwind.png