# Task_GTS

This is a simple Todo/Product Management API built using ASP.NET Core Web API. It manages **Categories** and **Products**, with full CRUD operations and a basic HTML Frontend for categories.

---

## ✅ Features Implemented

### 🔹 Categories
- Get all categories with related products.
- Get a specific category by ID.
- Create new category.
- Edit existing category.
- Delete a category.

### 🔹 Products
- Get all products with category details.
- Get a specific product by ID.
- Create new product (with image upload).
- Edit existing product (supports image replacement).
- Delete a product and its image from the server.

---

## 🌐 Frontend

- A simple `category.html` page (HTML + JavaScript) to interact with Category API endpoints.
- Features include:
  - View all categories
  - View details by ID
  - Add new category
  - Edit category by ID
  - Delete category by ID

> **Note:** I couldn't complete the frontend for the product APIs due to time constraints.

---

## 🛠️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022+ or Visual Studio Code
- SQL Server or LocalDB
- Git
- Browser (for testing the HTML page)

---

## 🚀 How to Run the Project

1. **Clone the Repository**
   ```bash
   git clone https://github.com/MuhammedReda55/Task_GTS.git
   cd GTS-Todo-Task

   ❗ Challenges Faced
Product Frontend: Due to limited time, I wasn’t able to implement the frontend UI for the Product APIs. However, the backend is fully functional and ready to be integrated with Angular or any other frontend framework.

Image Storage: I initially faced issues trying to store product images directly in the database. As a workaround, I stored the image files in a local folder named Images and saved the filename in the database. This makes it easier for frontend apps (like Angular) to fetch and display product images using a direct URL path.

📌 Notes
Clean architecture using Repository pattern.

All APIs follow RESTful conventions.

Fully documented and tested endpoints
