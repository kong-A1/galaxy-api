# galaxy-api

A .NET 10 rewrite of MJS-API (Laravel 8, separate repository). The database is redesigned from scratch, but the system should work the same way as the old one.

This file and `docs/PROGRESS.md` belong to **galaxy-api only**. Do not add information about other projects.

@docs/PROGRESS.md

## Starting a new session

1. Read `docs/PROGRESS.md`.
2. Check the real state before trusting it: `git status`, `git branch --show-current`, `git log --oneline -5`, and pull request status. If they disagree with PROGRESS.md, trust the code and git, and tell the user.

## Finishing a piece of work

Update `docs/PROGRESS.md` (what was done, what was decided, what is still open, what comes next) before proposing a commit.

## How to work with the user

- **Always reply in Thai.** Technical terms, file names and code can stay in English.
- The user is new to .NET and wants **to be taught and to write the code themselves**: give real code examples, explain them part by part, and review the user's code.
- Edit files, install packages, change databases or change machine-level tools **only when the user explicitly asks**. Read-only checks are fine.
- Commit, push and open pull requests only when the user asks.
- If the user is only asking a question, answer it and change nothing.
- Make clear what was verified, what is a proposal, and what has not been checked.
- Finish one topic at a time and keep answers to the point.
- Before giving a code example, try to build it on a copy of the repo outside the project folder.

## Conventions

- Stack: .NET 10, ASP.NET Core Controllers, EF Core 10, PostgreSQL (Npgsql)
- Flow: Controller → Service → Repository or DbContext
- Controllers must not contain business logic and must not call DbContext directly.
- All interfaces go in `Interfaces/`. Implementations go in `Services/` or `Repositories/`.
- Interface names must match their implementation (`IFooService` ↔ `FooService`). `ITimestamped` is a shared contract implemented by entities.
- Entities go in `Entities/`. Do not suffix names with "Entity". Do not create a `Models/` folder.
- Never return entities from the API. Use DTOs in `Dtos/Requests`, `Dtos/Responses` or `Dtos/Common`.
- Validate with DataAnnotations on request DTOs.
- Handle errors with `IExceptionHandler` + `ProblemDetails` (not built yet, and its folder has not been decided).
- DI registration goes in `Extensions/` (`AddAppServices`, `AddDatabase`). `AddOpenApi()` and `AddControllers()` are currently still in `Program.cs`.
- Never edit files in `Data/Migrations/` by hand. Create them with `dotnet ef migrations add`, and review the SQL before `database update`.
- Use `RandomNumberGenerator` for security codes (OTP, tokens). Never use `Random`.
- `dotnet build` must pass before work is considered done.
- Create a folder only when there is code to put in it.

## Things to know about the code

- Database names are snake_case via `UseSnakeCaseNamingConvention()`.
- `AppDbContext` loads only the configurations in the same namespace as `UserConfiguration`.
- `AuditInterceptor` fills timestamps and `*_by` columns, and turns deletes of `AuditableBase` entities into soft deletes.
  - `AppDbContext` sets `CascadeDeleteTiming` and `DeleteOrphansTiming` to `OnSaveChanges`. **Do not remove this**, or EF will handle related data before the interceptor runs.
  - `ExecuteUpdateAsync`, `ExecuteDeleteAsync` and raw SQL bypass the interceptor.
- The unique index on `lower(email)` for `users` is raw SQL in the `AddRolesAndUsers` migration, so it is not part of the EF model.
- `MapControllers()` is enabled only in Development because there is no authentication yet.

## Security

- The connection string is stored in .NET User Secrets. Never put passwords, tokens or personal data in committed files (including this file and PROGRESS.md).
- Before committing, check that `appsettings*.json` contains no secrets.

## Commands

```bash
dotnet build
dotnet run --launch-profile http          # http://localhost:5162/swagger
dotnet ef migrations list
dotnet ef migrations has-pending-model-changes
dotnet ef migrations script <from-migration>
```
