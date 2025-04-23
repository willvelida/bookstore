# 2025-04-23-1530-duplicate-test-files-removal

## Requirements
- Identify duplicate test files in the `src/tests` folder
- Remove the duplicate files to ensure there's only one version of each test

## Additional comments from user
- The user requested to look at the tests in the `src/tests` folder and remove any duplicate test files.

## Plan
1. Identify duplicate test files
   - Compare files in different directories that have the same name
   - Determine which files are actually duplicates

2. Decide which files to keep
   - Based on project organization patterns
   - Look at other similar files to determine the standard organization

3. Remove duplicate files
   - Keep the file in the proper location based on project organization
   - Remove the duplicated file

## Decisions
After analyzing the test files, I've found the following duplicate files:

1. `BookEndpointsTests.cs`:
   - One in the root of `Bookstore.Api.Tests` (namespace: `Bookstore.Api.Tests`)
   - One in the `Endpoints` subfolder (namespace: `Bookstore.Api.Tests.Endpoints`)

2. `GetBookRequestProcessorTests.cs`:
   - One in the root of `Bookstore.RequestProcessing.Tests` (namespace: `Bookstore.RequestProcessing.Tests`)
   - One in the `Features/GetBook` subfolder (namespace: `Bookstore.RequestProcessing.Tests.Features.GetBook`)

The code in each pair of files is nearly identical, with the only significant differences being the namespace declarations.

I've observed that:
- There are other test files organized in subdirectories based on their function:
  - API endpoint tests in `Bookstore.Api.Tests/Endpoints`
  - Request processor tests in `Bookstore.RequestProcessing.Tests/Features/{FeatureName}`

Based on this organization pattern, I'll:
- Keep the `BookEndpointsTests.cs` in the `Endpoints` folder (organized by endpoint type)
- Keep the `GetBookRequestProcessorTests.cs` in the `Features/GetBook` folder (organized by feature)
- Remove the duplicates from the root directories

## Implementation Details
I will:
1. Delete the duplicate `src/tests/Bookstore.Api.Tests/BookEndpointsTests.cs` file
2. Delete the duplicate `src/tests/Bookstore.RequestProcessing.Tests/GetBookRequestProcessorTests.cs` file

## Changes Made
- [x] Remove `src/tests/Bookstore.Api.Tests/BookEndpointsTests.cs`
- [x] Remove `src/tests/Bookstore.RequestProcessing.Tests/GetBookRequestProcessorTests.cs`

## Before/After Comparison
Before:
- Duplicate test files in both root directories and subdirectories
- Same tests being run twice during test execution
- Potential confusion when updating test cases

After:
- Each test exists only once in the appropriate subdirectory
- Consistent organization across test projects
- Clear structure following feature/endpoint organization

## References
- File structure examination of the current workspace
- Review of existing organization patterns in the test projects
