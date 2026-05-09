# CrumbsBookLoans

A book lending library for 1Breadcrumb — lets employees discover who owns which books and which ones are available to borrow.

Built as a prototype for the 1Breadcrumb hiring assessment. See `SPEC.md` for the original brief and `bookloans.png` for the design sketch.

## Overview

A React SPA backed by an ASP.NET Core Web API with a SQLite database. In development the two run as separate servers; in production ASP.NET Core serves both the API and the React app from the same origin (I didn't even test that, but I'm sure it works - famouse last words right?).

```
React (Vite + TypeScript)
    ↓  /api/*
ASP.NET Core Web API
    ↓
SQLite (EF Core)
```

## Projects

- [`src/CrumbsBookLoans.Api`](src/CrumbsBookLoans.Api/README.md) — ASP.NET Core Web API (.NET 10)
- `src/CrumbsBookLoans.Client` — React frontend (Vite + TypeScript + Tailwind)

## Development

Start the API:

```bash
dotnet run --project src/CrumbsBookLoans.Api
```

Start the React dev server:

```bash
cd src/CrumbsBookLoans.Client
npm install
npm run dev
```

Or use the **Full Stack** launch configuration in VS Code (`.vscode/launch.json`) to start both with debuggers attached.

Frontend: `http://localhost:5173`
API: `https://localhost:5001`

I love vscode ... but you might not ... use your poison of choice.