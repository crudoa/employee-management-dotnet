# 👨‍💼 Employee Management System - ASP.NET Core MVC

Employee management web app built with ASP.NET Core 8 MVC, Entity Framework Core and SQL Server. Full employee CRUD with a relational Department / Province / District structure and server-side validation.

---

## 🚀 Tech Stack

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

---

## 📂 Features

- Employee CRUD (Create, Read, Update, Delete)
- Department / Province / District relational structure
- MVC layered architecture
- Entity Framework Core database integration
- Server-side validation

---

## 🏗 Project Structure

```
Controllers/
Models/
Data/
Views/
wwwroot/
Program.cs
```

- **Controllers** → Application logic  
- **Models** → Domain entities  
- **Data** → DbContext configuration  
- **Views** → Razor UI  
- **wwwroot** → Static assets  

---

## ⚙️ Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/your-username/employee-management-dotnet.git
cd employee-management-dotnet
```

### 2️⃣ Restore dependencies

```bash
dotnet restore
```

### 3️⃣ Update database

Configure your connection string in:

```
appsettings.json
```

Then run:

```bash
dotnet ef database update
```

### 4️⃣ Run the application

```bash
dotnet run
```

---

## 🎯 Purpose

This project was developed to demonstrate backend fundamentals in:

- ASP.NET Core MVC  
- Clean architecture principles  
- Entity Framework Core  
- Relational database modeling  

---

## 📌 Future Improvements

- Add authentication & authorization  
- Implement API version  
- Add pagination and filtering  
- Improve UI with modern frontend framework  
- Add unit testing  

---

## 👤 crudoa
