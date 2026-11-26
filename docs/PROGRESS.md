# galaxy-api progress

Last updated: 2026-11-26

Branch and pull request status below is as of this date. Check git and GitHub again.

## Current status

- PRs #1–#4 are merged.
- **PR #5 (`GET /roles`) is open** from branch `feat/roles-api` (commit `6d6327c`).
- **Next task:** after PR #5 is merged, create branch `feat/create-role` from `main` and teach `POST /roles`.

## Decisions (current)

- New database design; the old Laravel schema is not reused. This means data must be migrated (ETL) and customers switched over one at a time.
- **Each customer has its own API and its own database** (one repo, many deployments). **There is no central database** and no code that picks a database per request.
  - No customer-specific conditions in code. Differences live in config or data.
  - When JWT is added, each customer needs its own signing key, and `issuer` and `audience` must be validated.
  - The Control DB idea and using `tenant_id` in the JWT to choose a database are **cancelled**.
- Structure is Controller + Service + DTO, following the conventions in `CLAUDE.md`.
- The `src/` and `tests/` layout is **postponed until testing starts**. It must be set up before creating a test project, because the `.csproj` is at the repo root.
- A user can have several roles, and permissions are combined (union). `is_super_admin` skips permission checks, and at least one active super admin must always exist. Roles with `is_system` cannot be edited or deleted from the UI.
- Menus: a menu is visible if the user has at least one of its linked permissions. A menu with no linked permission is visible to super admins only. Maximum depth is 2.
- A system user (fixed UUID) represents actions done by the system (not seeded yet).
- `users` are soft deleted, and anonymized when personal data must be removed.

## Done

- 10 database tables: `users`, `roles`, `permissions`, `user_roles`, `permission_role`, `master_menus`, `menu_permissions`, `refresh_tokens`, `password_reset_tokens`, `audit_logs` (5 migrations applied to `galaxy_dev`)
- `AuditInterceptor` (timestamps, `*_by`, soft delete)
- Structure refactor to match the conventions
- `GET /roles` (in PR #5)

## Tested / not tested

- Tested against `galaxy_dev` inside a rolled-back transaction: role soft delete, timestamp updates, a tracked `permission_role` row is not deleted, and a duplicate email that differs only in letter case is rejected.
- `GET /roles` has only been tested with an empty list.
- Not tested: deleting a user that has related data against a real database, and `*_by` values with a real user.
- There are no automated tests yet.

## Remaining work (proposed order)

1. `POST /roles`: request DTO + DataAnnotations. The caller **must not be able to set `is_super_admin` or `is_system`**. A duplicate `code` returns `409` (PostgreSQL `23505`) without exposing the raw error.
2. `GET /roles/{id}`, `PUT`, `DELETE` (a role that still has users cannot be deleted, and an `is_system` role cannot be edited or deleted; both must be checked in code)
3. Error handling (`IExceptionHandler` + `ProblemDetails`)
4. Set up the test layout and start writing tests (use real PostgreSQL, not the EF in-memory provider)
5. Seed data: system user, permissions, roles
6. Authentication (JWT access token + refresh token stored as a hash) and permission checks, then remove the Development-only condition around `MapControllers()`
7. Per-customer deployment steps (config, migrations, CI/CD)
8. Later: other CRUD, background jobs, integrations, point and tier engine, data migration and cutover

**Waiting for the user to confirm:** a note from an earlier session (October 2026) gives the order Database → Auth → User/Role/Permission → connect CMS → Test/CI/CD → Deploy → LINE. That **puts Auth before the Role API**, unlike the list above, which starts with the Role API. Which one to follow has not been discussed.

## Open items

- Should `master_menus.route` be unique?
- Index on `users.is_active`
- Delete rules and `audit_logs` writing are not fully implemented in code, and database permissions do not yet make `audit_logs` append-only.
- `audit_logs.platform_actor_id` and `actor_type = 'platform'` were meant for the cancelled central database and need review.
- No job cleans up expired tokens yet.
- The design spreadsheet `Galaxy_Database_Schema_Template_v2.xlsx` is at the repo root on the local machine and is **not committed**. Its Control DB sheets are outdated. The file without `_v2` is an old version.
- VS Code go-to-definition is broken (2026-11-26): C# Dev Kit 3.40.215 failed to open the multi-folder workspace (`SolutionData service` error). Version 11.11.115 was installed, but the result after restarting VS Code has not been confirmed. The user parked this. Do not add a `.sln` next to `galaxy-api.csproj` without agreement.

## Setting up a new machine

1. Install .NET SDK 10 and PostgreSQL, then create the `galaxy_dev` database.
2. Install `dotnet-ef`.
3. `dotnet user-secrets set "ConnectionStrings:Default" "<connection string>"`
4. `dotnet ef database update`
