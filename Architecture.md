# Architecture Overview

## Why No Entity Framework?

This solution **deliberately avoids Entity Framework (EF)**.  
Instead, it uses **SQL Server stored procedures** and **lean data transfer objects (DTOs)** to decouple the database from the application layer.

### Rationale

- **Full Power of the Database Engine**  
  Business logic remains close to the data. SQL Server features (joins, aggregates, indexes, query hints, execution plans) are first-class citizens, not abstracted away by an ORM.

- **Reduced Coupling**  
  The database exposes a stable contract (stored procs). The API can be rewritten in another language tomorrow, and as long as it calls the same procs, nothing breaks.

- **Security & Governance**  
  Applications get execute rights on procs, not raw table access. This provides a clean audit trail and tighter control of data flows.

- **Performance**  
  EF-generated queries often move logic into the application layer. By contrast, our procs ensure filtering, paging, and aggregation happen *inside* SQL Server, minimizing network traffic and exploiting query plan optimizations.

- **Maintainability**  
  Database experts can tune queries independently of the API codebase. Indexes and query plans can be evolved without changing C#.

---

## Why EF Was Avoided

Some EF “conveniences” are actually risks at scale:

- **Change Tracking & Navigation Properties**  
  Useful in small apps, but unnecessary overhead for millions of rows. We fetch only what we need.

- **Migrations**  
  We prefer explicit SQL scripts. Schema evolution is controlled, reviewable, and predictable.

- **LINQ Queries**  
  Nice for prototypes, but they hide query details. At scale, we want deterministic SQL, not opaque translation.

---

## Scaling Considerations

This solution is designed for **millions of cases, millions of tasks, and hundreds of case workers**.  
Key patterns:

- **Projections** – Procs return *exact* fields needed by the UI (`StatusName`, `TaskTypeName`, `CaseTitle`). No over-fetching.
- **Pagination** – Keyset/seek pagination instead of offset for large lists.
- **Indexing** – Covering indexes tuned to workload; review query plans regularly.
- **Bulk Ops** – Use table-valued parameters (TVPs) for batch inserts/updates.
- **Concurrency** – Favor `READ COMMITTED SNAPSHOT` to reduce blocking, keep transactions short.
- **Data Lifecycle** – Partition and archive old cases; filtered indexes for active workloads.
- **Observability** – Query Store + telemetry logs monitor slow or regressed procs.
- **Team Specialization** – In large organizations, DB performance is a *dedicated responsibility* of the SQL development team.  
  This architecture supports clear division of responsibilities:  
  - The **API team** focuses on business logic and service orchestration.  
  - The **DB team** owns query tuning, indexing, and schema evolution.  
  If EF were used, these responsibilities would blur, reducing clarity and accountability.

---

## Summary

This architecture **treats SQL Server as a first-class business engine, not just a table store**.  
By isolating all queries in stored procedures and avoiding EF, we gain:

- Predictability and performance at scale.  
- Independence between the DB layer and API implementation.  
- Stronger use of SQL Server’s native strengths.  
- Clearer team responsibilities in large organizations.  
- A maintainable system that can evolve technology stacks without rewriting the data layer.
