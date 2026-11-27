# galaxy-api progress

Last updated: 2026-11-26

Branch and pull request status below is as of this date. Check git and GitHub again.

## Current status

- PRs #1–#6 are merged.
- Branch `feat/create-role` holds batch role creation and database column comments (the user chose not to split them). A pull request into `main` was opened from it; check its state on GitHub.
- Batch role creation has been verified against `galaxy_dev` (commit `b0bfb02`).
- **Next task:** after that pull request is merged, create a new branch from `main` and teach getting one role, updating and deleting.

## Decisions (current)

- New database design; the old Laravel schema is not reused. This means data must be migrated (ETL) and customers switched over one at a time.
- **Each customer has its own API and its own database** (one repo, many deployments). **There is no central database** and no code that picks a database per request.
  - No customer-specific conditions in code. Differences live in config or data.
  - When JWT is added, each customer needs its own signing key, and `issuer` and `audience` must be validated.
  - The Control DB idea and using `tenant_id` in the JWT to choose a database are **cancelled**.
- Structure is Controller + Service + DTO, following the conventions in `CLAUDE.md`.
- The `src/` and `tests/` layout is **postponed until testing starts**. It must be set up before creating a test project, because the `.csproj` is at the repo root.
- **Route style (all topics):** URLs use nouns and the HTTP method states the action: `GET /roles`, `POST /roles`, later `GET`/`PUT`/`DELETE /roles/{id}`. No `list` or `create` words in URLs. `POST /roles` takes a list (`{ "roles": [...] }`); to create one role, send one item.
- Batch creation is all or nothing: 1–50 roles per request, no repeated codes within a request, and a code that already exists returns `409` listing the duplicates.
- The create request accepts only `code` and `name`. `isActive` and `isSuperAdmin` were removed for now ("option A"). Do not let callers set `is_super_admin` or `is_system`.
- Role codes accept only `a-z`, `0-9` and `_` (enforced by the API, not by the database). The design spreadsheet still shows an uppercase example (`ADMIN`).
- DTOs may be grouped by topic in one file (for example, all role request classes in `Dtos/Requests/RoleRequest.cs`).
- All columns have database comments through `HasComment()`. Making this a rule for new columns was proposed but not confirmed.
- A user can have several roles, and permissions are combined (union). `is_super_admin` skips permission checks, and at least one active super admin must always exist. Roles with `is_system` cannot be edited or deleted from the UI.
- Menus: a menu is visible if the user has at least one of its linked permissions. A menu with no linked permission is visible to super admins only. Maximum depth is 2.
- A system user (fixed UUID) represents actions done by the system (not seeded yet).
- `users` are soft deleted, and anonymized when personal data must be removed.

## Done

- 10 database tables: `users`, `roles`, `permissions`, `user_roles`, `permission_role`, `master_menus`, `menu_permissions`, `refresh_tokens`, `password_reset_tokens`, `audit_logs` (created by the first 5 migrations; all migrations are applied to `galaxy_dev`)
- `AuditInterceptor` (timestamps, `*_by`, soft delete)
- Structure refactor to match the conventions
- List roles, `GET /roles` (merged in PR #5)
- Batch role creation, `POST /roles`
- `AddColumnComments` migration: comments on all 10 tables and 86 columns, applied to `galaxy_dev`

## Tested / not tested

- Tested against `galaxy_dev` inside a rolled-back transaction: role soft delete, timestamp updates, a tracked `permission_role` row is not deleted, and a duplicate email that differs only in letter case is rejected.
- Verified against `galaxy_dev` over HTTP: creating two roles returns `201` with database-generated ids; `isSuperAdmin: true` in the request is ignored; repeated codes in one request return `400`; one new plus one existing code returns `409` and the new one is not saved; the list returns the created roles.
- The test roles `marketing` and `support` remain in `galaxy_dev`.
- Not verified against a real database: other validation cases (they were tested only with a simulated database), list ordering with more varied data, and hiding soft-deleted roles through the HTTP endpoint.
- The comment migration SQL was checked: only `COMMENT ON` statements plus migration history, covering every column. The comments were not viewed in a database client afterwards.
- Not tested: deleting a user that has related data against a real database, and `*_by` values with a real user.
- There are no automated tests yet.

## Remaining work (proposed order)

1. Get one role, update, delete (a role that still has users cannot be deleted, and an `is_system` role cannot be edited or deleted; both must be checked in code). The create response `Location` header currently points to `/roles`.
2. Error handling (`IExceptionHandler` + `ProblemDetails`)
3. Set up the test layout and start writing tests (use real PostgreSQL, not the EF in-memory provider)
4. Seed data: system user, permissions, roles
5. Authentication (JWT access token + refresh token stored as a hash) and permission checks, then remove the Development-only condition around `MapControllers()`
6. Per-customer deployment steps (config, migrations, CI/CD)
7. Later: other CRUD, background jobs, integrations, point and tier engine, data migration and cutover

**Waiting for the user to confirm:** a note from an earlier session (October 2026) gives the order Database → Auth → User/Role/Permission → connect CMS → Test/CI/CD → Deploy → LINE. That **puts Auth before the Role API**, unlike the list above, which starts with the Role API. Which one to follow has not been discussed.

## Open items

- `galaxy-api.http` still calls the removed `/weatherforecast` endpoint.
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
