<?php

namespace App\Support;

final class ApiError
{
    # [Error 401]
    public const UNAUTHENTICATED = [
        'code' => 'UNAUTHENTICATED',
        'message' => 'Unauthenticated.',
    ];

    # [Error 401]
    public const INVALID_CREDENTIALS = [
        'code' => 'INVALID_CREDENTIALS',
        'message' => 'The provided credentials are incorrect.',
    ];

    # [Error 403]
    public const ACCOUNT_INACTIVE = [
        'code' => 'ACCOUNT_INACTIVE',
        'message' => 'The account is inactive.',
    ];

    # [Error 404]
    public const NOT_FOUND = [
        'code' => 'NOT_FOUND',
        'message' => 'The requested resource was not found.',
    ];

    # [Error 405]
    public const METHOD_NOT_ALLOWED = [
        'code' => 'METHOD_NOT_ALLOWED',
        'message' => 'The requested method is not allowed.',
    ];

    # [Error 409]
    public const EMAIL_ALREADY_EXISTS = [
        'code' => 'EMAIL_ALREADY_EXISTS',
        'message' => 'The email has already been taken.',
    ];

    # [Error 422]
    public const VALIDATION_ERROR = [
        'code' => 'VALIDATION_ERROR',
        'message' => 'The given data is invalid.',
    ];

    # [Error 429]
    public const TOO_MANY_REQUESTS = [
        'code' => 'TOO_MANY_REQUESTS',
        'message' => 'Too many requests.',
    ];

    # [Error 500]
    public const INTERNAL_SERVER_ERROR = [
        'code' => 'INTERNAL_SERVER_ERROR',
        'message' => 'Internal server error.',
    ];
}
