<?php

use App\Exceptions\AccountInactiveException;
use App\Exceptions\EmailAlreadyExistsException;
use Illuminate\Auth\AuthenticationException;
use Illuminate\Foundation\Application;
use Illuminate\Foundation\Configuration\Exceptions;
use Illuminate\Foundation\Configuration\Middleware;
use Illuminate\Http\Request;
use Illuminate\Validation\ValidationException;
use Throwable;

return Application::configure(basePath: dirname(__DIR__))
    ->withRouting(
        web: __DIR__ . '/../routes/web.php',
        api: __DIR__ . '/../routes/api.php',
        commands: __DIR__ . '/../routes/console.php',
        health: '/up',
    )
    ->withMiddleware(function (Middleware $middleware): void {
        $middleware->statefulApi();

        // API-only application: do not redirect unauthenticated users.
        $middleware->redirectGuestsTo(null);
    })
    ->withExceptions(function (Exceptions $exceptions): void {
        $exceptions->shouldRenderJsonWhen(function (
            Request $request,
            Throwable $e,
        ) {
            return $request->is('api/*') || $request->expectsJson();
        });

        $exceptions->render(
            function (
                AuthenticationException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                return response()->json([
                    'error' => [
                        'code' => 'UNAUTHENTICATED',
                        'message' => $e->getMessage(),
                        'details' => (object) [],
                    ],
                ], 401);
            }
        );

        $exceptions->render(
            function (
                AccountInactiveException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                return response()->json([
                    'error' => [
                        'code' => 'ACCOUNT_INACTIVE',
                        'message' => 'The account is inactive.',
                        'details' => (object) [],
                    ],
                ], 403);
            }
        );

        $exceptions->render(
            function (
                EmailAlreadyExistsException $e,
                Request $request,
            ) {
                return response()->json([
                    'error' => [
                        'code' => 'EMAIL_ALREADY_EXISTS',
                        'message' => 'The email has already been taken.',
                        'details' => (object) [],
                    ],
                ], 409);
            }
        );

        $exceptions->render(
            function (
                ValidationException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                return response()->json([
                    'error' => [
                        'code' => 'VALIDATION_ERROR',
                        'message' => 'The given data is invalid.',
                        'details' => $e->errors(),
                    ],
                ], 422);
            }
        );
    })
    ->create();
