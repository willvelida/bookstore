# Bookstore

A .NET Core API application for managing a bookstore system.

## API Testing with HTTP Files

### Overview

The Bookstore API includes an HTTP file (`Bookstore.Api.http`) that allows you to easily test all available API endpoints directly from Visual Studio Code. This file contains predefined HTTP requests for each endpoint, organized by entity type.

### Prerequisites

To use the HTTP file for testing, you need:

1. Visual Studio Code
2. [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) for VS Code

### Using the HTTP File

1. Open the `src/backend/Bookstore.Api/Bookstore.Api.http` file in VS Code
2. Make sure the Bookstore.Api project is running
3. Click on the "Send Request" link that appears above any request in the file
4. The response will be displayed in a separate panel within VS Code

### Available Endpoints

The HTTP file includes tests for the following endpoint categories:

#### Book Endpoints
- Get book by ID

#### Author Endpoints
- Get author by ID

#### Category Endpoints
- Get all categories
- Get category by ID

#### Publisher Endpoints
- Get all publishers
- Get publisher by ID

#### Customer Endpoints
- Get all customers
- Get customer by ID

#### Order Endpoints
- Get all orders
- Get order by ID

#### Review Endpoints
- Get reviews for a book

#### Cart Endpoints
- Get customer cart

### Example: Testing a Specific Endpoint

To test retrieving a book with ID 1:

```http
# Get book by ID
GET {{Bookstore.Api_HostAddress}}/api/books/1
Accept: application/json
```

The `{{Bookstore.Api_HostAddress}}` variable is defined at the top of the file and points to `http://localhost:5223`, which is the default development server URL.

### Customizing Requests

You can modify the requests in the HTTP file to test different scenarios:

1. Change the IDs in the URL to retrieve different resources
2. Add request bodies for POST or PUT requests
3. Add authentication headers if required
4. Change query parameters to filter results

For more information on using HTTP files with REST Client, see the [REST Client documentation](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).