<?php

use Symfony\Component\HttpKernel\Exception\MethodNotAllowedHttpException;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;
use App\Exceptions\AccountInactiveException;
use App\Exceptions\EmailAlreadyExistsException;
use App\Exceptions\InternalException;
use App\Exceptions\TooManyRequestsException;
use App\Support\ApiErrorMapper;
use Illuminate\Auth\AuthenticationException;
use Illuminate\Foundation\Application;
use Illuminate\Foundation\Configuration\Exceptions;
use Illuminate\Foundation\Configuration\Middleware;
use Illuminate\Http\Request;
use Illuminate\Validation\ValidationException;

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

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
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

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => (object) [],
                    ],
                ], 403);
            }
        );

        $exceptions->render(
            function (
                NotFoundHttpException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => (object) [],
                    ],
                ], 404);
            }
        );

        $exceptions->render(
            function (
                MethodNotAllowedHttpException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => (object) [],
                    ],
                ], 405);
            }
        );

        $exceptions->render(
            function (
                EmailAlreadyExistsException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
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

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => $e->errors(),
                    ],
                ], 422);
            }
        );

        $exceptions->render(
            function (
                TooManyRequestsException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => (object) [],
                    ],
                ], 429);
            }
        );

        $exceptions->render(
            function (
                InternalException $e,
                Request $request,
            ) {
                if (! $request->is('api/*')) {
                    return null;
                }

                $apiError = ApiErrorMapper::map($e);

                return response()->json([
                    'error' => [
                        ...$apiError,
                        'details' => (object) [],
                    ],
                ], 500);
            }
        );
    })
    ->create();
