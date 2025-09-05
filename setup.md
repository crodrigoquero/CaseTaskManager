# Setup & Installation

## 1. Clone the Repository
```bash
git clone https://github.com/crodrigoquero/CaseTaskManager.git
cd CaseTaskManager
```

## 2. Database Setup
- Go to `CaseTaskManagerDb/`
- Run the scripts in your SQL Server instance
- Update the connection string in:
  - `CaseTaskManager/appsettings.json`
  - `CaseTaskManager.UI/appsettings.json`

## 3. Run the API
```bash
cd CaseTaskManager
dotnet run
```

## 4. Run the Blazor UI
```bash
cd CaseTaskManager.UI
dotnet run
```

Then open: [http://localhost:7231](http://localhost:7231)
