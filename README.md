# Galaxy API

Backend API for the Galaxy Line loyalty platform and its CMS.

## Technology

- PHP 8.3+
- Laravel 13
- PostgreSQL
- OpenAPI for API documentation

## Scope

The initial implementation focuses on CMS authentication, authorization, roles, permissions, and menu access. LINE Mini App APIs and loyalty business features are outside the initial foundation scope.

## Local setup

The Laravel application scaffold and dependency lock file must be generated with Composer from the approved Laravel 13 skeleton. After the scaffold is present, use:

```bash
composer install
cp .env.example .env
php artisan key:generate
php artisan migrate
php artisan serve
```

Configure PostgreSQL connection values in `.env` before running migrations. Do not commit real environment files or credentials.

## API conventions

- Versioned CMS endpoints use the `/api/v1/cms` prefix.
- Authentication is intended to use Laravel Sanctum's stateful cookie/session flow for the first-party CMS SPA.
- API errors and OpenAPI documentation will be established with the API foundation.

## Verification

Run the project's documented test, formatting, and static-analysis commands before considering a change complete.
