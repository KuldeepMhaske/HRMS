# HRMS
# 🏢 HRMS — Human Resource Management System

<p align="center">
  <strong>A .NET 8 HR Management Application with Azure Hosting & Automated CI/CD</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/Azure-App%20Service-0078D4?style=for-the-badge&logo=microsoftazure&logoColor=white" alt="Azure App Service" />
  <img src="https://img.shields.io/badge/GitHub-Actions-2088FF?style=for-the-badge&logo=githubactions&logoColor=white" alt="GitHub Actions" />
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Database-Azure%20SQL-0078D4?style=flat-square&logo=microsoftsqlserver&logoColor=white" alt="Azure SQL" />
  <img src="https://img.shields.io/badge/Architecture-Modular%20Monolith-6C757D?style=flat-square" alt="Modular Monolith" />
  <img src="https://img.shields.io/badge/CI%2FCD-Automated-success?style=flat-square" alt="Automated CI/CD" />
</p>

---
<img width="1919" height="979" alt="image" src="https://github.com/user-attachments/assets/aed7544f-a3d7-4cc7-b72b-0f28c889c8f8" />

## 📌 Overview

**HRMS (Human Resource Management System)** is a web-based application developed using **ASP.NET Core MVC and .NET 8**.

The project is designed to manage HR-related operations such as authentication, employees, roles, accounts and daily tasks. The application uses **Entity Framework Core** for database access and **SQL Server / Azure SQL Database** for persistent data.

The application is hosted on **Microsoft Azure App Service** and is connected to a **GitHub Actions CI/CD pipeline**. Every push to the `master` branch can automatically build, publish and deploy the latest version of the application to Azure.

> 🚀 **Current focus:** .NET application development, Azure deployment, database management, Git/GitHub and automated CI/CD.

---

## 🌐 Live Application

**Azure App Service:**

> `https://hrms-kuldeep.azurewebsites.net/`

The root URL is configured to open the Account Login page.

---

## 📸 Screenshots

> **Add your screenshots here.** The suggested structure below keeps the README visually clean.

### 🔐 Login Page

<!-- Replace the path with your actual screenshot -->

![HRMS Login Page](docs/screenshots/login-page.png)

### 🏠 Dashboard

![HRMS Dashboard](docs/screenshots/dashboard.png)

### 👨‍💼 Employee Management

![Employee Management](docs/screenshots/employee-management.png)

### ☁️ Azure App Service

![Azure App Service](docs/screenshots/azure-app-service.png)

### ⚙️ GitHub Actions CI/CD

![GitHub Actions CI/CD](docs/screenshots/github-actions.png)

> **Recommended:** Create a `docs/screenshots/` folder in the repository and place the images there using the filenames above.

---

## ✨ Current Features

- 🔐 User authentication and login
- 👨‍💼 Employee management
- 👥 Role and access management
- 🧑‍💻 Employee accounts
- 📋 Daily task management
- 🗄️ Database-driven application
- 🔄 Entity Framework Core migrations
- ☁️ Azure App Service deployment
- 🤖 Automated GitHub Actions CI/CD
- 🔒 Production connection string stored in Azure configuration
- 🧹 Repository cleanup with `.gitignore`
- 📦 Project-level EF CLI version management

---

# 🧰 Technology Stack

| Layer | Technology |
|---|---|
| Language | **C#** |
| Framework | **ASP.NET Core MVC** |
| Runtime / SDK | **.NET 8** |
| ORM | **Entity Framework Core 8** |
| Database | **SQL Server / Azure SQL Database** |
| UI | **Razor Views, HTML, CSS, Bootstrap, JavaScript** |
| IDE | **Visual Studio** |
| Source Control | **Git + GitHub** |
| CI/CD | **GitHub Actions** |
| Cloud | **Microsoft Azure** |
| Hosting | **Azure App Service** |
| Database Hosting | **Azure SQL Database** |

---

# 🏗️ Architecture

The current application follows a **modular monolith** approach.

```text
                         ┌─────────────────────┐
                         │       Browser       │
                         └──────────┬──────────┘
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │   ASP.NET Core MVC  │
                         │        HRMS         │
                         └──────────┬──────────┘
                                    │
                 ┌──────────────────┼──────────────────┐
                 │                  │                  │
                 ▼                  ▼                  ▼
          ┌────────────┐     ┌────────────┐     ┌────────────┐
          │    Auth    │     │ Employees  │     │   Tasks    │
          └────────────┘     └────────────┘     └────────────┘
                 │                  │                  │
                 └──────────────────┼──────────────────┘
                                    ▼
                         ┌─────────────────────┐
                         │   Entity Framework  │
                         │       Core 8        │
                         └──────────┬──────────┘
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │ SQL Server / Azure  │
                         │        SQL          │
                         └─────────────────────┘
```

All modules currently belong to the same ASP.NET Core application.

### Why not microservices yet?

Microservices are **not currently implemented** because the HRMS is still a relatively compact application. Splitting it too early would add unnecessary operational complexity such as multiple deployments, service-to-service communication, distributed logging and additional infrastructure.

A future version could extract larger modules such as Authentication, Attendance, Tasks or Payroll into independent services if there is a real scaling or organizational requirement.

---

# 📁 Project Structure

```text
HRMS/
│
├── .github/
│   └── workflows/
│       └── azure-deploy.yml       # GitHub Actions CI/CD
│
├── HRMS/
│   ├── Controllers/
│   │   ├── AccountController.cs
│   │   ├── AdminController.cs
│   │   └── ...
│   │
│   ├── Data/
│   │   └── AppDbContext.cs
│   │
│   ├── Models/
│   │   └── Application models
│   │
│   ├── Views/
│   │   ├── Account/
│   │   │   └── Login.cshtml
│   │   ├── Admin/
│   │   └── ...
│   │
│   ├── Migrations/
│   │   ├── 20260811105753_InitialCreate.cs
│   │   ├── 20260811105753_InitialCreate.Designer.cs
│   │   └── AppDbContextModelSnapshot.cs
│   │
│   ├── Filters/
│   ├── wwwroot/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── dotnet-tools.json
│   └── HRMS.csproj
│
├── .gitignore
├── global.json
├── HRMS.sln
└── README.md
```

---

# 🔐 Authentication & Login Routing

The application uses the Account controller as the default entry point.

The default route in `Program.cs` is configured as:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);
```

Therefore:

```text
https://hrms-kuldeep.azurewebsites.net/
                │
                ▼
      AccountController
                │
                ▼
             Login()
                │
                ▼
     Views/Account/Login.cshtml
```

This also fixed the initial deployment behavior where the application opened correctly only when `/Account/Login` was manually added to the URL.

---

# 🗄️ Database & Entity Framework Core

The project uses **Entity Framework Core 8.0.0** with SQL Server.

The project configuration uses:

```text
.NET SDK       → 8.0.423
EF Core        → 8.0.0
EF CLI         → 8.0.0
SQL Server     → Azure SQL Database
```

### Current migration

The current database migration is:

```text
20260811105753_InitialCreate
```

The migration history was verified against the Azure SQL database using:

```powershell
dotnet ef migrations list
```

---

# 🛠️ EF Core Tooling Setup

The repository uses a local tool manifest to keep the EF CLI version consistent with the project.

`dotnet-tools.json` pins:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "dotnet-ef": {
      "version": "8.0.0",
      "commands": [
        "dotnet-ef"
      ],
      "rollForward": false
    }
  }
}
```

Restore the tool with:

```powershell
dotnet tool restore
```

Verify it with:

```powershell
dotnet ef --version
```

Expected:

```text
Entity Framework Core .NET Command-line Tools
8.0.0
```

---

# 🧩 .NET SDK Version Management

The repository contains a root `global.json` to keep the project on the required .NET SDK version.

```json
{
  "sdk": {
    "version": "8.0.423",
    "rollForward": "latestPatch",
    "allowPrerelease": false,
    "paths": [
      "C:\\Users\\Wpuser\\dotnet8",
      "$host$"
    ]
  }
}
```

The important project requirement is:

```text
.NET SDK 8.0.423
```

The custom SDK path was required because the machine also has a system-level .NET 10 SDK while the HRMS project targets .NET 8.

This configuration allows the HRMS repository to resolve its required .NET 8 SDK without replacing the machine-wide .NET installation.

---

# ☁️ Azure Deployment

The application is hosted using:

```text
Microsoft Azure
       │
       ▼
Azure App Service
       │
       ▼
hrms-kuldeep
```

The deployed application is available at:

```text
https://hrms-kuldeep.azurewebsites.net/
```

---

# 🔄 CI/CD Pipeline

One of the major implementations in this project is an automated **Continuous Integration / Continuous Deployment pipeline** using GitHub Actions.

### Pipeline flow

```text
Developer
    │
    │ git push
    ▼
┌─────────────────┐
│     GitHub      │
│     master      │
└────────┬────────┘
         │
         │ push trigger
         ▼
┌─────────────────────────┐
│     GitHub Actions      │
├─────────────────────────┤
│ 1. Checkout repository  │
│ 2. Setup .NET 8         │
│ 3. Restore dependencies │
│ 4. Build                │
│ 5. Publish              │
│ 6. Deploy to Azure      │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│     Azure App Service   │
│       hrms-kuldeep      │
└─────────────────────────┘
```

### Workflow file

```text
.github/workflows/azure-deploy.yml
```

The workflow is triggered automatically when code is pushed to the `master` branch.

Simplified workflow:

```yaml
name: Build and Deploy HRMS

on:
  push:
    branches:
      - master

workflow_dispatch:
```

The pipeline performs:

```text
Checkout
   ↓
Setup .NET 8
   ↓
Restore
   ↓
Build Release
   ↓
Publish
   ↓
Deploy to Azure
```

---

# 🧪 CI/CD Validation

The CI/CD pipeline has been tested with multiple deployments.

Successful workflow runs include:

- **Add GitHub Actions CI CD pipeline**
- **Test CI CD deployment with login page**
- **updated connection string**
- **Clean repository and pin .NET 8 EF tooling**

Each successful run verified that code pushed to GitHub was built and deployed through GitHub Actions to Azure App Service.

### Example workflow result

![GitHub Actions Successful Deployment](docs/screenshots/github-actions-success.png)

---

# 🔒 Configuration & Secrets

Production database configuration is maintained in **Azure App Service Environment variables / Connection strings** rather than depending on a production password stored in the repository.

The application uses the configuration key:

```text
DefaultConnection
```

### Important security rule

Never commit the following to GitHub:

```text
❌ Database passwords
❌ API keys
❌ Azure publish profiles
❌ Access tokens
❌ Private certificates
❌ Other production secrets
```

The repository should contain configuration structure, while sensitive production values should remain in Azure configuration or an appropriate secret-management system.

---

# 🧹 Repository Cleanup

The repository was cleaned to prevent generated or local-only files from being committed.

The `.gitignore` includes common Visual Studio / .NET generated files such as:

```text
.vs/
bin/
obj/
publish/
*.user
*.suo
*.pdb
*.nupkg
```

It also ignores local project artifacts:

```text
HRMS/HRMS-deploy.zip
HRMS_Migrations_Backup/
```

This keeps the GitHub repository focused on actual source code and project configuration.

---

# 🚀 Development Workflow

The current development workflow is:

```text
1. Make changes in Visual Studio
          ↓
2. Test locally
          ↓
3. Commit changes
          ↓
4. Push to master
          ↓
5. GitHub Actions starts automatically
          ↓
6. Restore → Build → Publish
          ↓
7. Deploy to Azure
          ↓
8. Verify live application
```

This means a normal deployment no longer requires manually creating a ZIP package or manually uploading the application to Azure.

---

# 🧪 Local Development

### Prerequisites

- Visual Studio
- .NET 8 SDK
- SQL Server / Azure SQL Database
- Git

### Clone repository

```powershell
git clone https://github.com/KuldeepMhaske/HRMS.git
cd HRMS
```

### Restore .NET dependencies

```powershell
dotnet restore
```

### Restore local tools

```powershell
dotnet tool restore
```

### Build

```powershell
dotnet build
```

### Run

```powershell
dotnet run
```

### EF migration commands

List migrations:

```powershell
dotnet ef migrations list
```

Create a migration:

```powershell
dotnet ef migrations add MigrationName
```

Apply migrations:

```powershell
dotnet ef database update
```

> Production database migrations should be handled carefully and should not be run blindly against production.

---

# 📦 Deployment Strategy

### Previous manual deployment

The application was initially deployed manually using an Azure App Service deployment package.

```text
Build
 ↓
Publish
 ↓
Create ZIP
 ↓
Upload to Azure
```

### Current automated deployment

The project now uses:

```text
Git Push
 ↓
GitHub Actions
 ↓
Build
 ↓
Publish
 ↓
Azure App Service
```

This reduces manual deployment work and makes deployments repeatable.

---

# 📊 Current Project Status

| Area | Status |
|---|---:|
| ASP.NET Core MVC | ✅ Implemented |
| .NET 8 | ✅ Configured |
| Entity Framework Core 8 | ✅ Configured |
| SQL Server / Azure SQL | ✅ Connected |
| Authentication/Login | ✅ Implemented |
| Azure App Service | ✅ Deployed |
| GitHub Repository | ✅ Configured |
| GitHub Actions CI/CD | ✅ Working |
| Automated Azure Deployment | ✅ Working |
| Repository `.gitignore` cleanup | ✅ Completed |
| EF CLI version pinning | ✅ Completed |
| Microservices | ⏳ Not currently implemented |
| Automated production migrations | ⏳ Future consideration |
| Automated tests | 🔜 Future improvement |
| Health checks | 🔜 Future improvement |
| Centralized monitoring | 🔜 Future improvement |

---

# 🔮 Future Improvements

Possible next steps for the project include:

### 🧪 Testing

- Unit tests
- Integration tests
- CI test stage before deployment

### 🔐 Security

- Move all production secrets to secure configuration
- Password hashing/security review
- Authentication and authorization hardening
- Azure Key Vault integration if required

### 📈 Reliability

- Health checks
- Application logging
- Azure Application Insights
- Deployment verification
- Rollback strategy

### 🗄️ Database

- Controlled production migrations
- Database backup strategy
- Migration validation in CI/CD

### 🏗️ Architecture

- Further modularization
- Clean Architecture improvements
- API layer where useful
- Microservices only when there is a genuine requirement

---

# 🧠 Architecture Decision: Modular Monolith First

The current HRMS intentionally remains a **modular monolith**.

This provides:

- Simpler development
- Simpler deployment
- Easier debugging
- Lower infrastructure cost
- One application to maintain
- One primary database

If a specific module later needs independent scaling or deployment, it can be extracted into a microservice.

For example:

```text
Current

             HRMS
              │
      ┌───────┼────────┐
      │       │        │
     Auth  Employees  Tasks

Future possibility

                 HRMS Gateway
                      │
       ┌──────────────┼──────────────┐
       ▼              ▼              ▼
   Auth API      Employee API     Task API
```

Microservices are therefore considered a **future architectural option**, not a requirement for the current application.

---

# 📚 Key Concepts Implemented

This project provides practical experience with:

```text
C#
 │
 ├── ASP.NET Core MVC
 ├── Razor Views
 ├── Entity Framework Core
 ├── SQL Server
 │
 ├── Git
 ├── GitHub
 ├── GitHub Actions
 │
 └── Microsoft Azure
      ├── App Service
      └── Azure SQL
```

The project therefore demonstrates not only application development, but also the path from **local development → source control → automated build → cloud deployment**.

---

# 👨‍💻 Author

**Kuldeep Mhaske**

.NET Developer

GitHub: [@KuldeepMhaske](https://github.com/KuldeepMhaske)

Repository: [HRMS](https://github.com/KuldeepMhaske/HRMS)

---

## ⭐ Project Summary

> **HRMS is an ASP.NET Core MVC application built with .NET 8 and Entity Framework Core, backed by SQL Server/Azure SQL, hosted on Azure App Service, and deployed automatically through a GitHub Actions CI/CD pipeline.**

---

<p align="center">
  <strong>Built with .NET 8 • Deployed with Azure • Automated with GitHub Actions 🚀</strong>
</p>
