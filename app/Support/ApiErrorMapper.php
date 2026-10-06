<?php

namespace App\Support;

use Symfony\Component\HttpKernel\Exception\MethodNotAllowedHttpException;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;
use App\Exceptions\AccountInactiveException;
use App\Exceptions\EmailAlreadyExistsException;
use App\Exceptions\InvalidCredentialsException;
use App\Exceptions\InternalException;
use App\Exceptions\TooManyRequestsException;
use Illuminate\Auth\AuthenticationException;
use Illuminate\Validation\ValidationException;

final class ApiErrorMapper
{
    public static function map(\Throwable $exception): array
    {
        return match (true) {
            $exception instanceof InvalidCredentialsException
            => ApiError::INVALID_CREDENTIALS,

            $exception instanceof AuthenticationException
            => ApiError::UNAUTHENTICATED,

            $exception instanceof AccountInactiveException
            => ApiError::ACCOUNT_INACTIVE,

            $exception instanceof NotFoundHttpException
            => ApiError::NOT_FOUND,

            $exception instanceof MethodNotAllowedHttpException
            => ApiError::METHOD_NOT_ALLOWED,

            $exception instanceof EmailAlreadyExistsException
            => ApiError::EMAIL_ALREADY_EXISTS,

            $exception instanceof ValidationException
            => ApiError::VALIDATION_ERROR,

            $exception instanceof TooManyRequestsException
            => ApiError::TOO_MANY_REQUESTS,

            $exception instanceof InternalException
            => ApiError::INTERNAL_SERVER_ERROR,

            default => throw $exception,
        };
    }
}
