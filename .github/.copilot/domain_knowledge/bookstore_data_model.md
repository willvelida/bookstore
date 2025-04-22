# 📚 Bookstore App – Data Model

## 🧱 Core Entities and Relationships

### 1. **Book**
- **Attributes**:
  - BookID (Primary Key)
  - Title
  - ISBN
  - Description
  - Price
  - PublicationDate
  - StockQuantity
  - CoverImageURL
- **Relationships**:
  - Belongs to one or more **Categories**
  - Written by one or more **Authors**
  - Published by one **Publisher**

---

### 2. **Author**
- **Attributes**:
  - AuthorID (Primary Key)
  - Name
  - Bio
  - DOB
- **Relationships**:
  - Can write many **Books**

---

### 3. **Category**
- **Attributes**:
  - CategoryID (Primary Key)
  - Name
  - Description
- **Relationships**:
  - Can include many **Books**

---

### 4. **Publisher**
- **Attributes**:
  - PublisherID (Primary Key)
  - Name
  - ContactEmail
  - Address
- **Relationships**:
  - Can publish many **Books**

---

### 5. **Customer**
- **Attributes**:
  - CustomerID (Primary Key)
  - Name
  - Email
  - PasswordHash
  - Address
  - PhoneNumber
- **Relationships**:
  - Can place many **Orders**

---

### 6. **Order**
- **Attributes**:
  - OrderID (Primary Key)
  - CustomerID (Foreign Key)
  - OrderDate
  - TotalAmount
  - Status (e.g., Pending, Shipped, Completed, Cancelled)
- **Relationships**:
  - Belongs to one **Customer**
  - Contains multiple **OrderItems**

---

### 7. **OrderItem**
- **Attributes**:
  - OrderItemID (Primary Key)
  - OrderID (Foreign Key)
  - BookID (Foreign Key)
  - Quantity
  - UnitPrice
- **Relationships**:
  - Linked to one **Order**
  - Linked to one **Book**

---

## 🧩 Optional Enhancements

### 8. **Review**
- **Attributes**:
  - ReviewID (Primary Key)
  - CustomerID (Foreign Key)
  - BookID (Foreign Key)
  - Rating (1–5)
  - Comment
  - ReviewDate

---

### 9. **Cart**
- **Attributes**:
  - CartID (Primary Key)
  - CustomerID (Foreign Key)
  - CreatedAt
  - UpdatedAt
- **Relationships**:
  - Contains multiple **CartItems**

### **CartItem**
- **Attributes**:
  - CartItemID (Primary Key)
  - CartID (Foreign Key)
  - BookID (Foreign Key)
  - Quantity

## Entity Relationship Summary

- **Author ↔ Book**
  - An author can write **many books**
  - A book can be written by **multiple authors** (many-to-many)

- **Book ↔ Category**
  - A book can belong to **multiple categories**
  - A category can contain **multiple books** (many-to-many)

- **Book → Publisher**
  - A book is published by **one publisher**
  - A publisher can publish **many books** (many-to-one)

- **Customer ↔ Order**
  - A customer can place **many orders**
  - Each order belongs to **one customer** (one-to-many)

- **Order → OrderItem**
  - An order contains **multiple order items**
  - Each order item belongs to **one order** (one-to-many)

- **OrderItem → Book**
  - Each order item references **one book**
  - A book can appear in **many order items** (many-to-one)

- **Customer ↔ Review**
  - A customer can write **many reviews**
  - A review is written for **one book**
  - A book can have **many reviews**

- **Customer → Cart**
  - A customer has **one cart**
  - A cart belongs to **one customer** (one-to-one)

- **Cart → CartItem**
  - A cart contains **multiple cart items**
  - Each cart item belongs to **one cart** (one-to-many)

- **CartItem → Book**
  - Each cart item references **one book**
  - A book can appear in **many cart items**

This structure provides the foundation for a bookstore e-commerce application while maintaining the relationships between Books, Authors, Publishers, and other key entities.
