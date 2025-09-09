# CaseTaskManager

CaseTaskManager is a **Blazor + ASP.NET Core** application for managing **Cases, Tasks, Task Types, Task Statuses, and Case Workers**.  
It provides a full CRUD interface, backed by a RESTful API and SQL Server database.

---

## Features

- Manage **Cases** with status tracking and assignments  
- Manage **Tasks** with types, statuses, and due dates  
- CRUD operations for **Task Types** and **Task Statuses**  
- CRUD operations for **Case Workers** with activation/deactivation  
- RESTful API built with **ASP.NET Core**  
- **Blazor UI** frontend with Create, Edit, and Delete confirmation pages  

---

## Repository Structure

```
CaseTaskManager/
├── CaseTaskManager/        # API project
├── CaseTaskManager.UI/     # Blazor UI project
├── CaseTaskManagerDb/      # Database project (SQL Server scripts)
├── docs/                   # Extra documentation
└── CaseTaskManager.sln     # Visual Studio solution
```

---

## Documentation

- [⚙️ Setup & Installation Guide](setup.md)  
- [💡 Possible Improvements](improvements.md)  
- [⚙️ Seed Script: Case Statuses](seed-case-statuses.md)
- [⚙️ Seed Script: Task Statuses](task-statuses-seed.md)
- [🤝 Contributing Guidelines](contributing.md)

---
 
## 🛠️ Tech Stack

- **.NET 7 / 8**  
- **Blazor Server**  
- **ASP.NET Core Web API**  
- **Dapper** for database access  
- **SQL Server**  

---

## License

This project is licensed under the **MIT License**.
